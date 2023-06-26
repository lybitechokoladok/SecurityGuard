using Moq;
using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Tests.Models
{
    [TestFixture]
    public class RequestTest
    {
        [Test]
        public void ToString_ReturnsClientFullName() 
        {
            Client client = new Client() 
            {
                Id = It.IsAny<int>(),
                FirstName = "test",
                LastName = "test",
                Patronomic = "test",
            };

            Request request = new Request()
            {
                Id = It.IsAny<int>(),
                RequestDetails = It.IsAny<RequestDetails>(),
                VisitingReason = It.IsAny<string>(),
                ArrivalDate = It.IsAny<DateTime>(),
                CreationDate = It.IsAny<DateTime>(),
                Clients = client,
                GroupId = It.IsAny<int>(),
            };

            string clientFullName = request.ToString();

            Assert.That(clientFullName, Is.EqualTo("test test test"));
        }
    }
}
