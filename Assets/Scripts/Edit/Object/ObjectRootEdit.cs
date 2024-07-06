using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edit.Object
{
    public class ObjectRootEdit : MonoBehaviour
    {
        public ObjectPoolSO objectPoolSO;
        [ContextMenu("Save Asset")]
        private void SaveAsset()
        {
            objectPoolSO.objects.tools.Clear();
            objectPoolSO.objects.clothes.Clear();
            objectPoolSO.objects.consumables.Clear();
            objectPoolSO.objects.items.Clear();
            
            var consumableTransform = transform.Find("Consumable");
            var consumableEdits = consumableTransform.GetComponentsInChildren<ConsumableEdit>();
            foreach (var edit in consumableEdits)
            {
                objectPoolSO.objects.consumables.Add(edit.CreateConsumable());
            }
            
            var itemTransform = transform.Find("Item");
            var itemEdits = itemTransform.GetComponentsInChildren<ItemEdit>();
            foreach (var edit in itemEdits)
            {
                objectPoolSO.objects.items.Add(edit.CreateItem());
            }
            
            UnityEditor.EditorUtility.SetDirty(objectPoolSO);
            
            Debug.Log("Save Asset");
        }
    }
}
