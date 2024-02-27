using MyTerraria.Tile;
using SFML.Graphics;
using SFML.System;

namespace MyTerraria.Tile
{
    public class TileDrawble : Transformable, Drawable
    {
        private TileDrawble upTile = null;     // Верхний сосед
        private TileDrawble downTile = null;   // Нижний сосед
        private TileDrawble leftTile = null;   // Левый сосед
        private TileDrawble rightTile = null;  // Правый сосед
        private Color color;
        public TileDrawble UpTile { get => upTile; set { upTile = value; UpdateView(); } }
        public TileDrawble DownTile { get => downTile; set { downTile = value; UpdateView(); } }
        public TileDrawble LeftTile { get => leftTile; set { leftTile = value; UpdateView(); } }
        public TileDrawble RightTile { get => rightTile; set { rightTile = value; UpdateView(); } }
        public Color Color { get => color; set { color = value; UpdateColor(); } }

        /// <summary>
        /// Информация о плитке
        /// </summary>
        public InfoTile InfoTile { get; private set; }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="infoTile"></param>
        public TileDrawble(InfoTile infoTile, TileDrawble leftTile, TileDrawble rightTile, TileDrawble downTile, TileDrawble upTile)
        {
            // Присваиваем соседей, а соседям эту плитку
            if (upTile != null)
            {
                this.upTile = upTile;
                this.upTile.DownTile = this;    // Для верхнего соседа эта плитка будет нижним соседом
            }
            if (downTile != null)
            {
                this.downTile = downTile;
                this.downTile.UpTile = this;    // Для нижнего соседа эта плитка будет верхним соседом
            }
            if (leftTile != null)
            {
                this.leftTile = leftTile;
                this.leftTile.RightTile = this;    // Для левого соседа эта плитка будет правым соседом
            }
            if (rightTile != null)
            {
                this.rightTile = rightTile;
                this.rightTile.LeftTile = this;    // Для правого соседа эта плитка будет левым соседом
            }
            
            InfoTile = infoTile;

            Color = Color.Black;
        }
        private void UpdateColor()
        {
            InfoTile.vertices[0].Color = Color;
            InfoTile.vertices[1].Color = Color;
            InfoTile.vertices[2].Color = Color;
            InfoTile.vertices[3].Color = Color;
            InfoTile.vertices[4].Color = Color;
            InfoTile.vertices[5].Color = Color;
        }

        private IntRect GetTextureRect(int i, int j)
        {
            if(InfoTile != null)
            {
                int SubWidth = Content.ssTile[(int)Convert.ChangeType(InfoTile.ETile, InfoTile.ETile.GetTypeCode())]!.SubWidth;
                int SubHeight = Content.ssTile[(int)Convert.ChangeType(InfoTile.ETile, InfoTile.ETile.GetTypeCode())]!.SubHeight;
                int borderSize = 0;
                int x = i * SubWidth + i * borderSize;
                int y = j * SubHeight + j * borderSize;
                return new IntRect(x, y, SubWidth, SubHeight);
            }

            return new IntRect();
        }
        private void UpdateView()
        {
            IntRect rect = new IntRect();
                    // Если у плитки есть все соседи
                    if (upTile != null && downTile != null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(1 + i, 1);
                    }
                    // Если у плитки отсутствуют все соседи
                    else if (upTile == null && downTile == null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(1 + i, 0);
                    }

                    //---------------

                    // Если у плитки отсутствует только верхний сосед
                    else if (upTile == null && downTile != null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(1 + i, 0);
                    }
                    // Если у плитки отсутствует только нижний сосед
                    else if (upTile != null && downTile == null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(1 + i, 2);
                    }
                    // Если у плитки отсутствует только левый сосед
                    else if (upTile != null && downTile != null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(0, 1);
                    }
                    // Если у плитки отсутствует только правый сосед
                    else if (upTile != null && downTile != null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(4, 1);
                    }

                    //---------------

                    // Если у плитки отсутствует только верхний и левый сосед
                    else if (upTile == null && downTile != null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(0, 0);
                    }
                    // Если у плитки отсутствует только верхний и правый сосед
                    else if (upTile == null && downTile != null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(4, 0);
                    }
                    // Если у плитки отсутствует только нижний и левый сосед
                    else if (upTile != null && downTile == null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(0, 2);
                    }
                    // Если у плитки отсутствует только нижний и правый сосед
                    else if (upTile != null && downTile == null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(4, 2);
                    }
                    // Если у плитки отсуттвует правый и левый соседи
                    else if (upTile != null && downTile != null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(5, 1);
                    }
                    // Если у плитки отсуттвует все коме нижнего соседа
                    else if (upTile == null && downTile != null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(5, 0);
                    }
                    // Если у плитки отсуттвует все кроме верхнего соседа
                    else if (upTile != null && downTile == null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(5, 2);
                    }
                    // Если у плитки отсуттвует верхний и нижний соседи
                    else if (upTile == null && downTile == null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(7, 0);
                    }
                    // Если у плитки отсуттвует все коме левого соседа
                    else if (upTile == null && downTile == null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(8, 0);
                    }
                    // Если у плитки отсуттвует все кроме правого соседа
                    else if (upTile == null && downTile == null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand!.Next(0, 3); // Случайное число от 0 до 2
                        rect = GetTextureRect(6, 0);
                    }
                    UpdateVertex(rect);
        }

        private void UpdateVertex(IntRect rect)
        {
            InfoTile.vertices[0].Position = new Vector2f(0, 0);
            InfoTile.vertices[1].Position = new Vector2f(0, rect.Height);
            InfoTile.vertices[2].Position = new Vector2f(rect.Width, 0);
            InfoTile.vertices[3].Position = new Vector2f(0, rect.Height);
            InfoTile.vertices[4].Position = new Vector2f(rect.Width, 0);
            InfoTile.vertices[5].Position = new Vector2f(rect.Width, rect.Height);

            float left = rect.Left;
            float right = left + rect.Width;
            float top = rect.Top;
            float bottom = top + rect.Height;

            InfoTile.vertices[0].TexCoords = new Vector2f(left, top);
            InfoTile.vertices[1].TexCoords = new Vector2f(left, bottom);
            InfoTile.vertices[2].TexCoords = new Vector2f(right, top);
            InfoTile.vertices[3].TexCoords = new Vector2f(left, bottom);
            InfoTile.vertices[4].TexCoords = new Vector2f(right, top);
            InfoTile.vertices[5].TexCoords = new Vector2f(right, bottom);
        }

        /// <summary>
        /// Рисуем нашу плитку
        /// </summary>
        /// <param name="target"></param>
        /// <param name="states"></param>
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if(InfoTile != null)
            {
                states.Texture = Content.ssTile[InfoTile.TextureId]!.Texture;
                target.Draw(InfoTile!.GetVertices(), PrimitiveType.Triangles, states);
            }
        }
        /// <summary>
        /// Получаем FloatRect
        /// </summary>
        /// <returns></returns>
        public FloatRect GetFloatRect() => new FloatRect(Position, new Vector2f(InfoTile.TileSize, InfoTile.TileSize));
    }
}
