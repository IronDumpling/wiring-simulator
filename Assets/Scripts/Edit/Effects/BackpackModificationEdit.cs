using Effects;

namespace Edit.Effects
{
    public class BackpackModificationEdit : EffectEdit
    {
        public string itemName;
        public int modification;

        public override ObjectEffect GetEffect()
        {
            return BackpackModificationEffect.CreateEffect(itemName, modification);
        }
    }
}
