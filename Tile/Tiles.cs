using MyTerraria.Item;
using MyTerraria.Tile.TileList;

namespace MyTerraria.Tile
{
    public class Tiles
    {
        public static InfoTile ToTile(EnumTile enumTile)
        {
            switch (enumTile)
            {
                case EnumTile.Ground: return new StandatrtTile(0, EnumTile.Ground);
                case EnumTile.Grass: return new StandatrtTile(1, EnumTile.Grass);
                case EnumTile.Stone: return new StandatrtTile(2, EnumTile.Stone);
                case EnumTile.OakLog: return new StandatrtTile(4, EnumTile.OakLog);
                case EnumTile.PlanksOak: return new StandatrtTile(3, EnumTile.PlanksOak).SetCraftRecipe(new Element(EnumTile.OakLog, 1));
            }

            return null;
        }
    }
}

