namespace BookMeMobile.Model
{
    public class ResponseModelStatusCode<T> : ResponseStatusCode
    {
        public T Result { get; set; }
    }
}