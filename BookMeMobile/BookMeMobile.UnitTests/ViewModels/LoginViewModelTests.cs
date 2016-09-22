using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xamarin.Forms;

namespace BookMeMobile.UnitTests.ViewModels
{
    [TestClass]
    public class LoginViewModelTests
    {
        private Mock<INavigationService> navigationServiceMock;
        private Mock<IAccountService> accountServiceMock;

        [TestInitialize]
        public void Init()
        {
            this.navigationServiceMock = new Mock<INavigationService>();
            this.accountServiceMock = new Mock<IAccountService>();
        }

        [TestMethod]
        public void SignInCommand_Should_Show_Select_Page_If_Operation_Was_Successful()
        {
            //arrange
            this.accountServiceMock.Setup(m => m.GetToken(It.IsAny<User>())).ReturnsAsync(StatusCode.Ok);
            LoginViewModel loginViewModel = new LoginViewModel(this.accountServiceMock.Object, this.navigationServiceMock.Object);

            //act
            loginViewModel.SignInCommand.Execute(null);

            //assert
            this.navigationServiceMock.Verify(m => m.ShowViewModel(It.IsAny<LoginViewModel>()), Times.Once);
        }
    }
}