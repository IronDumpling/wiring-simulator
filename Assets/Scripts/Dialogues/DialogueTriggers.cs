using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTriggers : MonoBehaviour{
    [Header("UI")]
    [SerializeField] private UIDocument doc;
    private VisualElement root;
    private VisualElement contents;
    private List<Button> buttons = new List<Button>();
    private VisualTreeAsset m_button;

    [Header("Stories")]
    [SerializeField] private List<TextAsset> texts;

    void Awake(){
        m_button = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/OpenButton");
    }
    
    void Start(){
        InitButtons();
        DisplayButtons();
    }

    void InitButtons(){
        foreach(TextAsset txt in texts){
            Button button = m_button.Instantiate().Q<Button>();
            Length width = new Length(Constants.FULL_WIDTH, LengthUnit.Percent);
            button.style.width = new StyleLength(width);

            button.text = txt.name;
            button.clicked += () => {
                DialogueUI.Instance.BeginDialogue(txt);
            };
            buttons.Add(button);
        }
    }

    void DisplayButtons(){
        root = doc.rootVisualElement;
        contents = root.Q<VisualElement>(name: "Contents");
        for(int i = 0; i < buttons.Count; i++){
            contents.Add(buttons[i]);
        }
    }
}
