using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

using Ink.Runtime;
using DG.Tweening;

public class DialogueUI : MonoSingleton<DialogueUI>{
    [Header("Params")]
    [SerializeField] private float TYPE_SPEED = 0.04f;
    [SerializeField] private float SCROLL_SPEED_AMPLIFIER = 50f;
    [SerializeField] private float SCROLL_DAMP = 0.1f;
    [SerializeField] private float SCROLL_OFFSET = 100f;
    [SerializeField] private float SPACER_HEIGHT = 200f; 
    [SerializeField] private float MIN_WIDTH = 500f; 
    [SerializeField] private float EXIT_LAG_TIME = 0.5f;
    [SerializeField] private int PANEL_WIDTH = 30;
    [SerializeField] private float HIDE_POSITION = 98.5f;

    [Header("UI")]
    public UIDocument doc;
    
    [Header("Dialogue Content")]
    [SerializeField] private TextAsset globalJSON;
    [SerializeField] private TextAsset defaultInkJSON;
    
    private VisualElement root;
    private VisualElement expPanel;
    private VisualElement expBody;
    private bool isMouseOverElement = false;
    private Label title;
    private ScrollView content;
    private Scroller scroller;
    private Button expand;
    private VisualElement spacer;
    private VisualTreeAsset choice;
    private VisualTreeAsset sectionButton;
    private VisualTreeAsset textArea;
    private VisualTreeAsset imgArea;

    private float SCROLL_SPEED;
    
    private bool isPanelExpanded = true;
    private bool isPlaying = false;
    private bool canGoToNextLine = false;
    private Story currStory;
    private Coroutine displayLine;
    private const string SPEAKER_TAG = "speaker";
    private const string TITLE_TAG = "title";
    private const string PORTRAIT_TAG = "portrait";
    private const string IMG_TAG = "image";
    private const string DICE_TAG = "dice";
    private const string TIME_TAG = "time";
    private string displaySpeakerName = "";
    private DialogueVar dialogueVars;
    
    #region Life Cycles
    private void Awake()
    {
        root = doc.rootVisualElement;

        expPanel = root.Q<VisualElement>(name: "Panel");
        expBody = root.Q<VisualElement>(name: "Body");

        expand = root.Q<Button>(name: "ExpandButton");
        title = root.Q<Label>(name: "Title");
        SetScrollView();

        choice = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/RealChoice");
        sectionButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/FakeChoice");
        textArea = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/TextArea");
        imgArea = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/ImgArea");
        dialogueVars = new DialogueVar(globalJSON);
    }
    
    private void Start()
    {   
        OpenExpandPanel();
        PreRegisterCallback();
        BeginDialogue(defaultInkJSON);
        // Assuming you have already obtained a reference to your specific VisualElement
        expBody.RegisterCallback<MouseEnterEvent>(evt => MouseEntered(evt));
        expBody.RegisterCallback<MouseLeaveEvent>(evt => MouseLeft(evt));
    }
    
    private void Update(){
        if (!isPlaying) return;
        if (canGoToNextLine && IsUserInput() 
        && currStory.currentChoices.Count == 0){
            ContinueStory();
        }
    }

    public void OnApplicationQuit(){
        dialogueVars.SaveVariables();
    }
    #endregion
    
    #region Panels
    public void CloseExpandPanel(){
        expPanel.style.width = PANEL_WIDTH;
        Length width = new Length(HIDE_POSITION, LengthUnit.Percent);
        expPanel.style.left = new StyleLength(width);
        expBody.style.display = DisplayStyle.None;
        expand.text = "\u2190";
        isPanelExpanded = false;
    }
    
    public void OpenExpandPanel(){
        Length width = new Length(PANEL_WIDTH, LengthUnit.Percent);
        expPanel.style.width = new StyleLength(width);
        width = new Length(100 - PANEL_WIDTH, LengthUnit.Percent);
        expPanel.style.left = new StyleLength(width);
        expBody.style.display = DisplayStyle.Flex;
        expand.text = "\u2192";
        isPanelExpanded = true;
    }
    
    public void PreRegisterCallback() {
        expand.clicked += () => {
            if(isPanelExpanded) CloseExpandPanel();
            else OpenExpandPanel();
        };
    }
    #endregion
    
    #region Dialogues
    public void BeginDialogue(TextAsset inkJSON){
        currStory = new Story(inkJSON.text);

        dialogueVars.StartListening(currStory);
        dialogueVars.LoadVariables();

        isPlaying = true;
        title.text = "???";
        displaySpeakerName = "???";
        content.contentContainer.Clear();
        DisplaySpacer();
        OpenExpandPanel();
        ContinueStory();
    }
    
    private void ContinueStory(){
        if(!currStory.canContinue){
            StartCoroutine(ExitDialogue());
            return;
        }
        
        // update variable 

        currStory.Continue();
        HandleTags(currStory.currentTags);

        while(currStory.canContinue && currStory.currentText == "\n"){
            currStory.Continue();
            HandleTags(currStory.currentTags);
        }

        if(displayLine != null) StopCoroutine(displayLine); 
        displayLine = StartCoroutine(DisplayLine(currStory.currentText));

        MoveSpacerToEnd();
        ScrollToBottom();
    }
    
    private IEnumerator ExitDialogue(){
        yield return new WaitForSeconds(EXIT_LAG_TIME);

        dialogueVars.StopListening(currStory);
        dialogueVars.SaveVariables();

        isPlaying = false;
        displaySpeakerName = "";
        CloseExpandPanel();
    }
    #endregion

    #region Renders
    private IEnumerator DisplayLine(string line){
        VisualElement textLine = textArea.Instantiate();
        Label label = textLine.Q<Label>();
        label.text = displaySpeakerName + "-";
        content.Add(textLine);
        canGoToNextLine = false;
        foreach (char letter in line.ToCharArray()){
            if (IsUserInput()) {
                label.text = displaySpeakerName + "-" + line;
                break;
            }
            label.text += letter;
            yield return new WaitForSeconds(TYPE_SPEED);
        }
        DisplayChoices();
        canGoToNextLine = true;
    }
    
    private void DisplayChoices(){
        List<Choice> currChoices = currStory.currentChoices;

        if(currChoices.Count == 1 && 
        (currChoices[0].text == Constants.CONTINUE || 
        currChoices[0].text == Constants.LEAVE)){
            DisplaySectionButton(currChoices[0]);
            return;
        }
        
        List<VisualElement> choices = new List<VisualElement>();
        foreach(Choice chc in currChoices) {   
            VisualElement choiceElement = choice.Instantiate();
            choices.Add(choiceElement);
            content.Add(choiceElement);
        }
        
        int index = 1;
        foreach(Choice chc in currChoices){
            Button button = choices[index-1].Q<Button>();
            button.text = index + ".-" + chc.text;
            button.clicked += () => {
                MakeChoice(chc, choices);
            };
            index++;
        }
    }

    private void DisplaySectionButton(Choice chc){
        VisualElement choiceEl = sectionButton.Instantiate();
        Button button = choiceEl.Q<Button>();
        button.text = chc.text + " " + '\u25B6';
        button.clicked += () => {
            ClickSectionButton(chc, choiceEl);
        };
        content.Add(choiceEl);
    }

    private void DisplayImage(string imgVal){
        VisualElement imgContainer = imgArea.Instantiate();
        VisualElement img = imgContainer.Q<VisualElement>(name:"Image");

        Sprite sp = Resources.Load<Sprite>("Arts/Images/" + imgVal);
        if(sp == null){
            Debug.LogError("Can't find image: " + imgVal);
            return;
        }
        if(img.style.width.value.value == 0){
            img.style.width = MIN_WIDTH;
            Debug.LogWarning("No valid width!");
        } 
        float aspectRatio = (float)sp.textureRect.height / (float)sp.textureRect.width;
        img.style.height = new StyleLength(img.style.width.value.value * aspectRatio);
        img.style.backgroundImage = new StyleBackground(sp);
        content.Add(imgContainer);
    }
    
    private void DisplaySpacer(){
        if(spacer != null){
            if(content.Contains(spacer)) content.Remove(spacer);
            spacer = null;
        }
        spacer = new VisualElement();
        spacer.style.height = SPACER_HEIGHT;
        content.Add(spacer);
    }
    #endregion
    
    #region Logics
    private void MakeChoice(Choice choice, List<VisualElement> choices){
        if (!canGoToNextLine) return;
        foreach(VisualElement choiceEl in choices){
            content.Remove(choiceEl);
        }
        VisualElement textLine = textArea.Instantiate();
        textLine.Q<Label>().text = "你-\"" + choice.text + "\"";
        content.Add(textLine);
        currStory.ChooseChoiceIndex(choice.index);
        ContinueStory();
    }
    
    private void ClickSectionButton(Choice choice, VisualElement choiceEl){
        if (!canGoToNextLine) return;
        content.Remove(choiceEl);
        currStory.ChooseChoiceIndex(choice.index);
        ContinueStory();
    }
    
    private void MoveSpacerToEnd(){
        float contentHeight = content.contentContainer.layout.height;
        float bottomOffset = Mathf.Max(0, contentHeight + spacer.layout.height/2f);
        spacer.style.top = bottomOffset;
    }
    
    private void ScrollToBottom(){
        float targetValue = scroller.highValue > 0 ? scroller.highValue + SCROLL_OFFSET : 0;
        DOTween.To(()=>scroller.value, x=> scroller.value = x, targetValue, EXIT_LAG_TIME);
    }
    
    private void SetScrollView(){
        content = root.Q<ScrollView>(name: "Content");
        content.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
        scroller = content.verticalScroller;
        scroller.valueChanged += ChangeSpeed;
        content.RegisterCallback<WheelEvent>(ScrollCallback);
    }
    
    public void ScrollCallback(WheelEvent evt){
        content.UnregisterCallback<WheelEvent>(ScrollCallback);
        SCROLL_SPEED += evt.delta.y * SCROLL_SPEED_AMPLIFIER;
        evt.StopPropagation();
        content.RegisterCallback<WheelEvent>(ScrollCallback);
    }
    
    public void ChangeSpeed(float num){
        scroller.valueChanged -= ChangeSpeed;
        scroller.value += SCROLL_SPEED;
        SCROLL_SPEED -= SCROLL_SPEED * SCROLL_DAMP;
        scroller.valueChanged += ChangeSpeed;
    }
    #endregion
    
    #region Variables
    public Ink.Runtime.Object GetVariableState(string varName){
        Ink.Runtime.Object varValue = null;
        dialogueVars.variables.TryGetValue(varName, out varValue);
        if (varValue == null) {
            Debug.LogWarning("Ink Variable was found to be null: " + varName);
        }
        return varValue;
    }
    #endregion
    
    #region Inputs
    private void MouseEntered(MouseEnterEvent evt){
        isMouseOverElement = true;
    }
    
    private void MouseLeft(MouseLeaveEvent evt){
        isMouseOverElement = false;
    }

    private bool IsUserInput(){
        return MouseClick.Instance.isInput && isMouseOverElement;
    }    
    #endregion

    #region Tags
    private void HandleTags(List<string> tags){
        foreach (string tag in tags) {
            Debug.Log("Current tag " + tag);

            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
                return;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            switch (tagKey) {
                // 1. dialogue tags
                case SPEAKER_TAG:
                    displaySpeakerName = tagValue;
                    break;
                case TITLE_TAG:
                    title.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // TODO
                    break;
                case IMG_TAG:
                    DisplayImage(tagValue);
                    break;
                // 2. check tags
                case DICE_TAG:
                    Check(tagValue);
                    break;
                // 3. world tags
                case TIME_TAG:
                    TimeModification(tagValue);
                    break;
                // 4. character tags
                default:
                    if(Utils.IsCharacterTag(tagKey)) CharacterModification(tagKey, tagValue);
                    else Debug.LogWarning("Tag came in but is not being handled: " + tag);
                    break;
            }
        }
    }

    private void Check(string value){
        // format: (component1+component2-component3)>(level)
        string[] tokens = value.Split('>');
        if (tokens.Length != 2) {
            Debug.LogError("'dice' tag could not be appropriately parsed");
            return;
        }

        string[] subStrings = tokens[0].Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
        string level = tokens[1];

        List<(string, string)> components = new List<(string, string)>();
        foreach(string subString in subStrings){
            string sign = "+";
            if (tokens[0].IndexOf(subString) > 0){
                char prevChar = tokens[0][tokens[0].IndexOf(subString) - 1];
                if (prevChar == '-') sign = "-";
            }
            components.Add((subString, sign));
        }

        CheckResultData result = CheckManager.Instance.MakeCheck(components, level);
        
        currStory.variablesState["CHECK"] = result.Result.ToString(); // sync
        
        VisualElement textLine = textArea.Instantiate();
        Label label = textLine.Q<Label>();
        label.text = result.PrintResult();
        content.Add(textLine);
    }

    private void CharacterModification(string key, string value){
        const int INIT_IDX = 1;
        bool success = int.TryParse(value.Substring(INIT_IDX), out int number);
        if(!success){
            Debug.LogError(key + " tag could not be appropriately parsed");
            return;
        }
        
        
        if(value.StartsWith("+")){
            GameManager.Instance.character.IncreaseVal(key, number);
        } 
        else if(value.StartsWith("-")){
            GameManager.Instance.character.DecreaseVal(key, number);
        }
        else if(value.StartsWith("=")){
            GameManager.Instance.character.SetVal(key, number);
        }
        else Debug.LogError(key + " tag could not be appropriately parsed");
        
        Debug.Log($"{key} now have value {GameManager.Instance.character.GetVal(key)}");
    }

    private void TimeModification(string value){
        // format: +1d,1hr,10min
        const int INIT_IDX = 1;
        string[] durations = value.Substring(INIT_IDX).Split(",");

        const string DAY = "d";
        const string HOUR = "hr";
        const string MIN = "min";
        const int DAY_TO_HOUR = 24;
        const int HOUR_TO_MIN = 60;
        
        int time = 0;
        int number;
        foreach(string duration in durations){
            if(duration.Contains(MIN) && 
            int.TryParse(duration.Replace(MIN, ""), out number)){
                time += number;
            }
            else if(duration.Contains(HOUR) && 
            int.TryParse(duration.Replace(HOUR, ""), out number)){
                time += number * HOUR_TO_MIN;
            }
            else if(duration.Contains(DAY) && 
            int.TryParse(duration.Replace(DAY, ""), out number)){
                time += number * DAY_TO_HOUR * HOUR_TO_MIN;
            }
            else Debug.LogError("'time' tag could not be appropriately parsed");
        }
        
        if(value.StartsWith("+")){
            // TODO: ChangeTime(time);
            GameManager.Instance.character.IncreaseTime(time);
        } 
        else if(value.StartsWith("-")){
            // TODO: ChangeTime(-1 * time);
            GameManager.Instance.character.DecreaseTime(time);
        }
        else if(value.StartsWith("=")){
            // TODO: SetTime(time);
            GameManager.Instance.character.SetTime(time);
        }
        else Debug.LogError("'time' tag could not be appropriately parsed");
        
        Debug.Log($"Current Time: {GameManager.Instance.character.GetTime()}");
    }
    #endregion
}
