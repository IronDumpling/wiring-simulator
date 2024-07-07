using Core;
using UnityEngine;

namespace Init{
    public class InitSceneRoot : MonoBehaviour{
        // Start is called before the first frame update
        void Start(){
            // Invoke(nameof(OnStartPressed), 2);
        }
    
        public void OnStartPressed(){
            var world = new WorldState();
            Game.Instance.nextState = world;
        }
    }
}
