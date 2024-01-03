namespace BancoDoacaoSangue.Core.Exceptions
{
    public class DoadorValidationException : Exception
    {
        public DoadorValidationException(string? message) : base(message)
        {
        }
    }
}
