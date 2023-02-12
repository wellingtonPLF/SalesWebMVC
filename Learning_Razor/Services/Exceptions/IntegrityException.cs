namespace Learning_Razor.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message) 
        {
        }
    }
}
