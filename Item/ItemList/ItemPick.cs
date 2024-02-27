using MyTerraria.Tile;
using SFML.System;

namespace MyTerraria.Item.ItemList
{
    public class ItemPick : InfoItem
    {
        public int Power { get; protected set; } = 1;
        public int Speed { get; protected set; } = 1;
        public ItemPick(EnumItem enumItem, int textureId, int maxCountInStack) : base(enumItem, textureId, maxCountInStack)
        {
        }

        public override bool OnItemUse(World world, Vector2i pos, EnumTile type = EnumTile.None)
        {
            if(EItem == EnumItem.Pick)
            {
                world.SetTile(type, pos);
                return false;
            }

            return base.OnItemUse(world, pos, type);
        }
    }
}


