using System;

namespace ChainEngine.Shared.Exceptions
{
    public class UnableToLoadPlayerInfo : Exception
    {
        public UnableToLoadPlayerInfo()
        {
        }

        public UnableToLoadPlayerInfo(string message)
            : base(message)
        {
        }

        public UnableToLoadPlayerInfo(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
