using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManager;
using Moq;
using NUnit.Framework;

namespace InventoryManagerUnitTests
{
    [TestFixture]
    public class ExpirationMonitorUnitTests
    {
        private MockRepository _factory;
        private Mock<INotifier> _notifierMock;
        private ExpirationMonitor _monitor;
        private Shelf _shelf;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new MockRepository(MockBehavior.Strict);
            _notifierMock = _factory.Create<INotifier>();
            _shelf = new Shelf();
            _monitor = new ExpirationMonitor(_notifierMock.Object, _shelf);
        }

        [Test]
        public void monitor_should_not_notify_if_no_items_present()
        {
            _monitor.ExpungeExpiredItems();
            _notifierMock.Verify();
        }

        [Test]
        public void monitor_should_not_notify_if_a_single_item_is_still_valid()
        {
            _notifierMock.Setup(m => m.Notify(It.IsAny<string>())).AtMostOnce();
            var date = "04/30/2016";
            var dt = Convert.ToDateTime(date);
            var newItem = new Item() { Description = "NewItem", Expiration = dt, Label = "GenericItem", Sku = 1 };
            _shelf.Add(newItem);
            _monitor.ExpungeExpiredItems();
            _notifierMock.Verify();
        }

        [Test]
        public void monitor_should_notify_item_expired()
        {
            _notifierMock.Setup(m => m.Notify(It.IsAny<string>())).AtMostOnce();
            var date = "01/10/2016";
            var dt = Convert.ToDateTime(date);
            var newItem = new Item() { Description = "NewItem", Expiration = dt, Label = "GenericItem", Sku = 1 };
            _shelf.Add(newItem);
            _monitor.ExpungeExpiredItems();
            _notifierMock.Verify();
        }
    }
}
