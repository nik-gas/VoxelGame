using System.Dynamic;
using MyTerraria.Item;
using SFML.Graphics;
using SFML.System;

namespace MyTerraria.Tile
{
    public class InfoTile
    {   
        /// <summary>
        /// Размер плитки( константа )
        /// </summary>
        public const int TileSize = 16;
        /// <summary>
        /// Номер текстуры
        /// </summary>
        public int TextureId { get; protected set; }

        /// <summary>
        /// id плитки
        /// </summary>
        public int TileId { get; protected set; }
        /// <summary>
        /// Тип плитки
        /// </summary>
        public EnumTile ETile { get; set; }
        /// <summary>
        /// Масив вершин
        /// </summary>
        public Vertex[] vertices { get; protected set; } = new Vertex[6];
        /// <summary>
        /// Краф
        /// </summary>
        public CraftItem craft { get; protected set; } = new CraftItem();

        /// <summary>
        /// Создаем экземляр класса
        /// </summary>
        /// <param name="textureId"> номер текстуры </param>
        /// <param name="enumTile"> тип блока </param>
        public InfoTile(int textureId, EnumTile enumTile)
        {
            TextureId = textureId;
            ETile = enumTile;
            InitVertices();
        }
        /// <summary>
        /// Создаем экземляр класса
        /// </summary>
        /// <param name="textureId"> номер текстуры </param>
        /// <param name="enumTile"> тип блока </param>
        /// <param name="vertices"> задаем свой массив вершин </param>
        public InfoTile(int textureId, EnumTile enumTile, Vertex[] vertices)
        {
            TextureId = textureId;
            ETile = enumTile;
            this.vertices = vertices;
        }

        /// <summary>
        /// Инициализируем масив vertices
        /// </summary>
        private void InitVertices()
        {
            vertices[0] = new Vertex(new Vector2f(0, 0), new Vector2f(0, 0));
            vertices[1] = new Vertex(new Vector2f(0, TileSize), new Vector2f(0, TileSize));
            vertices[2] = new Vertex(new Vector2f(TileSize, 0), new Vector2f(TileSize, 0));
            vertices[3] = new Vertex(new Vector2f(0, TileSize), new Vector2f(0, TileSize));
            vertices[4] = new Vertex(new Vector2f(TileSize, 0), new Vector2f(TileSize, 0));
            vertices[5] = new Vertex(new Vector2f(TileSize, TileSize), new Vector2f(TileSize, TileSize));
        }
        public Vertex[] GetVertices() => vertices;

        public virtual bool OnTileUse() => false;
        /// <summary>
        /// Получаем крафт блока
        /// </summary>
        /// <returns></returns>
        public CraftItem GetCraft() => craft;
        /// <summary>
        /// Установить рецепт
        /// </summary>
        /// <param name="recipe"> Рецепт </param>
        /// <returns></returns>
        public InfoTile SetCraftRecipe(params Element[] recipe)
        {
            craft.SetRecipe(recipe);
            return this;
        }
        /// <summary>
        /// Установить рецепт
        /// </summary>
        /// <param name="amount"> Количество блоков на выходе </param>
        /// <param name="recipe"> Рецепт </param>
        /// <returns></returns>
        public InfoTile SetCraftRecipe(int amount, params Element[] recipe)
        {
            craft.SetRecipe(amount, recipe);
            return this;
        }

    }
}


