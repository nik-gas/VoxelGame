using SFML.Graphics;
using SFML.System;

namespace MyTerraria
{
    public class Camera : Transformable
    {
        private Vector2u WinSize;
        private Vector2f Point;
        private Vector2f Centre;
        public Camera(Vector2u winSize)
        {
            WinSize = winSize;
        }

        public void Update(Vector2f pointUpdate)
        {
            Point = pointUpdate;
            WinSize = Program.GetWindowSize();

            Centre = new Vector2f(Point.X - (WinSize.X / 2), Point.Y - (WinSize.Y / 2));
        }

        public View GetView()
        {
            return new View(new FloatRect(Centre, new Vector2f(WinSize.X, WinSize.Y)));
        }
    }
}