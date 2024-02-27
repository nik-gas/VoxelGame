using MyTerraria.Item;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyTerraria.Inventory
{
    public class PlayerInventory : Transformable
    {
        private int CellMargine = 2;
        private List<InventoryCell> cells = new List<InventoryCell>();

        public PlayerInventory()
        {
            for(int x = 0; x < 10; x++)
                for(int y = 0; y < 5; y++)
                {
                    InventoryCell cell = new InventoryCell(cells.Count);
                    cell.Position = new Vector2f(x * InventoryCell.texCell.Size.X + CellMargine, y * InventoryCell.texCell.Size.Y + CellMargine) + Position;
                    cells.Add(cell);
                }

            Position -= new Vector2f((InventoryCell.texCell.Size.X + CellMargine) * 5, 0);
        }

        public void Update()
        {
            foreach(var cell in cells)
                if(cell.GetItemStack() != null && cell.GetItemStack().ItemCount <= 0)
                {
                    cell.Clear();
                }
        }
        public bool FindingAnItemInInventory(ItemStack itemStack)
        {
            foreach (var cell in cells)
            {
                if(cell != null && cell.GetItemStack() != null)
                    if(cell.GetItemStack().GetInfoItem().ItemId == itemStack.GetInfoItem().ItemId)
                    {
                        cell.SetItemStack(itemStack);
                        return true;
                    }
            }

            return false;
        }
        public bool AddItemInStack(ItemStack itemStack)
        {
            if(itemStack == null)
                return false;

            if(FindingAnItemInInventory(itemStack))
                return true;
            else
            {
                foreach (var cell in cells)
                {
                    if(cell != null && cell.GetItemStack() == null)
                    {
                        cell.SetItemStack(itemStack);
                        return true;
                    }
                }
            }

            return false;
        }
        public InventoryCell GetCell(int index) => cells[index];

        public void OnClick(Vector2f mousePos, bool isRightButton = false)
        {
            ItemStack ItemInTheAir = null;
            FloatRect mouseRect = new FloatRect(mousePos, new Vector2f(16,16));
            for (int i = 0; i < cells.Count; i++)
            {
                InventoryCell cell = cells[i];
                if(!isRightButton && mouseRect.Intersects(cell.GetFloatRect()))
                {
                    if(ItemInTheAir == null && cell != null && cell.GetItemStack() != null)
                    {
                        ItemInTheAir = cell.GetNewItemStack();
                        cell.Clear();
                    }
                    else if(ItemInTheAir != null && cell != null && cell.GetItemStack() == null)
                    {
                        cell.SetItemStack(ItemInTheAir);
                        ItemInTheAir = null;
                    }
                    else if(ItemInTheAir != null && cell != null && cell.GetItemStack() != null)
                    {
                        if(ItemInTheAir.GetInfoItem().ItemId != cell.GetItemStack().GetInfoItem().ItemId)
                        {
                            var saveItemStak = cell.GetNewItemStack();
                            cell.SetItemStack(ItemInTheAir);
                            ItemInTheAir = saveItemStak;
                        }
                        else
                        {
                            cell.GetItemStack().ItemCount += ItemInTheAir.ItemCount;
                            ItemInTheAir = null;
                        }
                    }
                }
                else if(isRightButton && mouseRect.Intersects(cell.GetFloatRect()))
                {
                    if(ItemInTheAir == null && cell != null && cell.GetItemStack() != null)
                    {
                        ItemInTheAir = cell.GetNewItemStack();
                        ItemInTheAir.ItemCount /= 2;
                        cell.GetItemStack().ItemCount -= ItemInTheAir.ItemCount;
                    }
                    else if(ItemInTheAir != null && cell != null && cell.GetItemStack() == null)
                    {
                        cell.SetItemStack(new ItemStack(ItemInTheAir.GetInfoItem(), 1));
                        ItemInTheAir.ItemCount--;
                    }
                    else if(ItemInTheAir != null && cell != null && cell.GetItemStack() != null)
                    {
                        if(ItemInTheAir.GetInfoItem().ItemId != cell.GetItemStack().GetInfoItem().ItemId)
                        {
                            var saveItemStak = cell.GetNewItemStack();
                            cell.SetItemStack(ItemInTheAir);
                            ItemInTheAir = saveItemStak;
                        }
                        else
                        {
                            cell.GetItemStack().ItemCount++;
                            ItemInTheAir.ItemCount--;
                        }
                    }
                }
            }
        }

        public bool FindingElementCraft(InfoItem infoItem)
        {
            CraftItem craft = infoItem.GetCraft();
            Element[] element = craft.Recipe;

            for (int i = 0; i < element.Length; i++)
            {
                foreach (var cell in cells)
                {
                    if(element[i].GetItemId != cell.GetItemStack().GetInfoItem().ItemId)
                        return false;
                    else if(element[i].GetAmount <= cell.GetItemStack().ItemCount)
                    {
                        cell.GetItemStack().ItemCount -= element[i].GetAmount;
                    }
                }
            }

            return false;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            foreach (var cell in cells)
            {
                target.Draw(cell, states);
            }
        }
    }
}