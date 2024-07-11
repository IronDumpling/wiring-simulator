using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameOverUI : MonoSingleton<GameOverUI>
    {
        [Header("UI")]
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private VisualElement m_panel;
        private Button m_backButton;

        protected override void Init(){
            if(m_doc == null){
                Debug.LogError("No ui document for Backpack Panel");
                return;
            }

            m_root = m_doc.rootVisualElement;
            m_panel = m_root.Q<VisualElement>(name: "panel");
            if(m_backButton == null) DisplayBackButton();
            HidePanel();
        }

        private void DisplayBackButton(){
            m_backButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/OpenButton").Instantiate().Q<Button>();
            m_backButton.text = "重新开始游戏";
            m_backButton.clicked += () => {
                SceneManager.LoadScene(Constants.MAIN_MENU);
            };

            m_panel.Add(m_backButton);
        }

        public void DisplayPanel(){
            m_root.style.display = DisplayStyle.Flex;
        }

        public void HidePanel(){
            m_root.style.display = DisplayStyle.None;
        }
    }
}