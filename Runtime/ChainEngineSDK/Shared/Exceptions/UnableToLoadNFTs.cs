using System;

namespace ChainEngineSDK.Shared.Exceptions
{
    public class UnableToLoadNFTs : Exception
    {
        public UnableToLoadNFTs()
        {
        }

        public UnableToLoadNFTs(string message)
            : base(message)
        {
        }

        public UnableToLoadNFTs(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}