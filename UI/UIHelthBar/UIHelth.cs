using SFML.System;

namespace MyTerraria.UI.UIHelthBar
{
    class UIHelth : UIWindow
    {
        private Vector2f pointPos;
        private List<UIHelthCell> cells = new List<UIHelthCell>();
        public UIHelth(int count)
        {
            IsVisibleTitleBar = false;
            for (int i = 0; i < count; i++)
            {
                UIHelthCell cell = new UIHelthCell();
                cell.Position = new Vector2i(0, cell.Height * i);
                cell.Index = cells.Count;
                cells.Add(cell);
                Childs.Add(cell);
            }

            Size = new Vector2i((int)Content.texUIHelthBar.Size.X, (int)Content.texUIHelthBar.Size.Y * count);
        }

        public override void Update()
        {
            base.Update();

            Position = new Vector2i((int)(pointPos.X + Program.GetWindowSize().X / 2 - 72), (int)pointPos.Y - (int)Program.GetWindowSize().Y / 2 + 72);
        }

                public void PointPosUpdate(Vector2f pos)
        {
            pointPos = pos;
        }
    }
}