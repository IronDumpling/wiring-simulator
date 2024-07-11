using System;
using System.Collections.Generic;
using Edit.Events;
using Unity.VisualScripting;
using UnityEngine;

namespace Edit.Map
{
    public class NodeEdit: MonoBehaviour
    {
        public bool isEntry;
        public bool isDestination;

        public NodeStatus status;
        
        [HideInInspector]
        public int idx;
        public Node CreateNode()
        {
            var startTransform = transform.Find("StartingEvent");
            var evtListTransform = transform.Find("ActiveEvents");

            var startingEvt = startTransform.gameObject.GetComponent<EventEdit>().CreateEvent();
            var activeEvts = new List<Event>();
            var activeEvtsEditList = evtListTransform.gameObject.GetComponentsInChildren<EventEdit>();
            
            foreach (var edit in activeEvtsEditList)
            {
                activeEvts.Add(edit.CreateEvent());
            }

            return Node.CreateNode(status, startingEvt, activeEvts);
        }

        
    }
}