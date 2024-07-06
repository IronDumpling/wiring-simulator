
using System.Collections.Generic;
using Effects;

namespace Edit.Object
{
    public class ConsumableEdit : ObjectEdit
    {
        public ConsumableCategory category;

        public Consumable CreateConsumable()
        {
            var effects = CreateEffects();
            return Consumable.Init(category, effects, objectName, thumbnail, description, load);
        }
    }
}
