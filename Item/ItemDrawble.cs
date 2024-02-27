using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;

namespace MyTerraria.Item
{
    class ItemDrawble : Entity
    {
        public const float MOVE_DISTANCE_TO_PLAYER = 100f;  // Дистанция начала движения предмета в сторону игрока
        public const float TAKE_DISTANCE_TO_PLAYER = 20f;   // Дистанция подбора предмета игроком
        public const float MOVE_SPEED_COEF = 2f;          // Коэффицент увеличения скорости движения
        InfoItem InfoItem;
        public ItemDrawble(InfoItem infoItem, World world) : base(world)
        {
            InfoItem = infoItem;
            isItem = true;
            rect = new RectangleShape(new Vector2f(infoItem.Width / 2, infoItem.Height / 2)); 
            rect.Texture = Content.texItems[infoItem.TextureId];
        }

        public override void Update()
        {
            Vector2f playerPos = Game.Player.Position;
            float dist = MathHelper.GetDistance(Position, playerPos);

            isGhost = dist < MOVE_DISTANCE_TO_PLAYER;
            if (isGhost)
            {
                if (dist < TAKE_DISTANCE_TO_PLAYER)
                {
                    // Подбираем предмет (пока просто уничтожаем его)
                    if(Game.Player.Inventory.AddItemInStack(new ItemStack(InfoItem, 1)))
                        IsDestroyed = true;
                }
                else
                {
                    Vector2f dir = MathHelper.Normalize(playerPos - Position);
                    float speed = 1f - dist / MOVE_DISTANCE_TO_PLAYER;
                    velocity += dir * speed * MOVE_SPEED_COEF;
                }
            }

            base.Update();

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            target.Draw(rect, states);
        }

        public override void OnWallCollided()
        {
            
        }

        public override void SetColor(Color color)
        {
            
        }
    }
}
