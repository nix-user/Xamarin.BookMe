using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookMeMobile.UnitTests.Data.Routes
{
    [TestClass]
    public class RestURLTests
    {
        [TestMethod]
        public void Reservation_Should_Return_Correct_Route()
        {
            Assert.AreEqual("http://10.10.40.80:666/api/reservation/", RestURl.Reservation);
        }

        [TestMethod]
        public void RoomURl_Should_Return_Correct_Route()
        {
            Assert.AreEqual("http://10.10.40.80:666/api/room/", RestURl.RoomURl);
        }

        [TestMethod]
        public void GetEmptyRoom_Should_Return_Correct_Route()
        {
            Assert.AreEqual("http://10.10.40.80:666/api/room/available?From={0}&To={1}&HasPolycom={2}&IsLarge={3}", RestURl.GetEmptyRoom);
        }

        [TestMethod]
        public void GetCurrentRoomReservation_Should_Return_Correct_Route()
        {
            Assert.AreEqual("http://10.10.40.80:666/api/room/reservations?From={0}&To={1}&roomId={2}", RestURl.GetCurrentRoomReservation);
        }

        [TestMethod]
        public void GetToken_Should_Return_Correct_Route()
        {
            Assert.AreEqual("http://10.10.40.80:666/token", RestURl.GetToken);
        }
    }
}