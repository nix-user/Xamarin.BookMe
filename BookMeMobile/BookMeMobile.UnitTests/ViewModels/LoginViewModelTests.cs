using BookMeMobile.BL.Abstract;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model.Login;
using BookMeMobile.ViewModels.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookMeMobile.UnitTests.ViewModels
{
    [TestClass]
    public class LoginViewModelTests
    {
        private Mock<INavigationService> navigationServiceMock;
        private Mock<IAuthService> accountServiceMock;

        [TestInitialize]
        public void Init()
        {
            this.navigationServiceMock = new Mock<INavigationService>();
            this.accountServiceMock = new Mock<IAuthService>();
        }

        [TestMethod]
        public void SignInCommand_Should_Show_Select_Page_If_Operation_Was_Successful()
        {
            //arrange
            this.accountServiceMock.Setup(m => m.AuthAsync(It.IsAny<LoginModel>())).ReturnsAsync(StatusCode.Ok);
            LoginViewModel loginViewModel = new LoginViewModel(this.accountServiceMock.Object, this.navigationServiceMock.Object);

            //act
            loginViewModel.SignInCommand.Execute(null);

            //assert
            this.navigationServiceMock.Verify(m => m.ShowViewModel(It.IsAny<LoginViewModel>()), Times.Once); //replace with call to select page
        }
    }
}