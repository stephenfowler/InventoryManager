using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager
{
    public class Shelf
    {
        private readonly List<Item> _stock = new List<Item>(); 
        private int _quantity;
        public int Quantity {
            get { return _quantity; }
            set { _quantity = _quantity + value; }
        }

        public void Add(Item newItem)
        {
            Quantity = 1;
            _stock.Add(newItem);
        }

        public Item GetItem()
        {
            Item returnItem = null;
            if (Quantity > 0)
            {
                Quantity = -1;
                returnItem = _stock.OrderBy(i => i.Expiration).FirstOrDefault();
                _stock.Remove(returnItem);
            }
            return returnItem;
        }
    }
}