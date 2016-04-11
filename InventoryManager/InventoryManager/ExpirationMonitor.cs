using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    public class ExpirationMonitor
    {
        //Dependencies for testing. 
        //Something that would have to be evaluated if this was actually production code. 
        private INotifier _notifier;
        private Shelf _shelf;

        public ExpirationMonitor(INotifier notifier, Shelf shelf)
        {
            _notifier = notifier;
            _shelf = shelf;
        }


        public void ExpungeExpiredItems()
        {
            DateTime now = DateTime.Now;
            ArrayList goodItems = new ArrayList();
            while (_shelf.Quantity > 0)
            {
                var current = _shelf.GetItem();
                if (current.Expiration < now)
                {
                    _notifier.Notify("An item labeled:" + current.Label + " Expired");
                }
                else
                {
                    goodItems.Add(current);
                }
            }

            foreach (Item item in goodItems)
            {
                _shelf.Add(item);
            }
        }
    }
}
