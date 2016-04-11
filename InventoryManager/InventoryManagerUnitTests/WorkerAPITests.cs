using System;
using InventoryManager;
using NUnit.Framework;
using Moq;
using Newtonsoft.Json;

namespace InventoryManagerUnitTests
{
    [TestFixture]
    public class WorkerAPITests
    {
        private WorkerAPI _worker;
        private Mock<INotifier> _notifierMock = new Mock<INotifier>();
        [OneTimeSetUp]
        public void Setup()
        { 
            _worker = new WorkerAPI(_notifierMock.Object);
        }

        [Test]
        [TestCase(null, "mockAuth")]
        [TestCase("", "mockAuth")]
        public void WorkerAPI_AddInventory_fails_with_null_or_empty__item(string item, string authToken)
        {
            Assert.Throws<ArgumentException>(
                delegate { _worker.Add(item, authToken); }).Message.Equals("item must not be null or empty");
        }

        [Test]
        public void WorkerAPI_AddInventory_fails_without_required_data()
        {
            Assert.Throws<ArgumentException>(
                delegate { _worker.Add(@"{
   'Label': '',
   'Expiration': '',
   'Description': '',
   'Sku': '' 
 }", "mockAuthToken"); });
        }

        [Test]
        public void WorkerAPI_Add_succeeds()
        {
            Assert.DoesNotThrow(delegate { _worker.Add(@"{
   'Label': 'Tuna',
   'Expiration': '2017-05-10T00:00:00Z',
   'Description': 'Canned Tuna gauranteed to not have any dolphin ',
   'Sku': '123456789' 
 }", "mockAuthToken"); });
           
        }

        [Test]
        public void Retrieve_with_invalid_data_does_not_retrieve()
        {
            string retrievedItem = _worker.Retrieve("2");
            Assert.AreEqual("not found", retrievedItem);
        }

        [Test]
        public void Retrieve_with_valid_data_without_stock_returns_no_data()
        {
            _notifierMock.Setup(n => n.Notify(It.IsAny<string>()));
            var item = new Item()
            {
                Description = "Peanut Butter",
                Expiration = DateTime.Today.AddDays(5),
                Label = "Peanut Butter",
                Sku = 9
            };
            string stringItem = JsonConvert.SerializeObject(item);
            _worker.Add(stringItem, "authorized");
            string retrievedItem = _worker.Retrieve("9");

            Assert.AreEqual(stringItem, retrievedItem);
            retrievedItem = _worker.Retrieve("9");

            Assert.AreEqual("Out of Stock", retrievedItem);
            _notifierMock.Verify();
        }

        [Test]
        public void Retrieve_with_valid_data_retrieves_expected_data()
        {
            _notifierMock.Setup(n => n.Notify(It.IsAny<string>()));
            var item = new Item()
            {
                Description = "Peanut Butter",
                Expiration = DateTime.Today.AddDays(5),
                Label = "Peanut Butter",
                Sku = 9
            };
            string stringItem = JsonConvert.SerializeObject(item);
            _worker.Add(stringItem, "authorized");
            string retrievedItem = _worker.Retrieve("9");

            Assert.AreEqual(stringItem, retrievedItem);
            _notifierMock.Verify();
        }
    }
}