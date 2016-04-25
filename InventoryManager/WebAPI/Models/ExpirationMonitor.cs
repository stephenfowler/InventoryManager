using System;
using System.Collections;
namespace WebAPI.Models
{
    public class ExpirationMonitor
    {
        //Dependencies for testing. 
        //Something that would have to be evaluated if this was actually production code. 
        private readonly INotifier _notifier;
        private readonly Shelf _shelf;

        public ExpirationMonitor(INotifier notifier, Shelf shelf)
        {
            _notifier = notifier;
            _shelf = shelf;
        }


        public void ExpungeExpiredItems()
        {
            var now = DateTime.Now;
            var goodItems = new ArrayList();
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