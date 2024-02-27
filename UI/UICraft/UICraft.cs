using MyTerraria.Item;
using SFML.System;

namespace MyTerraria.UI.UICraft
{
    public class UICraft : UIWindow
    {
        //public List<UIInvertoryCell> cells = new List<UIInvertoryCell>();

        private Vector2f pointPos;

        public static void Init()
        {
            int[,] ItemId = new int[5,5];
            ItemId[0,0] = 3;
        }

        public UICraft()
        {
            IsVisibleTitleBar = false;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    /*UIInvertoryCell cell = new UIInvertoryCell();
                    cell.ItemStack = new UIItemStack(Items.GetItem(EnumItem.Tile, Tile.EnumTile.PlanksOak), 1);
                    cell.Position = new Vector2i(x * cell.Width, y * cell.Height);
                    cells.Add(cell);
                    Childs.Add(cell);*/
                }
            }
        }

        public override void Update()
        {
            Position = new  Vector2i((int)(pointPos.X + Program.GetWindowSize().X / 2 - (5 * 42)) - 72 * 2, (int)(pointPos.Y - Program.GetWindowSize().Y / 2 + 48));

            base.Update();
        }
        public void PointPosUpdate(Vector2f pos)
        {
            pointPos = pos;
        }
    }
}
