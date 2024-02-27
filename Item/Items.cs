using MyTerraria.Item.ItemList;
using MyTerraria.Tile;

namespace MyTerraria.Item
{
    public class Items
    {
        public static InfoItem GetItem(EnumItem enumItem, EnumTile enumTile = EnumTile.None)
        {
            switch (enumItem)
            {
                case EnumItem.Tile: return new ItemTile(EnumItem.Tile, (int)Convert.ChangeType(enumTile, enumTile.GetTypeCode()), 99);
                case EnumItem.Pick: return new ItemPick(enumItem, (int)Convert.ChangeType(enumItem, enumItem.GetTypeCode()), 1);
                case EnumItem.Axe: return new ItemTile(EnumItem.Axe, (int)Convert.ChangeType(EnumItem.Axe, EnumItem.Axe.GetTypeCode()), 1);
            }

            return null;
        }
    }
}
