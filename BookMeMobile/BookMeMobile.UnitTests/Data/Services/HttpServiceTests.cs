using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

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
            this.fileWorkerMock.Setup(m => m.LoadTextAsync(It.IsAny<string>())).Returns(Task.FromResult(string.Empty));
        }

        [TestMethod]
        public async Task Get_When_Http_Handler_Get_Throws_Exception_Should_Return_Connection_Error()
        {
            //arrange
            this.httpHandlerMock.Setup(handler => handler.GetAsync(It.IsAny<string>())).Throws(new Exception());
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";

            //act
            var result = await httpService.Get<ServiceResponseTestClass>(testRoute);

            //assert
            Assert.AreEqual(StatusCode.ConnectionProblem, result.Status);
        }

        [TestMethod]
        public async Task Get_When_Http_Handler_Get_Executes_Successfuly_Should_Return_Requested_Entity()
        {
            //arrange
            var responseMessageContent = (new ResponseModel<ServiceResponseTestClass>()
            {
                IsOperationSuccessful = true,
                Result = new ServiceResponseTestClass() { Id = 1 }
            });

            var testResponseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseMessageContent))
            };
            this.httpHandlerMock.Setup(handler => handler.GetAsync(It.IsAny<string>())).ReturnsAsync(testResponseMessage);
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";

            //act
            var retrieval = await httpService.Get<ServiceResponseTestClass>(testRoute);

            //assert
            Assert.AreEqual(StatusCode.Ok, retrieval.Status);
            Assert.AreEqual(responseMessageContent.Result.Id, retrieval.Result.Id);
        }

        [TestMethod]
        public async Task Get_When_Http_Handler_Returns_Internal_Server_Error_Should_Return_Error_Status()
        {
            //arrange
            var responseMessageContent = (new ResponseModel<ServiceResponseTestClass>()
            {
                IsOperationSuccessful = false,
            });

            var testResponseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseMessageContent))
            };
            this.httpHandlerMock.Setup(handler => handler.GetAsync(It.IsAny<string>())).ReturnsAsync(testResponseMessage);
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";

            //act
            var retrieval = await httpService.Get<ServiceResponseTestClass>(testRoute);

            //assert
            Assert.AreEqual(StatusCode.Error, retrieval.Status);
        }

        [TestMethod]
        public async Task Post_When_Http_Handler_Post_Throws_Exception_Should_Return_Connection_Error()
        {
            //arrange
            this.httpHandlerMock.Setup(handler => handler.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).Throws(new Exception());
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";
            var testContent = new ServiceResponseTestClass();
            //act
            var result = await httpService.Post(testRoute, testContent);

            //assert
            Assert.AreEqual(StatusCode.ConnectionProblem, result.Status);
        }

        [TestMethod]
        public async Task Post_When_Http_Handler_Post_Executes_Successfuly_Should_Return_Success_Status()
        {
            //arrange
            var responseMessageContent = (new ResponseModel<ServiceResponseTestClass>()
            {
                IsOperationSuccessful = true
            });

            var testResponseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseMessageContent))
            };
            this.httpHandlerMock.Setup(handler => handler.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(testResponseMessage);
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";
            var testContent = new ServiceResponseTestClass();

            //act
            var retrieval = await httpService.Post(testRoute, testContent);

            //assert
            Assert.AreEqual(StatusCode.Ok, retrieval.Status);
        }

        [TestMethod]
        public async Task Post_When_Http_Handler_Returns_Internal_Server_Error_Should_Return_Error_Status()
        {
            //arrange
            var responseMessageContent = (new ResponseModel<ServiceResponseTestClass>()
            {
                IsOperationSuccessful = false
            });

            var testResponseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseMessageContent))
            };
            this.httpHandlerMock.Setup(handler => handler.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(testResponseMessage);
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";
            var testContent = new ServiceResponseTestClass();

            //act
            var retrieval = await httpService.Post(testRoute, testContent);

            //assert
            Assert.AreEqual(StatusCode.Error, retrieval.Status);
        }

        [TestMethod]
        public async Task Delete_When_Http_Handler_Delete_Throws_Exception_Should_Return_Connection_Error()
        {
            //arrange
            this.httpHandlerMock.Setup(handler => handler.DeleteAsync(It.IsAny<string>())).Throws(new Exception());
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";

            //act
            var result = await httpService.Delete(testRoute);

            //assert
            Assert.AreEqual(StatusCode.ConnectionProblem, result.Status);
        }

        [TestMethod]
        public async Task Delete_When_Http_Handler_Delete_Executes_Successfuly_Should_Return_Success_Status()
        {
            //arrange
            var responseMessageContent = (new ResponseModel<ServiceResponseTestClass>()
            {
                IsOperationSuccessful = true
            });

            var testResponseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseMessageContent))
            };
            this.httpHandlerMock.Setup(handler => handler.DeleteAsync(It.IsAny<string>())).ReturnsAsync(testResponseMessage);
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";

            //act
            var retrieval = await httpService.Delete(testRoute);

            //assert
            Assert.AreEqual(StatusCode.Ok, retrieval.Status);
        }

        [TestMethod]
        public async Task Delete_When_Http_Handler_Returns_Internal_Server_Error_Should_Return_Error_Status()
        {
            //arrange
            var responseMessageContent = (new ResponseModel<ServiceResponseTestClass>()
            {
                IsOperationSuccessful = false
            });

            var testResponseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseMessageContent))
            };
            this.httpHandlerMock.Setup(handler => handler.DeleteAsync(It.IsAny<string>())).ReturnsAsync(testResponseMessage);
            HttpService httpService = new HttpService(this.dependencyServiceMock.Object, this.httpHandlerMock.Object);
            string testRoute = "testRoute";

            //act
            var retrieval = await httpService.Delete(testRoute);

            //assert
            Assert.AreEqual(StatusCode.Error, retrieval.Status);
        }
    }
}