namespace MyTerraria.Item
{
    public class CraftItem
    {
        public int Amount { get; private set; } = 1;

        public Element[] Recipe { get; protected set; } = new Element[0];

        public CraftItem SetRecipe(params Element[] recipe)
        {
            Recipe = recipe;
            return this;
        }

        public CraftItem SetRecipe(int amount, params Element[] recipe)
        {
            Amount = amount;
            Recipe = recipe;
            return this;
        }
    }
}


