using ChainEngine.Shared.Exceptions;
using ChainEngine.Model;
using System;

namespace ChainEngine.Actions
{
    public static class ChainEngineActions
    {
        public static Action<AuthError> OnAuthFailure;
        public static Action<Player> OnAuthSuccess;
    }
}
