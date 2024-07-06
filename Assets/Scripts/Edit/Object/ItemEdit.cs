
using System.Collections.Generic;

namespace Edit.Object
{
    public class ItemEdit : ObjectEdit
    {
        public ItemCategory category;

        public Item CreateItem()
        {
            var effects = CreateEffects();
            return Item.Init(category, effects, objectName, thumbnail, description, load);
        }
    }
}
