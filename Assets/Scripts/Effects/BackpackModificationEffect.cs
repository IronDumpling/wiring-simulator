using Core;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class BackpackModificationEffect: ObjectEffect
    {
        [SerializeField]
        private string m_name;
        
        [SerializeField]
        private int m_count;

        private BackpackModificationEffect(string name, int count)
        {
            m_name = name;
            m_count = count;
        }
        
        protected override void OnTrigger()
        {
            if (m_count > 0)
            {
                for (int i = 0; i < m_count; i++) GameManager.Instance.GetBackpack().AddObject(m_name);
            }
            else
            {
                for (int i = 0; i > m_count; i--) GameManager.Instance.GetBackpack().RemoveObject(m_name);
            }
            
        }

        public static BackpackModificationEffect CreateEffect(string name, int count)
        {
            return new BackpackModificationEffect(name, count);
        }
    }
}