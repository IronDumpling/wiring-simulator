using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTriggers : MonoSingleton<DialogueTriggers>{
    [Header("UI")]
    [SerializeField] private UIDocument doc;
    private VisualElement root;
    private VisualElement contents;
    private List<Button> buttons = new List<Button>();
    private VisualTreeAsset m_button;

    [Header("Stories")]
    private List<TextAsset> m_texts = new();


    private List<Event> m_events;
    void Awake(){
        root = doc.rootVisualElement;
        m_button = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/OpenButton");
    }

    public void DisplayEvents(List<Event> events){
        m_texts.Clear();
        m_events = new List<Event>(events);
        DisplayPanel();
        InitEvtButtons();
        InitDepartureButton();
        DisplayButtons();
    }
    
    

    public void DisplayPanel(){
        root.style.display = DisplayStyle.Flex;
    }

    public void HidePanel(){
        root.style.display = DisplayStyle.None;
    }

    void InitButtons(){
        foreach(TextAsset txt in m_texts){
            Button button = m_button.Instantiate().Q<Button>();
            Length width = new Length(Constants.FULL_WIDTH, LengthUnit.Percent);
            button.style.width = new StyleLength(width);

            button.text = txt.name;
            button.clicked += () => {
                DialogueUI.Instance.BeginDialogue(txt);
            };
            buttons.Add(button);
        }
        
        Button btn = m_button.Instantiate().Q<Button>();
        Length wid = new Length(Constants.FULL_WIDTH, LengthUnit.Percent);
        btn.style.width = new StyleLength(wid);

        btn.text = "Departure";
        btn.clicked += () => {
            GameManager.Instance.ChangeToMapSelectionState();
        };
        buttons.Add(btn);
    }

    private void InitEvtButtons()
    {
        foreach(Event evt in m_events){
            Button button = m_button.Instantiate().Q<Button>();
            Length width = new Length(Constants.FULL_WIDTH, LengthUnit.Percent);
            button.style.width = new StyleLength(width);

            button.text = evt.name;
            button.clicked += () => {
                GameManager.Instance.ChangeToDialogueState(evt);
            };
            buttons.Add(button);
        }
        
        
    }

    private void InitDepartureButton()
    {
        Button btn = m_button.Instantiate().Q<Button>();
        Length wid = new Length(Constants.FULL_WIDTH, LengthUnit.Percent);
        btn.style.width = new StyleLength(wid);

        btn.text = "Departure";
        btn.clicked += () => {
            GameManager.Instance.ChangeToMapSelectionState();
        };
        buttons.Add(btn);
    }

    void DisplayButtons(){
        root = doc.rootVisualElement;
        contents = root.Q<VisualElement>(name: "Contents");
        for(int i = 0; i < buttons.Count; i++){
            contents.Add(buttons[i]);
        }
    }
}
