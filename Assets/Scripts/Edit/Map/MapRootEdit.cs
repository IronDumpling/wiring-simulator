using Edit.Object;
using Events;
using UnityEngine;

namespace Edit.Map
{
    public class MapRootEdit : MonoBehaviour
    {
        public MapSetUp mapSetUp;
        [ContextMenu("Save Asset")]
        private void SaveAsset()
        {
            mapSetUp.paths.Clear();
            mapSetUp.nodes.Clear();
            var objectsTransform = transform.Find("Nodes");

            var nodeEdits = objectsTransform.gameObject.GetComponentsInChildren<NodeEdit>();

            int nodeIdx = 0;
            foreach (var edit in nodeEdits)
            {

                if (edit.isEntry) mapSetUp.entryNodeIdx = nodeIdx;
                edit.idx = nodeIdx;
                nodeIdx++;
                mapSetUp.nodes.Add(edit.CreateNode());
            }

            var pathTransform = transform.Find("Paths");
            var pathEdits = pathTransform.gameObject.GetComponentsInChildren<PathEdit>();

            int pathIdx = 0;
            foreach (var edit in pathEdits)
            {
                var path = edit.CreatePath();
                int fromIdx = path.from;
                mapSetUp.nodes[fromIdx].AddPath(pathIdx);
                
                mapSetUp.paths.Add(path);
                pathIdx++;

            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(mapSetUp);
#endif
            Debug.Log("Save Asset");
        }
    }
}