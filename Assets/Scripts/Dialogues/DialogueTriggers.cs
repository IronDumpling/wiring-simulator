using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTriggers : MonoBehaviour{
    [Header("UI")]
    [SerializeField] private UIDocument doc;
    private VisualElement root;
    private VisualElement contents;
    private List<Button> buttons = new List<Button>();

    [Header("Stories")]
    [SerializeField] private List<TextAsset> texts;
    
    void Start(){
        InitButtons();
        DisplayButtons();
    }

    void InitButtons(){
        foreach(TextAsset txt in texts){
            Button button = new Button{
                text = txt.name
            };
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
