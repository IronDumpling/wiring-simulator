using Effects;

namespace Edit.Effects
{
    public class BackpackModificationEdit : EffectEdit
    {
        public string objectName;
        public int modification;

        public override ObjectEffect GetEffect()
        {
            return BackpackModificationEffect.CreateEffect(objectName, modification);
        }
    }
}
