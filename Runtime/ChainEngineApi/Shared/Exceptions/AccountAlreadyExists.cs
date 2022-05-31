using System;

namespace ChainEngineSDK.ChainEngineApi.Shared.Exceptions
{
    public class AccountAlreadyExists : Exception
    {
        public AccountAlreadyExists()
        {
        }

        public AccountAlreadyExists(string message)
            : base(message)
        {
        }

        public AccountAlreadyExists(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}