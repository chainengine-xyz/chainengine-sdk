using System;

namespace ChainEngineSDK.Shared.Exceptions
{
    public class PlayerNotCreated : Exception
    {
        public PlayerNotCreated()
        {
        }

        public PlayerNotCreated(string message)
            : base(message)
        {
        }

        public PlayerNotCreated(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}