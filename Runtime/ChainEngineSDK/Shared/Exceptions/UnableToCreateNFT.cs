using System;

namespace ChainEngineSDK.Shared.Exceptions
{
    public class UnableToCreateNFT : Exception
    {
        public UnableToCreateNFT()
        {
        }

        public UnableToCreateNFT(string message)
            : base(message)
        {
        }

        public UnableToCreateNFT(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}