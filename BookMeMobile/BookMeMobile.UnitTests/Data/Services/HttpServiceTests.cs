using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookMeMobile.UnitTests.Data.Services
{
    [TestClass]
    public class HttpServiceTests
    {
        private Mock<IDependencyService> dependencyServiceMock;
        private Mock<IHttpHandler> httpHandlerMock;
        private Mock<IFileWorker> fileWorkerMock;
        private Mock<HttpHeaders> httpRequestHeadersMock;

        [TestInitialize]
        public void Init()
        {
            this.httpHandlerMock = new Mock<IHttpHandler>();
            this.fileWorkerMock = new Mock<IFileWorker>();
            this.dependencyServiceMock = new Mock<IDependencyService>();
            this.httpRequestHeadersMock = new Mock<HttpHeaders>();

            this.dependencyServiceMock.Setup(m => m.Get<IFileWorker>()).Returns(this.fileWorkerMock.Object);
            this.httpHandlerMock.SetupGet(m => m.RequestHeaders).Returns(this.httpRequestHeadersMock.Object);
        }

        [TestMethod]
        public async Task Get_When_Http_Handler_Get_Throws_Exception_Should_Return_Connection_Error()
        {
            //arrange
            this.fileWorkerMock.Setup(m => m.LoadTextAsync()).Returns(Task.FromResult(string.Empty));
            this.httpHandlerMock.Setup(handler => handler.GetAsync(It.IsAny<string>())).Throws(new Exception());
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";

            //act
            var result = await httpService.Get<Room>(testRoute);

            //assert
            Assert.AreEqual(StatusCode.ConnectionProblem, result.Status);
        }
    }
}