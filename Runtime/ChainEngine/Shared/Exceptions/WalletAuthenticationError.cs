using System;

namespace ChainEngine.Shared.Exceptions
{
    public class WalletAuthenticationError : Exception
    {
        public WalletAuthenticationError(){}
        
        public WalletAuthenticationError(string message)
            : base(message)
        {
        }

        public WalletAuthenticationError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}