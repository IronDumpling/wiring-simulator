using System.Collections.Generic;

using Effects;

namespace Edit.Effects
{
    public class CharacterModificationEdit : EffectEdit
    {
        public List<CharacterModification> modifications;
        
        public override ObjectEffect GetEffect()
        {
            return CharacterModificationEffect.CreateEffect(modifications);
        }
    }
}