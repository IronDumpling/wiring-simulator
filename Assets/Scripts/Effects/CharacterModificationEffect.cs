namespace Effects{
    [System.Serializable]
    public class CharacterModificationEffect : ObjectEffect{
        public override void OnTrigger(){

        }

        public static CharacterModificationEffect CreateEffect(){
            return new CharacterModificationEffect();
        }
    }
}