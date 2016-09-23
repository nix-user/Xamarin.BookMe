using System.Threading.Tasks;
using BookMeMobile.Enums;
using BookMeMobile.Model.Login;

namespace BookMeMobile.BL.Abstract
{
    /// <summary>
    /// Interface for authentication logic
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authentication method
        /// </summary>
        /// <param name="loginModel">Model with user credentials</param>
        /// <returns></returns>
        Task<StatusCode> AuthAsync(LoginModel loginModel);
    }
}