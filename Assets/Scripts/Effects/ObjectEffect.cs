namespace Effects
{   
    [System.Serializable]
    public abstract class ObjectEffect
    {
        public void Trigger()
        {
            OnTrigger();
        }

        public abstract void OnTrigger();
    }
}