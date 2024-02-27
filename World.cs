using MyTerraria.Item;
using MyTerraria.Tile;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace MyTerraria
{
    public class World : Transformable, Drawable
    {
        // Кол-во плиток по ширине и высоте
        public const int WORLD_WIDTH = 300;
        public const int WORLD_HEIGHT = 300;

        public static Random? Rand { private set; get; }

        public static Perlin2D Perlin { private set; get; }

        // Плитки
        Tile.TileDrawble[,] tiles;

        // Предметы
        List<ItemDrawble> items = new List<ItemDrawble>();

        float[] arr = new float[WORLD_WIDTH];
        float[,] arr2 = new float[WORLD_WIDTH, WORLD_HEIGHT];

        // Конструктор класса
        public World()
        {
            tiles = new Tile.TileDrawble[WORLD_WIDTH, WORLD_HEIGHT];
        }
        // Генерируем новый мир
        public void GenerateWorld(int seed = -1)
        {
            Rand = seed >= 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);
            Perlin = seed >= 0 ? new Perlin2D(seed) : new Perlin2D((int)DateTime.Now.Ticks);

            int groundLevelMax = WORLD_HEIGHT / 3 + Rand.Next(10, 20);
            int groundLevelMin = groundLevelMax + Rand.Next(10, 20);

            // Генерация уровня ландшафта
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                float p1 = Perlin.Noise(0.02f * seed, x * seed * 0.02f, 4) * 0.004f;
                float p2 = Perlin.Noise(0.24f * seed, x * seed * 0.24f, 2) * 21; 


                arr[x] = (p1 + p2) / 2 + groundLevelMax;
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    arr2[x, y] = Perlin.Noise(0.02f * seed * y, x * seed * 0.02f, 4) * 7;
                }
            }

            // Сглаживание
            for (int i = 1; i < WORLD_WIDTH - 1; i++)
            {
                float sum = arr[i];
                int count = 1;
                for (int k = 1; k <= 1; k++)
                {
                    int i1 = i - k;
                    int i2 = i + k;

                    if (i1 > 0)
                    {
                        sum += arr[i1];
                        count++;
                    }

                    if (i2 < WORLD_WIDTH)
                    {
                        sum += arr[i2];
                        count++;
                    }
                }

                arr[i] = (int)(sum / count);
            }

            // Ставим плитки на карту
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                SetTile(EnumTile.Grass, x, (int)arr[x]);
                for (int y = (int)arr[x] + 1; y < WORLD_HEIGHT; y++)
                    {
                        SetTile(EnumTile.Ground, x, y);

                        /*if(arr2[y, x] >= 1.3)
                            SetTile(TileType.STONE, x, y);*/

                        if(arr2[y, x] >= 1.7)
                            SetTile(EnumTile.Stone, x, y);
                    }
            }
        }

        // Установить плитку
        public bool SetTile(EnumTile type, Vector2i pos, bool player = false)
        {
            return SetTile(type, pos.X, pos.Y, player);
        }
        public bool SetTile(EnumTile type, int i, int j, bool player = false)
        {
            if(i < 0 || i > WORLD_WIDTH || j < 0 || j > WORLD_HEIGHT)
                return false;

                TileDrawble leftTile = GetTile(i - 1, j);
                TileDrawble rightTile = GetTile(i + 1, j);
                TileDrawble downTile = GetTile(i, j + 1);
                TileDrawble upTile = GetTile(i, j - 1);

            if (type != EnumTile.None)
            {
                if(!player)
                {
                    var tile = new TileDrawble(Tiles.ToTile(type), leftTile, rightTile, downTile, upTile);
                    tile.Position = new Vector2f(i * InfoTile.TileSize, j * InfoTile.TileSize) + Position;
                    tiles[i, j] = tile;

                    return true;
                }
                else 
                    return false;
            }
            else
            {
                var tile = tiles[i, j];
                if (tile != null && tile.InfoTile != null)
                {
                    var item = new ItemDrawble(Items.GetItem(EnumItem.Tile, tile.InfoTile.ETile), this);
                    item.Position = tile.Position;
                    items.Add(item);
                }

                tiles[i, j] = null;

                // Присваиваем соседей, а соседям эту плитку
                if (upTile != null) upTile.DownTile = null;
                if (downTile != null) downTile.UpTile = null;
                if (leftTile != null) leftTile.RightTile = null;
                if (rightTile != null) rightTile.LeftTile = null;

                return true;
            }
        }

        // Получить плитку по мировым координатам
        public TileDrawble GetTileByWorldPos(float x, float y)
        {
            int i = (int)(x / InfoTile.TileSize);
            int j = (int)(y / InfoTile.TileSize);
            return GetTile(i, j);
        }
        public TileDrawble GetTileByWorldPos(Vector2f pos)
        {
            return GetTileByWorldPos(pos.X, pos.Y);
        }
        public TileDrawble GetTileByWorldPos(Vector2i pos)
        {
            return GetTileByWorldPos(pos.X, pos.Y);
        }

        // Получить плитку
        public TileDrawble GetTile(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < WORLD_WIDTH && j < WORLD_HEIGHT)
                return tiles[i, j];
            else
                return null;
        }

        public Time Time = new Time();

        public Color UpdateLight(Vector2f v1, Vector2f v2)
        {
            int dist = (int)MathHelper.GetDistance(v1, v2);
            if(dist < maxLight)
            {
                light = 256 - (maxLight * dist);
                if(light < 0)
                    light = 0;
                            
                return new Color((byte)light,(byte)light,(byte)light);
            }
            else
                return Color.Black;
        }

        // Обновить мир
        public void Update()
        {
            Time.UpdateClock(1.0f);
            //DebugRender.AddText(Content.font, Game.Player.GetPosition(), (Game.Player.GetMousePos()).ToString());
            //DebugRender.AddText(Content.font, Game.Player.GetPosition() - new Vector2f(0, 20), (Game.Player.Invertory.Position).ToString());

            //DebugRender.AddRectangle(Game.Player.Invertory.GetFloatRect(),Color.White);

            int i = 0;
            while (i < items.Count)
            {
                if (items[i].IsDestroyed)
                    items.RemoveAt(i);
                else
                {
                    items[i].Update();
                    items[i].SetColor(UpdateLight(items[i].Position / InfoTile.TileSize, Game.Player.GetPosition() / InfoTile.TileSize));
                    i++;
                }
            }
            for (int index = 0; index < Game.GetCountNPC(); index++)
            {
                var npc = Game.GetNPC(index);

                npc.SetColor(UpdateLight(npc.Position / InfoTile.TileSize, Game.Player.GetPosition() / InfoTile.TileSize));
            }
            /*image2 = new Image(300, 300);
            for (int x = 0; x < 300; x++)
            {
                for (int y = (int)arr[x]; y < 300; y++)
                {
                    Color color1 = new Color();
                    Tiles.Tile tile = GetTile(x, y);
                    if (tile != null)
                    {
                        if(tile.Type == TileType.GRASS)
                            color1 = Color.Green;
                        else if(tile.Type == TileType.GROUND)
                            color1 = new Color(154,86,38);
                        else if(tile.Type == TileType.STONE)
                            color1 = new Color(180,180,180);
                        image2.SetPixel((uint)x, (uint)y, color1);
                    }
                }
            }*/
        }

        int maxLight = 17;
        int light = 0;

        Lighting lighting = new Lighting();
        // Нарисовать мир
        public void Draw(RenderTarget target, RenderStates states)
        { 
            states.Transform *= Transform;

            var tilePos = (Game.Player.Position.X /InfoTile.TileSize, Game.Player.Position.Y / InfoTile.TileSize);
            var tilesPerScreen = (Program.GetWindowSize().X / InfoTile.TileSize, Program.GetWindowSize().Y / InfoTile.TileSize);
            var LeftMostTilesPos = (int)(tilePos.Item1 - (tilesPerScreen.Item1 / 2));
            var TopMostTilesPos = (int)(tilePos.Item2 - (tilesPerScreen.Item2 / 2));
            /*image2 = new Image(image1);
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if(tilePos.Item1 > 0 && tilePos.Item2 > 0)
                        image2.SetPixel((uint)(tilePos.Item1 + x), (uint)(tilePos.Item2 + y), Color.Red);
                }
            }*/
            // Рисуем чанки
            for (int x = LeftMostTilesPos - 1; x < LeftMostTilesPos + tilesPerScreen.Item1 + 1; x++)
            {
                for (int y = TopMostTilesPos; y < TopMostTilesPos + tilesPerScreen.Item2 + 1; y++)
                {
                    if(x > -1 && x < WORLD_WIDTH && y > -1 && y < WORLD_HEIGHT)
                    {
                        var tile = GetTile(x, y);
                        if (tile != null && tile.InfoTile != null)
                        {  
                            /*Color color1 = new Color();
                            if(tile.Type == TileType.GRASS)
                                color1 = Color.Green;
                            else if(tile.Type == TileType.GROUND)
                                color1 = new Color(154,86,38);
                            else if(tile.Type == TileType.STONE)
                                color1 = new Color(180,180,180);
                            image2.SetPixel((uint)x, (uint)y, color1);*/

                            lighting.UpdateLightTile(tile);

                            Color color = UpdateLight(tile.Position / InfoTile.TileSize, new Vector2f(tilePos.Item1, tilePos.Item2));
                            if(tile.Color.R < color.R)
                                tile.Color += color;
                            
                            if(tile.Color != Color.Black)
                            {
                                target.Draw(tile, states);

                                if(Mouse.IsButtonPressed(Mouse.Button.Right) && tile.GetFloatRect().Intersects(new FloatRect((Vector2f)Game.Player.GetMousePos(), new Vector2f(InfoTile.TileSize, InfoTile.TileSize))))
                                {
                                    tile.InfoTile.OnTileUse();
                                }
                            }

                            tile.Color = Color.Black;
                        }
                    }
                }
            }

            //DebugRender.AddImage(new Texture(image2), new Vector2f(Game.Player.GetPosition().X + Program.GetWindowSize().X / 2 - 300, Game.Player.GetPosition().Y - Program.GetWindowSize().Y / 2));

            // Рисуем вещи
            foreach (var item in items)
                if(item.Position.X / InfoTile.TileSize > LeftMostTilesPos && item.Position.X / InfoTile.TileSize < LeftMostTilesPos + tilesPerScreen.Item1)
                    if(item.Position.Y / InfoTile.TileSize > TopMostTilesPos && item.Position.Y / InfoTile.TileSize < TopMostTilesPos + tilesPerScreen.Item2)
                        target.Draw(item);
        }
    }
}
