
using System;
using System.Collections.Generic;
using Edit.Events;
using Events;
using UnityEngine;

namespace Edit.Map
{
    public class PathEdit: MonoBehaviour
    {
        public PathStatus status = PathStatus.NotDiscovered;

        public NodeEdit from;
        public NodeEdit to;
        
        [Min(1)]
        public int distance;


        public void OnDrawGizmos()
        {
            if (from != null)
            {
                Gizmos.DrawLine(from.transform.position, this.transform.position);
            }
            
            if (to != null)
                Gizmos.DrawLine(this.transform.position, to.transform.position);

            if (from == null || to == null) return;
            var evts = gameObject.GetComponentsInChildren<PathEventEdit>();

            float distanceFirst = (this.transform.position - from.transform.position).magnitude;
            float distanceSecond = (to.transform.position - this.transform.position).magnitude;
            float percentage = distanceFirst / (distanceFirst + distanceSecond);
            
            foreach (var evt in evts)
            {
                int triggerDist = evt.triggerDistance;
                float triggerPerc = ((float)triggerDist) / distance;

                Vector3 drawPosition;
                if (triggerPerc <= percentage)
                {
                    drawPosition = from.transform.position + triggerPerc / percentage * (this.transform.position - from.transform.position);
                }
                else
                {
                    drawPosition = this.transform.position + ((triggerPerc-percentage)/(1-percentage)) * (to.transform.position - this.transform.position);
                }
                
                Gizmos.DrawIcon(drawPosition, "Light Gizmo.tiff", true, Color.green);

            }

        }

        public Path CreatePath()
        {
            var evts = new List<PathEvent>();
            var evtEdits = gameObject.GetComponentsInChildren<PathEventEdit>();

            foreach (var edit in evtEdits)
            {

                evts.Add(edit.CreatePathEvent());
            }
            var path = Path.CreatePath(status, from.idx, to.idx, distance, evts);

            return path;
        }
    }


}