using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

using Ink.Runtime;
using DG.Tweening;

using Core;

public class DialogueUI : MonoSingleton<DialogueUI>{
    [Header("UI")]
    [SerializeField] private UIDocument m_doc;
    private VisualElement m_root;
    private VisualElement m_panel;
    private VisualElement m_body;
    private Label m_title;
    private ScrollView m_content;
    private Scroller m_scroller;
    private Button m_expandButton;
    private VisualElement m_spacer;
    private VisualTreeAsset choice;
    private VisualTreeAsset m_sectionButton;
    private VisualTreeAsset m_textArea;
    private VisualTreeAsset m_imgArea;
    
    [Header("UI Logic")]
    private float m_scrollSpeed = 0;
    private bool m_isPanelExpanded = true;
    private bool m_isPlaying = false;
    private bool m_canGoToNextLine = false;
    private bool m_isMouseOverElement = false;
    private string m_objName = "";

    [Header("Dialogue")]
    private Story m_currStory;
    private Coroutine m_displayLine;
    private string m_displaySpeakerName = "";
    private DialogueVar m_dialogueVars;

    [Header("Dialogue Content")]
    [SerializeField] private TextAsset m_globalnk;
    [SerializeField] private TextAsset m_defaultInk;
    
    #region Life Cycles
    private void Awake(){
        m_root = m_doc.rootVisualElement;
        m_panel = m_root.Q<VisualElement>(name: "Panel");
        m_body = m_root.Q<VisualElement>(name: "Body");
        m_title = m_root.Q<Label>(name: "Title");

        SetSideBar();
        SetScrollView();

        choice = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/RealChoice");
        m_sectionButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/FakeChoice");
        m_textArea = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/TextArea");
        m_imgArea = Resources.Load<VisualTreeAsset>("Frontends/Documents/Dialogue/ImgArea");

        m_dialogueVars = new DialogueVar(m_globalnk);
    }
    
    private void Start(){   
        OpenExpandPanel();
        BeginDialogue(m_defaultInk);
        // Assuming you have already obtained a reference to your specific VisualElement
        m_body.RegisterCallback<MouseEnterEvent>(evt => MouseEntered(evt));
        m_body.RegisterCallback<MouseLeaveEvent>(evt => MouseLeft(evt));
    }
    
    private void Update(){
        if (!m_isPlaying) return;
        if (m_canGoToNextLine && IsUserInput() 
        && m_currStory.currentChoices.Count == 0){
            ContinueStory();
        }
    }

    public void OnApplicationQuit(){
        m_dialogueVars.SaveVariables();
    }
    #endregion
    
    #region Dialogues
    public void BeginDialogue(TextAsset inkJSON){
        m_currStory = new Story(inkJSON.text);

        m_dialogueVars.StartListening(m_currStory);
        m_dialogueVars.LoadVariables();

        m_isPlaying = true;
        m_title.text = "???";
        m_displaySpeakerName = "???";
        m_content.contentContainer.Clear();
        
        DisplaySpacer();
        OpenExpandPanel();
        ContinueStory();
    }
    
    private void ContinueStory(){
        if(!m_currStory.canContinue){
            StartCoroutine(ExitDialogue());
            return;
        }
        
        // update variable 

        m_currStory.Continue();
        HandleTags(m_currStory.currentTags);

        while(m_currStory.canContinue && m_currStory.currentText == "\n"){
            m_currStory.Continue();
            HandleTags(m_currStory.currentTags);
        }

        if(m_displayLine != null) StopCoroutine(m_displayLine); 
        m_displayLine = StartCoroutine(DisplayLine(m_currStory.currentText));

        ScrollToBottom();
    }
    
    private IEnumerator ExitDialogue(){
        yield return new WaitForSeconds(Constants.EXIT_LAG_TIME);

        m_dialogueVars.StopListening(m_currStory);
        m_dialogueVars.SaveVariables();

        m_isPlaying = false;
        m_displaySpeakerName = "";

        m_currStory = null;

        m_objName = "";

        CloseExpandPanel();
    }
    #endregion

    #region Renders
    private IEnumerator DisplayLine(string line){
        Label label = DisplayTextArea(m_displaySpeakerName + "-").Q<Label>();
        m_canGoToNextLine = false;
        foreach (char letter in line.ToCharArray()){
            if (IsUserInput()) {
                label.text = m_displaySpeakerName + "-" + line;
                break;
            }
            label.text += letter;
            yield return new WaitForSeconds(Constants.TYPE_SPEED);
        }
        
        if(m_objName == "") DisplayChoices();
        else DisplayObjChoices();

        m_canGoToNextLine = true;
    }

    private VisualElement DisplayTextArea(string content){
        VisualElement textLine = m_textArea.Instantiate();
        Label label = textLine.Q<Label>();
        label.text = content;
        m_content.Add(textLine);
        return textLine;
    }
    
    private void DisplayChoices(){
        List<Choice> currChoices = m_currStory.currentChoices;

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
            m_content.Add(choiceElement);
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

    private void DisplayObjChoices(){
        List<Choice> currChoices = m_currStory.currentChoices;
        List<VisualElement> choices = new();
        foreach(Choice chc in currChoices) {   
            VisualElement choiceElement = choice.Instantiate();
            choices.Add(choiceElement);
            m_content.Add(choiceElement);
        }

        if(currChoices.Count < 3) return;

        Button button1 = choices[0].Q<Button>();
        button1.text = "1.-" + currChoices[0].text;
        button1.clicked += () => {
            MakeChoice(currChoices[0], choices);
            Debug.Log("Use");
            GameManager.Instance.GetBackpack().GetObject(m_objName).Use();
        };

        Button button2 = choices[1].Q<Button>();
        button2.text = "2.-" + currChoices[1].text;
        button2.clicked += () => {
            MakeChoice(currChoices[1], choices);
            GameManager.Instance.GetBackpack().RemoveObject(m_objName);
        };
        
        Button button3 = choices[2].Q<Button>();
        button3.text = "3.-" + currChoices[2].text;
        button3.clicked += () => {
            MakeChoice(currChoices[2], choices);
        };   
    }

    private void DisplaySectionButton(Choice chc){
        VisualElement choiceEl = m_sectionButton.Instantiate();
        Button button = choiceEl.Q<Button>();
        button.text = chc.text + " " + '\u25B6';
        button.clicked += () => {
            ClickSectionButton(chc, choiceEl);
        };
        m_content.Add(choiceEl);
    }

    private void DisplayImage(string imgVal){
        VisualElement imgContainer = m_imgArea.Instantiate();
        VisualElement img = imgContainer.Q<VisualElement>(name:"Image");

        Sprite sp = Resources.Load<Sprite>("Arts/Images/" + imgVal);
        if(sp == null){
            Debug.LogError("Can't find image: " + imgVal);
            return;
        }
        if(img.style.width.value.value == 0){
            img.style.width = Constants.MIN_WIDTH;
            Debug.LogWarning("No valid width!");
        } 
        float aspectRatio = (float)sp.textureRect.height / (float)sp.textureRect.width;
        img.style.height = new StyleLength(img.style.width.value.value * aspectRatio);
        img.style.backgroundImage = new StyleBackground(sp);
        m_content.Add(imgContainer);
    }
    
    private void DisplaySpacer(){
        if(m_spacer != null){
            if(m_content.Contains(m_spacer)) m_content.Remove(m_spacer);
            m_spacer = null;
        }
        m_spacer = new VisualElement();
        m_spacer.style.height = Constants.SPACER_HEIGHT;
        m_content.Add(m_spacer);
    }
    
    public void DisplayClickObject(string objName, string inkName){
        TextAsset objInkJson = Resources.Load<TextAsset>("Stories/Backpack/" + inkName);
        this.BeginDialogue(objInkJson);
        m_objName = objName;
        m_title.text = objName;
        m_currStory.variablesState["name"] = objName;
    }
    
    #endregion
    
    #region Logics
    public void CloseExpandPanel(){
        m_panel.style.width = Constants.PANEL_WIDTH;
        Length width = new Length(Constants.HIDE_POSITION, LengthUnit.Percent);
        m_panel.style.left = new StyleLength(width);
        m_body.style.display = DisplayStyle.None;
        m_expandButton.text = "\u2190";
        m_isPanelExpanded = false;
    }

    public void OpenExpandPanel(){
        Length width = new Length(Constants.PANEL_WIDTH, LengthUnit.Percent);
        m_panel.style.width = new StyleLength(width);
        width = new Length(100 - Constants.PANEL_WIDTH, LengthUnit.Percent);
        m_panel.style.left = new StyleLength(width);
        m_body.style.display = DisplayStyle.Flex;
        m_expandButton.text = "\u2192";
        m_isPanelExpanded = true;
    }

    public void SetSideBar() {
        m_expandButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/OpenButton").Instantiate().Q<Button>();
        Length width = new Length(Constants.MARGIN_WIDTH, LengthUnit.Percent);
        m_expandButton.style.width = new StyleLength(width);
        m_root.Q<VisualElement>(name: "SideBar").Add(m_expandButton);
        m_expandButton.clicked += () => {
            if(m_isPanelExpanded) CloseExpandPanel();
            else OpenExpandPanel();
        };
    }

    private void MakeChoice(Choice choice, List<VisualElement> choices){
        if (!m_canGoToNextLine) return;
        foreach(VisualElement choiceEl in choices){
            m_content.Remove(choiceEl);
        }
        DisplayTextArea("你-\"" + choice.text + "\"");
        m_currStory.ChooseChoiceIndex(choice.index);
        ContinueStory();
    }
    
    private void ClickSectionButton(Choice choice, VisualElement choiceEl){
        if (!m_canGoToNextLine) return;
        m_content.Remove(choiceEl);
        m_currStory.ChooseChoiceIndex(choice.index);
        ContinueStory();
    }
    
    private void ScrollToBottom(){
        // move spacer to the bottom
        float m_contentHeight = m_content.contentContainer.layout.height;
        float bottomOffset = Mathf.Max(0, m_contentHeight + m_spacer.layout.height/2f);
        m_spacer.style.top = bottomOffset;

        // scroll to the bottom
        float targetValue = m_scroller.highValue > 0 ? m_scroller.highValue + Constants.SCROLL_OFFSET : 0;
        DOTween.To(()=>m_scroller.value, x=> m_scroller.value = x, targetValue, Constants.EXIT_LAG_TIME);
    }
    
    private void SetScrollView(){
        m_content = m_root.Q<ScrollView>(name: "Content");
        m_content.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
        m_scroller = m_content.verticalScroller;
        m_scroller.valueChanged += ChangeSpeed;
        m_content.RegisterCallback<WheelEvent>(ScrollCallback);
    }
    
    public void ScrollCallback(WheelEvent evt){
        m_content.UnregisterCallback<WheelEvent>(ScrollCallback);
        m_scrollSpeed += evt.delta.y * Constants.SCROLL_SPEED_AMPLIFIER;
        evt.StopPropagation();
        m_content.RegisterCallback<WheelEvent>(ScrollCallback);
    }
    
    public void ChangeSpeed(float num){
        m_scroller.valueChanged -= ChangeSpeed;
        m_scroller.value += m_scrollSpeed;
        m_scrollSpeed -= m_scrollSpeed * Constants.SCROLL_DAMP;
        m_scroller.valueChanged += ChangeSpeed;
    }
    #endregion
    
    #region Variables
    public Ink.Runtime.Object GetVariableState(string varName){
        Ink.Runtime.Object varValue = null;
        m_dialogueVars.variables.TryGetValue(varName, out varValue);
        if (varValue == null) {
            Debug.LogWarning("Ink Variable was found to be null: " + varName);
        }
        return varValue;
    }
    #endregion
    
    #region Inputs
    private void MouseEntered(MouseEnterEvent evt){
        m_isMouseOverElement = true;
    }
    
    private void MouseLeft(MouseLeaveEvent evt){
        m_isMouseOverElement = false;
    }

    private bool IsUserInput(){
        return MouseClick.Instance.isInput && m_isMouseOverElement;
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
                case Constants.SPEAKER_TAG:
                    m_displaySpeakerName = tagValue;
                    break;
                case Constants.TITLE_TAG:
                    m_title.text = tagValue;
                    break;
                case Constants.PORTRAIT_TAG:
                    // TODO
                    break;
                case Constants.IMG_TAG:
                    DisplayImage(tagValue);
                    break;
                // 2. check tags
                case Constants.CHECK_TAG:
                    Check(tagValue);
                    break;
                // 3. world tags
                case Constants.TIME:
                    TimeModification(tagValue);
                    break;
                // 4. object tags
                case Constants.OBJECT_TAG:
                    ObjectModification(tagValue);
                    break;
                // 5. character tags
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

        string[] subStrings = tokens[0].Split(new char[] { '+', '-' }, 
                                            StringSplitOptions.RemoveEmptyEntries);
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
        m_currStory.variablesState[Constants.CHECK] = result.Result.ToString(); // ink var sync
        DisplayTextArea(result.PrintResult());
    }

    private void CharacterModification(string key, string value){
        const int INIT_IDX = 1;
        bool success = int.TryParse(value.Substring(INIT_IDX), out int number);
        if(!success){
            Debug.LogError(key + " tag could not be appropriately parsed");
            return;
        }
        
        if(value.StartsWith("+"))
            GameManager.Instance.GetCharacter().IncreaseVal(key, number);
        else if(value.StartsWith("-"))
            GameManager.Instance.GetCharacter().DecreaseVal(key, number);
        else if(value.StartsWith("="))
            GameManager.Instance.GetCharacter().SetVal(key, number);
        else 
            Debug.LogError(key + " tag could not be appropriately parsed");

        m_currStory.variablesState[key] = GameManager.Instance.GetCharacter().GetVal(key); // sync
        Debug.Log($"{key} now have value {GameManager.Instance.GetCharacter().GetVal(key)}");
    }
    
    private void TimeModification(string value){
        // format: +1d,1hr,10min
        const int INIT_IDX = 1;
        string[] durations = value.Substring(INIT_IDX).Split(",");
        
        int time = 0;
        int number;
        foreach(string duration in durations){
            if(duration.Contains(Constants.MIN) && 
            int.TryParse(duration.Replace(Constants.MIN, ""), out number))
                time += number;
            else if(duration.Contains(Constants.HOUR) && 
            int.TryParse(duration.Replace(Constants.HOUR, ""), out number))
                time += number * Constants.HOUR_TO_MIN;
            else if(duration.Contains(Constants.DAY) && 
            int.TryParse(duration.Replace(Constants.DAY, ""), out number))
                time += number * Constants.DAY_TO_HOUR * Constants.HOUR_TO_MIN;
            else 
                Debug.LogError("'time' tag could not be appropriately parsed");
        }
        
        if(value.StartsWith("+"))
            GameManager.Instance.IncreaseTime(time);
        else if(value.StartsWith("-"))
            GameManager.Instance.DecreaseTime(time);
        else if(value.StartsWith("="))
            GameManager.Instance.SetTime(time);
        else 
            Debug.LogError("'time' tag could not be appropriately parsed");

        m_currStory.variablesState[Constants.TIME] = GameManager.Instance.GetTimeString(); // sync
        
        Debug.Log($"Current Time: {GameManager.Instance.GetTimeString()}");
    }
    
    private void ObjectModification(string value){
        string[] subStrings = value.Split(new char[] { '+', '-' }, 
                                        StringSplitOptions.RemoveEmptyEntries);
        
        List<(string, string, int)> components = new();
        foreach(string subString in subStrings){
            string sign = "+";
            if (value.IndexOf(subString) > 0){
                char prevChar = value[value.IndexOf(subString) - 1];
                if (prevChar == '-') sign = "-";
            }

            string[] tokens = subString.Split('*');
            string obj = tokens[0];
            int count = 1;
            if(tokens.Length > 1 && int.TryParse(tokens[1], out count)){}

            components.Add((obj, sign, count));
        }

        bool isAccepted = GameManager.Instance.GetBackpack().ObjectModification(components);
        if(isAccepted) DisplayTextArea("[操作成功]");
        else DisplayTextArea("[无法进行此操作]");
    }
    #endregion
}
