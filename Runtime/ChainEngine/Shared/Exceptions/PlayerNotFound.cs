using System;

namespace ChainEngine.Shared.Exceptions
{
    public class PlayerNotFound : Exception
    {
        public PlayerNotFound()
        {
        }

        public PlayerNotFound(string message)
            : base(message)
        {
        }

        public PlayerNotFound(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
