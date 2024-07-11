namespace Effects
{   
    [System.Serializable]
    public abstract class ObjectEffect
    {
        public void Trigger()
        {
            OnTrigger();
        }

        protected abstract void OnTrigger();
        public abstract string EffectDescription();
    }
}