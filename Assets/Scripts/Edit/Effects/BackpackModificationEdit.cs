using System.Collections.Generic;
using Effects;

namespace Edit.Effects
{
    public class BackpackModificationEdit : EffectEdit
    {
        public List<ObjectSnapshot> modifications;

        public override ObjectEffect GetEffect()
        {
            return BackpackModificationEffect.CreateEffect(modifications);
        }
    }
}
