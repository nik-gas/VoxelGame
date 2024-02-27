using SFML.Graphics;
using SFML.System;

namespace MyTerraria.NPC
{
    class NpcSlime : Npc
    {
        protected Vector2f dir;
        const float TIME_WAIT_JUMP = 1f;

        SpriteSheet spriteSheet;
        float waitTimer = 0f;

        public NpcSlime(World world) : base(world)
        {
            spriteSheet = Content.ssNpcSlime;

            rect = new RectangleShape(new Vector2f(spriteSheet.SubWidth / 1.5f, spriteSheet.SubHeight / 1.5f));
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);
            rect.FillColor = new Color(0, 255, 0, 200);

            rect.Texture = spriteSheet.Texture;
            rect.TextureRect = spriteSheet.GetTextureRect(0, 0);

            dir = new Vector2f();
        }

        public override void OnKill()
        {
            Spawn();
        }

        public override void OnWallCollided()
        {
            dir.X *= -1;
            velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
        }

        public override void UpdateNPC()
        {
            dir = new Vector2f(Game.Player.GetPosition().X - this.Position.X, 0);
            dir = MathHelper.Normalize(dir);

            if (!isFly)
            {
                if (waitTimer >= TIME_WAIT_JUMP)
                {
                    velocity = GetJumpVelocity();
                    waitTimer = 0f;
                }
                else
                {
                    waitTimer += 0.05f;
                    velocity.X = 0f;
                }

                rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
            }
            else
                rect.TextureRect = spriteSheet.GetTextureRect(0, 1);
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
        }

        public virtual Vector2f GetJumpVelocity()
        {
            return new Vector2f(dir.X * World.Rand.Next(1, 5), -World.Rand.Next(5, 10));
        }

        public override void SetColor(Color color)
        {
            Color = color;
        }
    }
}
