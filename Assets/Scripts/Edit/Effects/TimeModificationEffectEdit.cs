using Effects;

namespace Edit.Effects
{
    public class TimeModificationEffectEdit : EffectEdit
    {

        public int timeModification;

        public override ObjectEffect GetEffect()
        {
            return TimeChangeEffect.CreateEffect(timeModification);
        }
    }
}
