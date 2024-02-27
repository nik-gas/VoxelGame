using MyTerraria.Tile;
using SFML.Graphics;

namespace MyTerraria
{
    class Content
    {
        public const string CONTENT_DIR = "..\\Content\\";
        public static readonly string FONT_DIR = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts) + "\\";

        public static List<SpriteSheet?> ssTile = new List<SpriteSheet?>();
        public static List<Texture> texItems = new List<Texture>();

        // NPC
        public static SpriteSheet? ssNpcSlime; // Слизень

        // Игрок
        public static SpriteSheet? ssPlayerHead;        // Голова
        public static SpriteSheet? ssPlayerHair;        // Волосы
        public static SpriteSheet? ssPlayerShirt;       // Рубашка
        public static SpriteSheet? ssPlayerUndershirt;  // Рукава
        public static SpriteSheet? ssPlayerHands;       // Кисти
        public static SpriteSheet? ssPlayerLegs;        // Ноги
        public static SpriteSheet? ssPlayerShoes;       // Обувь

        // UI
        public static Texture? texUIInvertoryBack;      // Инвертарь
        public static Texture? texUIHelthBar;      // Здорове

        public static Font? font;       // Шрифт
        public static void Load()
        {
            int count = Directory.GetFiles(CONTENT_DIR + "Textures\\Tiles\\", "*", SearchOption.AllDirectories).Length;
            for (int i = 0; i < count; i++)
            {
                ssTile.Add(new SpriteSheet(InfoTile.TileSize, InfoTile.TileSize, false, 0, new Texture(CONTENT_DIR + $"Textures\\Tiles\\Tiles_{i}.png")));
            }
            count = Directory.GetFiles(CONTENT_DIR + "Textures\\Items\\", "*", SearchOption.AllDirectories).Length;
            for (int i = 0; i < count; i++)
            {
                texItems.Add(new Texture(CONTENT_DIR + $"Textures\\Items\\Item_{i}.png"));
            }

            // NPC
            ssNpcSlime = new SpriteSheet(1, 2, true, 0, new Texture(CONTENT_DIR + "Textures\\npc\\slime.png"));

            // Игрок
            ssPlayerHead = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\head.png"));
            ssPlayerHair = new SpriteSheet(1, 14, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\hair.png"));
            ssPlayerShirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\shirt.png"));
            ssPlayerUndershirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\undershirt.png"));
            ssPlayerHands = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\hands.png"));
            ssPlayerLegs = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\legs.png"));
            ssPlayerShoes = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\shoes.png"));

            // UI
            texUIInvertoryBack = new Texture(CONTENT_DIR + "Textures\\ui\\Inventory_Back.png");
            texUIHelthBar = new Texture(CONTENT_DIR + "Textures\\ui\\PlayerHeart.png");

            // Шрифт
            font = new Font(FONT_DIR + "Arial.ttf");
        }
    }
}
