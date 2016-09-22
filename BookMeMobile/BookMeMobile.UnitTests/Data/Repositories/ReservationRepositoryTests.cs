using System.Threading.Tasks;
using BookMeMobile.Data;
using BookMeMobile.OperationResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookMeMobile.UnitTests.Data.Repositories
{
    [TestClass]
    public class ReservationRepositoryTests
    {
        private ReservationRepository reservationRepository;
        private Mock<HttpService> httpServcieMock;

        [TestInitialize]
        public void Init()
        {
            this.reservationRepository = new ReservationRepository();
            this.httpServcieMock = new Mock<HttpService>();
        }

        [TestMethod]
        public void Remove_DeleteMethodShouldBeCalledOnce()
        {
            Task<BaseOperationResult> result = this.reservationRepository.Remove(It.IsAny<int>());
            result.Wait();

            this.httpServcieMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        }
    }
}