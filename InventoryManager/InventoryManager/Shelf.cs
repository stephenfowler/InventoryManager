namespace InventoryManager
{
    public class Shelf
    {
        private int _quantity;
        public int Quantity {
            get { return _quantity; }
            set { _quantity = _quantity + value; }
        }
        public Item Item { get; set; }

    }
}