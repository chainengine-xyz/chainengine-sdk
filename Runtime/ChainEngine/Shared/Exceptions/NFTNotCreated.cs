using System;

namespace ChainEngine.Shared.Exceptions
{
    public class NFTNotCreated : Exception
    {
        public NFTNotCreated()
        {
        }

        public NFTNotCreated(string message)
            : base(message)
        {
        }

        public NFTNotCreated(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
