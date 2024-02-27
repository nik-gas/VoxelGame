using MyTerraria.Item;
using SFML.Graphics;
using SFML.System;

namespace MyTerraria.Inventory
{
    public class InventoryCell : Transformable, Drawable
    {
        public static Texture texCell = Content.texUIInvertoryBack;
        private Sprite Sprite { get; set; }
        private Sprite spItem { get; set; }
        private Text Text { get; set; }

        private ItemStack itemStack;
        private ItemStack ItemStack 
        { 
            get => itemStack;
            set
            {
                if(itemStack != null && value != null && itemStack.GetInfoItem().ItemId == value.GetInfoItem().ItemId)
                {
                    itemStack.ItemCount += value.ItemCount;
                    return;
                }

                itemStack = value;
                if(itemStack != null)
                    spItem = new Sprite(Content.texItems[itemStack.GetInfoItem().TextureId]);
                spItem.Position = (Vector2f)((texCell.Size / 2) - (spItem.Texture.Size / 2));
            }
        }
        private int Index = 0;

        public InventoryCell(int index)
        {
            Sprite = new Sprite(texCell);
            Text = new Text("0", Content.font, 15);
            Index = index;
        }

        public InventoryCell(ItemStack itemStack, int index)
        {
            Sprite = new Sprite(texCell);
            ItemStack = itemStack;
            Index = index;
        }

        public void SetIndex(int index) => Index = index;
        public void SetItemStack(ItemStack itemStack) => ItemStack = itemStack;

        public int GetIndex() => Index;
        public ItemStack GetItemStack() => ItemStack;
        public ItemStack GetNewItemStack() => new ItemStack(ItemStack.GetInfoItem(), ItemStack.ItemCount);
        public FloatRect GetFloatRect() => new FloatRect(Position, (Vector2f)texCell.Size);
        public void Clear()
        {
            ItemStack = null;
            spItem = null;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if(Sprite != null)
                target.Draw(Sprite, states);
            if(spItem != null)
                target.Draw(spItem, states);
            if(ItemStack != null && ItemStack.GetText() != null)
                target.Draw(ItemStack.GetText(), states);
        }
    }
}
