using MyTerraria.NPC;
using MyTerraria.UI;
using MyTerraria.UI.UICraft;
using SFML.System;
using System.Collections.Generic;

namespace MyTerraria
{
    class Game
    {
        public static Player Player { get; private set; }  // Игрок

        public Camera Camera { get; set; }

        public static World World { get => world; set => world = value; }

        private static World world;    // Мир

        // Слизни
        private static List<Npc> npc = new List<Npc>();

        public Game()
        {
            // Создаём новый мир и выполняем его генерацию
            Task.Run( () => 
            {
                world = new World();
                world.GenerateWorld();
            }).Wait();
            // Создаём игрока
            Player = new Player(World);
            Player.StartPosition = new Vector2f(300, 150);
            Player.Spawn();

            //Создайом обект камеры
            Camera = new Camera(Program.GetWindowSize());

            for (int i = 0; i < 1; i++)
            {
                var s = new NpcSlime(World);
                s.StartPosition = new Vector2f(World.Rand!.Next(150, 600), 150);
                s.Direction = World.Rand.Next(0, 2) == 0 ? 1 : -1;
                s.Spawn();
                npc.Add(s);
            }

            // Создаём UI
            Player.Craft = new UICraft();
            Player.InitInvertory();
            Player.HelthBar = new UI.UIHelthBar.UIHelth(5);
            //npc.Add(Player);

            UIManager.AddControl(Player.HelthBar);
            UIManager.AddControl(Player.Craft);
            //UIManager.AddControl(new UIWindow());

            // Включаем прорисовку объектов для визуальной отладки
            DebugRender.Enabled = true;
        }

        // Обновление логики игры
        public async void Update()
        {
            try
            {
                
            
            Task.Run( () =>
            {
                //Обновление мира
                World.Update();
                
                //Обновляем игрока и камеру         
                Player.Update();
                Camera.Update(Player.GetPosition());
                Program.SetView(Camera.GetView());

                //Обновление NPC
                foreach (var s in npc)
                    s.Update();

                // Обновляем UI
                UIManager.UpdateOver();
                UIManager.Update();
            }).Wait();
            }
            catch (System.Exception ex)
            {
                
            }
        }

        // Прорисовка игры
        public void Draw()
        {
            Program.Draw(World);
            Program.Draw(Player);

            foreach (var s in npc)
                Program.Draw(s);

            // Рисуем UI
            UIManager.Draw();

            DebugRender.Draw(Program.GetRenderTarget());
        }

        public static Npc GetNPC(int index)
        {
            if(index >= npc.Count)
                return null;

            return npc[index];
        }

        public static int GetCountNPC()
        {
            if(npc != null)
                return npc.Count;

            return 0;
        }
    }
}
