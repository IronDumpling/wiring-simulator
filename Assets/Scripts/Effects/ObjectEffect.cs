using Unity.VisualScripting;

namespace Effects
{
    public abstract class ObjectEffect
    {
        public void Trigger()
        {
            OnTrigger();
        }

        public abstract void OnTrigger();
    }
}