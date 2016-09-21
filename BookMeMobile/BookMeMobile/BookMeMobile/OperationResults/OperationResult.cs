namespace BookMeMobile.OperationResults
{
    public class BaseOperationResult<T> : BaseOperationResult
    {
        public T Result { get; set; }
    }
}