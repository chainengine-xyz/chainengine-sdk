using System;

namespace ChainEngineSDK.Shared.Exceptions
{
    public class AccountNotFound : Exception
    {
        public AccountNotFound()
        {
        }

        public AccountNotFound(string message)
            : base(message)
        {
        }

        public AccountNotFound(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}