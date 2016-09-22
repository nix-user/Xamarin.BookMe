using System.Net;
using System.Net.Http;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Enums;
using BookMeMobile.Model.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookMeMobile.UnitTests.BL
{
    [TestClass]
    public class AuthServiceTests
    {
        private AuthService authService;
        private Mock<IDependencyService> dependencyServiceMoq;
        private Mock<HttpClient> httpClientMoq;

        [TestInitialize]
        public void Init()
        {
            this.dependencyServiceMoq = new Mock<IDependencyService>();
            this.httpClientMoq = new Mock<HttpClient>();
            this.authService = new AuthService(this.dependencyServiceMoq.Object, this.httpClientMoq.Object);
        }

        [TestMethod]
        public void AuthAsync_WithoutHttpClient_ShouldReturnError()
        {
            var task = this.authService.AuthAsync(new LoginModel() { Login = "admin@a.a", Password = "pwd" });
            task.Wait();

            Assert.AreEqual(task.Result, StatusCode.Error);
        }
    }
}