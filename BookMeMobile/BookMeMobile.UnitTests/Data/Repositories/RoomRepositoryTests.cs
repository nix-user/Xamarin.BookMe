using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Model;
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

        [TestMethod]
        public async Task GetById_Should_Call_Http_Service_Get_With_Correct_URL()
        {
            //arrange
            RoomRepository roomRepository = new RoomRepository(this.httpServiceMock.Object);
            int roomId = 1;
            string expectedRoute = RestURl.RoomURl + roomId;

            //act
            await roomRepository.GetById(roomId);

            //assert
            this.httpServiceMock.Verify(m => m.Get<Room>(expectedRoute), Times.Once);
        }

        [TestMethod]
        public async Task GetEmptyRoom_With_LargeRoomParameter_Should_Call_Http_Service_Get_With_Correct_URL()
        {
            //arrange
            RoomRepository roomRepository = new RoomRepository(this.httpServiceMock.Object);
            RoomFilterParameters filterParameters = new RoomFilterParameters()
            {
                IsLarge = true,
                HasPolycom = false,
                From = new DateTime(2016, 9, 22, 3, 0, 0),
                To = new DateTime(2016, 9, 22, 5, 0, 0),
            };

            string expectedRoute = string.Format(RestURl.GetEmptyRoom,
                "22-Sep-16 12:00:00 AM",
                "22-Sep-16 2:00:00 AM",
                "False",
                "True");

            //act
            await roomRepository.GetEmptyRoom(filterParameters);

            //assert
            this.httpServiceMock.Verify(m => m.Get<IEnumerable<Room>>(expectedRoute), Times.Once);
        }

        [TestMethod]
        public async Task GetEmptyRoom_With_HasPolycomParameter_Should_Call_Http_Service_Get_With_Correct_URL()
        {
            //arrange
            RoomRepository roomRepository = new RoomRepository(this.httpServiceMock.Object);
            RoomFilterParameters filterParameters = new RoomFilterParameters()
            {
                IsLarge = false,
                HasPolycom = true,
                From = new DateTime(2016, 9, 22, 3, 0, 0),
                To = new DateTime(2016, 9, 22, 5, 0, 0),
            };

            string expectedRoute = string.Format(RestURl.GetEmptyRoom,
                "22-Sep-16 12:00:00 AM",
                "22-Sep-16 2:00:00 AM",
                "True",
                "False");

            //act
            await roomRepository.GetEmptyRoom(filterParameters);

            //assert
            this.httpServiceMock.Verify(m => m.Get<IEnumerable<Room>>(expectedRoute), Times.Once);
        }
    }
}