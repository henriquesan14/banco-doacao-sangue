using System;

namespace BancoDoacaoSangue.Core.Exceptions
{
    public class DoadorJaExisteException : Exception
    {
        public DoadorJaExisteException(string message) : base(message)
        {
        }
    }
}
