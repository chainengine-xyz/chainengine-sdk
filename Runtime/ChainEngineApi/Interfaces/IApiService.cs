using ChainEngineSDK.ChainEngineApi.Model;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace ChainEngineSDK.ChainEngineApi.Interfaces
{
    public interface IApiService
    {
        [CanBeNull] public UniTask<string> getByWallet(string name);

        public UniTask<string> getNFTsByPlayer(string wallet);
    } 
}