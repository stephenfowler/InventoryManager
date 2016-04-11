using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManager;
using NUnit.Framework;

namespace InventoryManagerUnitTests
{
    [TestFixture]
    public class ShelfUnitTests
    {
        private Shelf _shelf;

        [SetUp]
        public void Setup()
        {
            _shelf = new Shelf();
        }
        [Test]
        public void new_shelf_should_have_count_of_zero()
        {
            Shelf newShelf = new Shelf();
            Assert.AreEqual(0, newShelf.Quantity);
        }

        [Test]
        public void adding_an_item_should_update_count_by_one()
        {
            var date = "04/30/2016";
            var dt = Convert.ToDateTime(date);
            var newItem = new Item() {Description = "NewItem", Expiration = dt, Label = "GenericItem", Sku = 1};
            _shelf.Add(newItem);
            Assert.AreEqual(1, _shelf.Quantity);
        }

        [Test]
        public void getItem_should_return_item_and_reduce_count()
        {
            var date = "04/30/2016";
            var dt = Convert.ToDateTime(date);
            var newItem = new Item() { Description = "NewItem", Expiration = dt, Label = "GenericItem", Sku = 1 };
            _shelf.Add(newItem);
            Assert.AreEqual(1, _shelf.Quantity);

            _shelf.GetItem();
            Assert.AreEqual(0, _shelf.Quantity);
        }

        [Test]
        public void getItem_should_return_item_with_soonest_expiration()
        {
            var date = "04/30/2016";
            var dt = Convert.ToDateTime(date);
            var newItem = new Item() { Description = "NewItem", Expiration = dt, Label = "GenericItem", Sku = 1 };
            _shelf.Add(newItem);
            date = "04/29/2016";
            dt = Convert.ToDateTime(date);
            newItem = new Item() { Description = "NewItem", Expiration = dt, Label = "GenericItem", Sku = 1 };
            _shelf.Add(newItem);

            Assert.AreEqual(2, _shelf.Quantity);

            var returnedItem = _shelf.GetItem();
            Assert.AreEqual(1, _shelf.Quantity);
            Assert.AreEqual(dt, returnedItem.Expiration);
        }
    }
}
