using SFML.Graphics;
using SFML.System;

namespace MyTerraria.UI.UIHelthBar
{
    class UIHelthCell : UIBase
    {
        public int Index;
        public UIHelthCell()
        {
            rectShape = new RectangleShape((Vector2f)Content.texUIHelthBar!.Size);
            rectShape.Texture = Content.texUIHelthBar;
        }

        public FloatRect GetFloatRect()
        {
            return new FloatRect((Vector2f)Position, rectShape.Size);
        }
    }
}