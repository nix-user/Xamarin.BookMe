using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.OperationResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xamarin.Forms;

namespace BookMeMobile.UnitTests.Data.Repositories
{
    [TestClass]
    public class ReservationRepositoryTests
    {
        private ReservationRepository reservationRepository;
        private Mock<IHttpService> httpServcieMock;

        [TestInitialize]
        public void Init()
        {
            this.httpServcieMock = new Mock<IHttpService>();
            this.reservationRepository = new ReservationRepository(this.httpServcieMock.Object);
        }

        [TestMethod]
        public void Remove_DeleteMethodShouldBeCalledOnce()
        {
            Task<BaseOperationResult> result = this.reservationRepository.Remove(It.IsAny<int>());
            result.Wait();

            this.httpServcieMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GetAll_GetMethodShouldBeCalledOnce()
        {
            Task<BaseOperationResult<IEnumerable<Reservation>>> result = this.reservationRepository.GetAll();
            result.Wait();

            this.httpServcieMock.Verify(x => x.Get<IEnumerable<Reservation>>(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Add_PostMethodShouldBeCalledOnce()
        {
            Task<BaseOperationResult> result = this.reservationRepository.Add(It.IsAny<Reservation>());
            result.Wait();

            this.httpServcieMock.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<Reservation>()), Times.Once);
        }
    }
}