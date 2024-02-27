using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace MyTerraria
{
    class Program
    {
        private static RenderWindow? Window { set; get; }
        public static Game? Game { private set; get; }
        public static float Delta { private set; get; }

        public static Vector2f WindowCentre { get; set; }

        static void Main(string[] args)
        {
            Window = new RenderWindow(VideoMode.DesktopMode, "Моя Terraria!");
            Window.SetVerticalSyncEnabled(true);

            Window!.Closed += Win_Closed;
            Window!.Resized += Win_Resized;

            // Загрузка контента
            Content.Load();
            
            Game = new Game();      // Создаём новый объект класса игры

            SFML.System.Clock clock = new SFML.System.Clock();

            while (Window.IsOpen)
            {
                Delta = clock.Restart().AsSeconds();

                Window.DispatchEvents();

                Game.Update();

                Window.SetTitle($"MyTerraria, fps {1f / Delta}");

                Window.Clear();

                Game.Draw();

                Window.Display();
            }
        }

        private static void Win_Resized(object sender, SFML.Window.SizeEventArgs e)
        {
            Window?.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            Window?.Close();
        }

        public static void Draw(Drawable draw)
        {
            Window!.Draw(draw);
        }

        public static void SetView(View view)
        {
            Window!.SetView(view);
        }
        public static RenderTarget GetRenderTarget()
        {
            return Window;
        }

        public static RenderStates GetRenderStates()
        {
            return new RenderStates(Transform.Identity);
        }
        public static Vector2u GetWindowSize()
        {
            return Window!.Size;
        }

        public static Vector2i GetMouse()
        {
            return Mouse.GetPosition(Window);
        }
    }
}
