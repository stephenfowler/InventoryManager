using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InventoryManager
{
    public class WorkerAPI
    {
        //This is to replace the "Identity Provider
        private readonly IdP _idp = new IdP();
        //yet again this notifier is mocked.
        private readonly INotifier _notifier;
        //This is to replace the "DB Code"
        private readonly Dictionary<int, Shelf> _storage = new Dictionary<int, Shelf>();

        public WorkerAPI(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void Add(string item, string authToken)
        {
            if (!_idp.IsAuthenticated(authToken) || !_idp.IsAuthorized(authToken))
            {
                //in real life redirect either with 302 or perhaps send them to a different code flow if not HTTP request API
            }

            if (item == null || item.Equals(""))
            {
                throw new ArgumentException("item must not be null or empty");
            }
            Item deserializedItem;
            try
            {
                deserializedItem = JsonConvert.DeserializeObject<Item>(item);
                Shelf shelf;
                if (_storage.TryGetValue(deserializedItem.Sku, out shelf))
                {
                    shelf.Add(deserializedItem);
                    _storage[deserializedItem.Sku] = shelf;
                }
                else
                {
                    shelf = new Shelf();
                    shelf.Add(deserializedItem);
                    _storage.Add(deserializedItem.Sku, shelf);
                }
            }
            catch (Exception)
            {
                //might be nice to have some logging or metrics that would allow us to see what exeptions are being hit most often.
                throw new ArgumentException(@"item must specify all attributes correctly. 
e.g. {'Label': 'Tuna', 
      'Expiration': '2013-01-20T00:00:00Z', 
      'Description': 'canned fish',
      'Sku': 1}");
            }
        }

        public string Retrieve(string s)
        {
            int sku;
            int.TryParse(s, out sku);
            var returnVal = "";
            Shelf shelf;
            if (_storage.TryGetValue(sku, out shelf))
            {
                //return the item, decrease the size, and Notify someone
                var inventory = shelf.Quantity;
                if (inventory == 0)
                {
                    //Could wrap the notifier such that it only notifies if it is specified that this is a notifying API.
                    _notifier.Notify("We are out of stock for sku: " + sku);
                    returnVal = "Out of Stock";
                }
                if (inventory > 0)
                {
                    var returnItem = shelf.GetItem();
                    _storage[sku] = shelf;
                    _notifier.Notify($@"Stock has changed for sku:{sku} from:{inventory} to:{shelf.Quantity}");
                    returnVal = JsonConvert.SerializeObject(returnItem);
                }
            }
            else
            {
                returnVal = "not found";
            }
            return returnVal;
        }
    }
}