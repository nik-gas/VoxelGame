using MyTerraria.Tile;

namespace MyTerraria.Item
{
    public class Element
    {
        private int IdItem;
        private int Amount = 1;

        public Element(int idItem, int amount)
        {
            IdItem = idItem;
            Amount = amount;
        }

        public Element(EnumTile tileType, int amount)
        {
            IdItem = (int)Convert.ChangeType(tileType, tileType.GetTypeCode());
            Amount = amount;
        }

        public int GetItemId => IdItem;
        public int GetAmount => Amount;
    }
}
