namespace BookMeMobile.BL.Abstract
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}