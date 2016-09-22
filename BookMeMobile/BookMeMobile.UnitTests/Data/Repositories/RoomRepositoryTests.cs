using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookMeMobile.UnitTests.Data.Repositories
{
    [TestClass]
    public class RoomRepositoryTests
    {
        private Mock<IHttpService> httpServiceMock;

        [TestInitialize]
        public void Init()
        {
            this.httpServiceMock = new Mock<IHttpService>();
        }

        [TestMethod]
        public async Task GetAll_Should_Call_Http_Service_Get_With_Correct_URL()
        {
            //arrange
            RoomRepository roomRepository = new RoomRepository(this.httpServiceMock.Object);

            //act
            await roomRepository.GetAll();

            //assert
            this.httpServiceMock.Verify(m => m.Get<IEnumerable<Room>>(RestURl.RoomURl), Times.Once);
        }
    }
}