using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

using Core;

public class MainMenuUI : MonoSingleton<MainMenuUI>{
    [Header("UI")]
    [SerializeField] private UIDocument m_doc;
    private VisualElement m_root;
    private Button m_newGameButton;
    private Button m_quitButton;

    private void Awake(){
        m_root = m_doc.rootVisualElement;
        m_newGameButton = m_root.Q<Button>("newGame");
        m_quitButton = m_root.Q<Button>("quit");

        m_newGameButton.clicked += () => {
            OnStartPressed();
        };

        m_quitButton.clicked += () => {
            OnQuitPressed();
        };
    }

    private void OnStartPressed(){
        var world = new WorldState();
        Game.Instance.nextState = world;
    }

    private void OnQuitPressed(){
        Application.Quit();
    }
}
