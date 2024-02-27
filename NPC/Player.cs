using System;
using MyTerraria.Inventory;
using MyTerraria.Item;
using MyTerraria.Tile;
using MyTerraria.UI;
using MyTerraria.UI.UICraft;
using MyTerraria.UI.UIHelthBar;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyTerraria.NPC
{
    class Player : Npc
    {
        public const float PLAYER_MOVE_SPEED = 4f;
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        public Color HairColor = new Color(255, 0, 0);  // Цвет волос
        public Color BodyColor = new Color(255, 229, 186);  // Цвет кожи
        public Color ShirtColor = new Color(255, 255, 0);  // Цвет куртки
        public Color LegsColor = new Color(0, 76, 135);  // Цвет штанов

        // UI
        public PlayerInventory Inventory = new PlayerInventory();
        public UIHelth HelthBar;

        public int cellIndex = 0;

        private Vector2i mousePos;
        // Спрайты с анимацией
        AnimSprite asHair;         // Волосы
        AnimSprite asHead;         // Голова
        AnimSprite asShirt;        // Рубашка
        AnimSprite asUndershirt;   // Рукава
        AnimSprite asHands;        // Кисти
        AnimSprite asLegs;         // Ноги
        AnimSprite asShoes;        // Обувь

        public Player(World world) : base(world)
        {            
            rect = new RectangleShape(new Vector2f(InfoTile.TileSize * 1.5f, InfoTile.TileSize * 2.8f));
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);
            isRectVisible = false;

            // Волосы
            asHair = new AnimSprite(Content.ssPlayerHair);
            asHair.Position = new Vector2f(0, 19);
            asHair.Color = HairColor;
            asHair.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHair.AddAnimation("run", new Animation(
                new AnimationFrame(0, 0, 0.1f),
                new AnimationFrame(0, 1, 0.1f),
                new AnimationFrame(0, 2, 0.1f),
                new AnimationFrame(0, 3, 0.1f),
                new AnimationFrame(0, 4, 0.1f),
                new AnimationFrame(0, 5, 0.1f),
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f)
            ));

            // Голова
            asHead = new AnimSprite(Content.ssPlayerHead);
            asHead.Position = new Vector2f(0, 19);
            asHead.Color = BodyColor;
            asHead.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHead.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Рубашка
            asShirt = new AnimSprite(Content.ssPlayerShirt);
            asShirt.Position = new Vector2f(0, 19);
            asShirt.Color = ShirtColor;
            asShirt.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asShirt.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Рукава
            asUndershirt = new AnimSprite(Content.ssPlayerUndershirt);
            asUndershirt.Position = new Vector2f(0, 19);
            asUndershirt.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 1f)
            ));
            asUndershirt.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Кисти
            asHands = new AnimSprite(Content.ssPlayerHands);
            asHands.Position = new Vector2f(0, 19);
            asHands.Color = BodyColor;
            asHands.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHands.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Ноги
            asLegs = new AnimSprite(Content.ssPlayerLegs);
            asLegs.Color = LegsColor;
            asLegs.Position = new Vector2f(0, 19);
            asLegs.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asLegs.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Обувь
            asShoes = new AnimSprite(Content.ssPlayerShoes);
            asShoes.Position = new Vector2f(0, 19);
            asShoes.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 1f)
            ));
            asShoes.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));
        }

        public override void OnKill()
        {
            Spawn();
        }

        public Vector2f GetPosition()
        {
            return this.Position;
        }
        public Vector2i GetMousePos()
        {
            return mousePos;
        }
        public override void UpdateNPC()
        {
            updateMovement();
            updateWarColision();
            UpdateMouse();

            Inventory.Update();
            HelthBar.PointPosUpdate(GetPosition());

            if (UIManager.Over == null && UIManager.Drag == null)
            {
                mousePos = Program.GetMouse();

                mousePos = new Vector2i(mousePos.X + (int)(Game.Player.GetPosition().X - (Program.GetWindowSize().X / 2)), mousePos.Y + (int)(Game.Player.Position.Y - (Program.GetWindowSize().Y / 2)));
                
                Tile.TileDrawble tile = world.GetTileByWorldPos(mousePos);
                if(tile != null && MathHelper.GetDistance(new Vector2f(mousePos.X, mousePos.Y) / 16, this.Position / 16) <= 10)
                {
                    FloatRect tileRect = tile.GetFloatRect();
                    DebugRender.AddRectangle(tileRect, Color.Green);
                }
            }
        }

        private void UpdateMouse()
        {
            mousePos = Program.GetMouse();
            mousePos = new Vector2i(mousePos.X + (int)(Game.Player.GetPosition().X - (Program.GetWindowSize().X / 2)), mousePos.Y + (int)(Game.Player.Position.Y - (Program.GetWindowSize().Y / 2)));
        
            if (UIManager.Over == null && UIManager.Drag == null)
            {
                if(Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    //if(itemStack == null)
                    {
                        InventoryCell cell = Inventory.GetCell(cellIndex);
                        if(cell != null && cell.GetItemStack() != null)
                        {
                            if(cell.GetItemStack().GetInfoItem().OnItemUse(world, mousePos / InfoTile.TileSize))
                                cell.GetItemStack().ItemCount--;
                        }
                    }
                    /*else if(itemStack != null)
                    {
                        InfoItem item = itemStack.InfoItem;
                        if(item != null)
                        {
                            if(item.OnItemUse(world, mousePos / InfoTile.TileSize))
                                itemStack.ItemCount--;

                            if(itemStack.ItemCount == 0)
                                itemStack = null;
                        }
                    }*/
                }
            }
        }

        private void updateWarColision()
        {
            for(int i = 0; i < 6; i++)
            {
                var npc = Game.GetNPC(i);

                FloatRect playerRect = GetFloatRect();//new FloatRect(this.Position, rect.Size);
                if(npc != null)
                {
                    FloatRect npcRect = npc.GetFloatRect();

                    if(playerRect.Intersects(npcRect))
                    {
                        Direction = npc.Direction;
                        DebugRender.AddRectangle(npcRect, Color.Red);
                        if(!isFly)
                        {
                            velocity = new Vector2f(velocity.X * 1.2f, -5);
                            movement.X = 15 * npc.Direction; 
                        }
                        asHair.Color = Color.Red;
                        asHands.Color = Color.Red;
                        asHead.Color = Color.Red;
                        asLegs.Color = Color.Red;
                        asLegs.Color = Color.Red;
                        asShoes.Color = Color.Red;
                    }
                    else
                    {
                        asHair.Color = HairColor;
                        asHead.Color = BodyColor;
                        asShirt.Color = ShirtColor;
                        asLegs.Color = LegsColor;
                        asHands.Color = BodyColor;
                        asShoes.Color = BodyColor;

                        velocity.X = 0;
                    }
                }
                else
                    {
                        asHair.Color = HairColor;
                        asHead.Color = BodyColor;
                        asShirt.Color = ShirtColor;
                        asLegs.Color = LegsColor;
                        asHands.Color = BodyColor;
                        asShoes.Color = BodyColor;

                        velocity.X = 0;
                    }
            }
        }


        public void SetCellSelected()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num0))
                cellIndex = 9;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                cellIndex = 0;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                cellIndex = 1;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                cellIndex = 2;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num4))
                cellIndex = 3;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num5))
                cellIndex = 4;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num6))
                cellIndex = 5;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num7))
                cellIndex = 6;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num8))
                cellIndex = 7;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Num9))
                cellIndex = 8;
        }

        public UICraft Craft { get; internal set; }


        private void updateMovement()
        {
            SetCellSelected();

            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool isJump = Keyboard.IsKeyPressed(Keyboard.Key.Space);
            bool isMove = isMoveLeft || isMoveRight;


            // Прыжок
            if (isJump && !isFly)
            {
                velocity.Y = -10f;
            }

            if (isMove)
            {
                if (isMoveLeft)
                {
                    if (movement.X > 0)
                        movement.X = 0;

                    movement.X -= PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = -1;
                }
                else if (isMoveRight)
                {
                    if (movement.X < 0)
                        movement.X = 0;
                    
                    movement.X += PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = 1;
                }

                if (movement.X > PLAYER_MOVE_SPEED)
                    movement.X = PLAYER_MOVE_SPEED;
                else if (movement.X < -PLAYER_MOVE_SPEED)
                    movement.X = -PLAYER_MOVE_SPEED;

                // Анимация
                asHair.Play("run");
                asHead.Play("run");
                asShirt.Play("run");
                asUndershirt.Play("run");
                asHands.Play("run");
                asLegs.Play("run");
                asShoes.Play("run");
            }
            else
            {
                movement = new Vector2f();

                // Анимация
                asHair.Play("idle");
                asHead.Play("idle");
                asShirt.Play("idle");
                asUndershirt.Play("idle");
                asHands.Play("idle");
                asLegs.Play("idle");
                asShoes.Play("idle");
            }
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
            //states.Transform *= Transform;
            if(Inventory != null)
            {
                Inventory.Draw(target, states);
            }

            target.Draw(asHead, states);
            target.Draw(asHair, states);
            target.Draw(asShirt, states);
            target.Draw(asUndershirt, states);
            target.Draw(asHands, states);
            target.Draw(asLegs, states);
            target.Draw(asShoes, states);
        }

        public void InitInvertory()
        {
            Inventory.AddItemInStack(new ItemStack(Items.GetItem(EnumItem.Pick), 1));
            Inventory.AddItemInStack(new ItemStack(Items.GetItem(EnumItem.Axe), 1));
            Inventory.AddItemInStack(new ItemStack(Items.GetItem(EnumItem.Tile, EnumTile.OakLog), 10));
        }

        public override void OnWallCollided()
        {
            
        }

        public override void SetColor(Color color)
        {
            Color = color;
        }
    }
}
