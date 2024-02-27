using MyTerraria.Tile;
using SFML.Graphics;
using SFML.System;

namespace MyTerraria.Item
{
   public class InfoItem
   {
        /// <summary>
        /// id текстуры
        /// </summary>
        public int TextureId { get; protected set; } = 0;
        /// <summary>
        /// id предмета
        /// </summary>
        public int ItemId { get; private set; }
        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// Высота
        /// </summary>
        public int Height { get; protected set; }
        /// <summary>
        /// Максимальное количество предметов в стке
        /// </summary>
        public int MaxCountInStack { get; protected set; } = 99;
        /// <summary>
        /// Урон наносимый предметом, при 0 не учитываеться
        /// </summary>
        public int Damage { get; protected set; } = 0;
        /// <summary>
        /// Тип предмета
        /// </summary>
        public EnumItem EItem { get; protected set; }
        /// <summary>
        /// Действие предмета
        /// </summary>
        public EnumItemAction ItemAction { get; protected set; } = EnumItemAction.None;
        
        public CraftItem craft = new CraftItem();
        /// <summary>
        /// Вызываеться при создании нового обекта item
        /// </summary>
        /// <param name="enumItem"> Тип предмета </param>
        /// <param name="textureId"> id текстуры </param>
        /// <param name="maxCountInStack"> максимаальное количесвто в стаке </param>
        public InfoItem(EnumItem enumItem, int textureId, int maxCountInStack)
        {
            EItem = enumItem;
            TextureId = textureId;
            ItemId = textureId;
            if(maxCountInStack > 0)
                MaxCountInStack = maxCountInStack;

            Width = (int)Content.texItems[textureId].Size.X;
            Height = (int)Content.texItems[textureId].Size.Y;
        }

        /// <summary>
        /// Вызываеться при создании нового обекта item
        /// </summary>
        /// <param name="textureId"> id текстуры </param>
        /// <param name="maxCountInStack"> максимаальное количесвто в стаке </param>
        public InfoItem(int textureId, int maxCountInStack)
        {
            TextureId = textureId;
            ItemId = textureId;
            MaxCountInStack = maxCountInStack;
        }

        /// <summary>
        /// Задать тип предмета
        /// </summary>
        /// <param name="enumItem"></param>
        protected void SetEnumItem(EnumItem enumItem)
        {
            EItem = enumItem;
        }
        /// <summary>
        /// Действие предмет
        /// </summary>
        /// <param name="world"></param>
        /// <param name="pos"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool OnItemUse(World world, Vector2i pos, EnumTile type = EnumTile.None) => false;

        public virtual bool OnItemUse() => false;

        /// <summary>
        /// Получить обект крафта
        /// </summary>
        /// <returns>Возвращает обект крафта</returns>
        public virtual CraftItem GetCraft() => craft;
        
        /// <summary>
        /// Установить рецепт крафта
        /// </summary>
        /// <param name="recipe"> Рецепт </param>
        /// <returns> Возвращает предмет </returns>
        public InfoItem SetCraftRecipe(params Element[] recipe)
        {
            craft.SetRecipe(recipe);
            return this;
        }
        /// <summary>
        /// Установить рецепт крафта
        /// </summary>
        /// <param name="amount"> Количество предметов получаеиых на выходе </param>
        /// <param name="recipe"> Рецепт </param>
        /// <returns></returns>
        public InfoItem SetCraftRecipe(int amount, params Element[] recipe)
        {
            craft.SetRecipe(amount, recipe);
            return this;
        }
    }
}