using MyTerraria.Tile;
using SFML.Graphics;
using SFML.System;

namespace MyTerraria
{
    enum DirectionType
    {
        Left, Right, Up, Down
    }

    abstract class Entity : Transformable, Drawable
    {
        private Color color;

        public bool IsDestroyed = false;        // Объект уничтожен?

        protected RectangleShape rect;
        protected Vector2f velocity;
        protected Vector2f movement;
        protected World world;
        protected bool isFly = true;
        protected bool isRectVisible = true;
        protected bool isGhost = false;         // Режим призрака?
        protected bool isItem = false;

        protected Color Color { get => color; set { color = value; if(rect != null) rect.FillColor = color; } }


        public Entity(World world)
        {
            this.world = world;
        }

        public virtual void Update()
        {
            updatePhysics();
        }

        private void updatePhysics()
        {
            velocity.X *= 0.99f;

            if (!isGhost && !isItem)
            {
                velocity.Y += 0.55f;

                var offset = velocity + movement;
                float dist = MathHelper.GetDistance(offset);

                int countStep = 1;
                if (dist > (float)InfoTile.TileSize / 2)
                    countStep = (int)(dist / (InfoTile.TileSize / 2));

                Vector2f nextPos = Position + offset;
                Vector2f stepPos = Position - rect.Origin;
                FloatRect stepRect = new FloatRect(stepPos, rect.Size);
                Vector2f stepVec = (nextPos - Position) / countStep;

                for (int step = 0; step < countStep; step++)
                {
                    bool isBreakStep = false;

                    stepPos += stepVec;
                    stepRect = new FloatRect(stepPos, rect.Size);

                    DebugRender.AddRectangle(stepRect, Color.Blue);
                    for (int x = 0; x < rect.Size.X / InfoTile.TileSize; x++)
                    {
                        int i = (int)((stepPos.X + rect.Size.X / 1.3f) / InfoTile.TileSize - x);
                        int j = (int)((stepPos.Y + rect.Size.Y) / InfoTile.TileSize);
                        TileDrawble tile = world.GetTile(i, j);

                        if (tile != null)
                        {
                            FloatRect tileRect = tile.GetFloatRect();

                            DebugRender.AddRectangle(tileRect, Color.Red);

                            if (updateCollision(stepRect, tileRect, DirectionType.Down, ref stepPos))
                            {
                                velocity.Y = 0;
                                isFly = false;
                                isBreakStep = true;
                            }
                            else
                                isFly = true;
                        }
                        else
                            isFly = true;

                        if (updateWallCollision(i, j, -1, ref stepPos, stepRect) || updateWallCollision(i, j, 1, ref stepPos, stepRect))
                        {
                            OnWallCollided();
                            isBreakStep = true;
                        }

                        if (isBreakStep)
                            break;
                    }
                    if (isBreakStep)
                            break;
                }

                Position = stepPos + rect.Origin;
            }
            else if(isItem && !isGhost)
            {
                velocity.Y += 0.5f;

                TileDrawble tile = world.GetTileByWorldPos(Position + new Vector2f(0, InfoTile.TileSize));

                if(tile != null)
                {
                    if(new FloatRect(this.Position, rect.Size) == tile.GetFloatRect())
                        Position += velocity;
                    else
                    {
                        velocity.Y = 0;
                    }
                }
                else{
                    Position += velocity;
                }
            }
            else
                Position += velocity + movement;
        }

        bool updateWallCollision(int i, int j, int iOffset, ref Vector2f stepPos, FloatRect stepRect)
        {
            var dirType = iOffset > 0 ? DirectionType.Right : DirectionType.Left;

            TileDrawble[] walls = {
                world.GetTile(i + iOffset, j - 1),
                world.GetTile(i + iOffset, j - 2),
                world.GetTile(i + iOffset, j - 3),
            };

            bool isWallCollided = false;
            foreach (TileDrawble t in walls)
            {
                if (t != null) 
                {

                    FloatRect tileRect = t.GetFloatRect();

                    DebugRender.AddRectangle(tileRect, Color.Yellow);

                    if (updateCollision(stepRect, tileRect, dirType, ref stepPos))
                    {
                        isWallCollided = true;
                    }
                }
            }

            return isWallCollided;
        }

        bool updateCollision(FloatRect rectNPC, FloatRect rectTile, DirectionType direction, ref Vector2f pos)
        {
            if (rectNPC.Intersects(rectTile))
            {
                if (direction == DirectionType.Up)
                    pos = new Vector2f(pos.X, rectTile.Top + rectTile.Height - 1);
                else if (direction == DirectionType.Down)
                    pos = new Vector2f(pos.X, rectTile.Top - rectNPC.Height + 1);
                else if (direction == DirectionType.Left)
                    pos = new Vector2f(rectTile.Left + rectTile.Width - 1, pos.Y);
                else if (direction == DirectionType.Right)
                    pos = new Vector2f(rectTile.Left - rectNPC.Width + 1, pos.Y);

                return true;
            }

            return false;
        }

        public abstract void OnWallCollided();

        public abstract void SetColor(Color color);
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
