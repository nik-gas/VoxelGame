using SFML.Graphics;
using SFML.System;

namespace MyTerraria.Item
{
    public class ItemStack
    {
        private int itemCount = 0;
        private InfoItem InfoItem{ get; set; }

        private Text textItemCount { get; set; }
        public Texture texItem;
        public int ItemCount
        {
            get
            {
                return itemCount;
            }
            set
            {
                itemCount = value;
                texItem = Content.texItems[InfoItem.TextureId];

                if(textItemCount != null)
                {
                    textItemCount.DisplayedString = itemCount.ToString();
                }
            }
        }
        public ItemStack(InfoItem infoItem, int count)
        {
            InfoItem = infoItem;
            ItemCount = count;
            textItemCount = new Text(count.ToString(), Content.font, 15);
            textItemCount.Color = Color.Black;
        }

        public InfoItem GetInfoItem() => InfoItem;
        public Text GetText() => textItemCount;
    }
}

