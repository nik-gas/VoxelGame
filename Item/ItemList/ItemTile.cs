using MyTerraria.Item;
using MyTerraria.Tile;
using SFML.System;

namespace MyTerraria.Item.ItemList
{
    public class ItemTile : InfoItem
    {
        public ItemTile(EnumItem enumItem, int textureId, int maxCountInStack) : base(enumItem, textureId, maxCountInStack)
        {
            
        }

        public override bool OnItemUse(World world, Vector2i pos, EnumTile type = EnumTile.None)
        {
            if(EItem == EnumItem.Tile)
            {
                if(world.GetTileByWorldPos(pos * InfoTile.TileSize) == null)
                {
                    if(type != EnumTile.None)
                    {
                        return world.SetTile(type, pos);
                    }
                    else
                    {
                        type = (EnumTile)ItemId;
                        return world.SetTile(type, pos);
                    }
                }
                return false;
            }

            return base.OnItemUse(world, pos, type);
        }
    }
}