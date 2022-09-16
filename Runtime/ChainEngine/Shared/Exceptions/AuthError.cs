using System;

namespace ChainEngine.Shared.Exceptions
{
    public class AuthError : Exception
    {
        public AuthError(){}
        
        public AuthError(string message)
            : base(message)
        {
        }

        public AuthError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}