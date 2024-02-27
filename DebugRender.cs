using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace MyTerraria
{
    public class DebugRender
    {
        public static bool Enabled = false;

        static List<Drawable> objects = new List<Drawable>();

        public static void AddRectangle(float x, float y, float w, float h, Color color)
        {
            if (!Enabled) return;

            var obj = new RectangleShape(new Vector2f(w, h));
            obj.Position = new Vector2f(x, y);
            obj.FillColor = Color.Transparent;
            obj.OutlineColor = color;
            obj.OutlineThickness = 1;
            objects.Add(obj);
        }

        public static void AddRectangle(FloatRect rect, Color color)
        {
            AddRectangle(rect.Left, rect.Top, rect.Width, rect.Height, color);
        }

        public static void AddText(Font font, Vector2f pos, string mess)
        {
            var obj = new Text(mess, font);
            obj.Position = pos;
            objects.Add(obj);
        }

        public static void AddImage(Texture tx, Vector2f pos)
        {
            var obj = new Sprite(tx);
            obj.Position = pos;
            obj.Scale = new Vector2f(0.7f, 0.7f);
            objects.Add(obj);
        }

        public static void AddImage(Sprite sp, Vector2f pos)
        {
            var obj = sp;
            obj.Position = pos;
            obj.Scale = new Vector2f(0.7f, 0.7f);
            objects.Add(obj);
        }

        public static void Draw(RenderTarget target)
        {
            if (!Enabled) return;

            foreach (var obj in objects)
                target.Draw(obj);

            objects.Clear();
        }
    }
}
