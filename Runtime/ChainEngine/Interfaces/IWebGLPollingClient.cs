using System;

namespace ChainEngine.Interfaces
{
    public interface IWebGLPollingClient
    {
        public void PollingOnUnityThread(string eventName, Action<string> callback);

        public void Off(string eventName);
    }
}