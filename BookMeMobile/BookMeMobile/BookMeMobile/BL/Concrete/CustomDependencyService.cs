using BookMeMobile.BL.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.BL.Concrete
{
    public class CustomDependencyService : IDependencyService
    {
        public T Get<T>() where T : class
        {
            return DependencyService.Get<T>();
        }
    }
}