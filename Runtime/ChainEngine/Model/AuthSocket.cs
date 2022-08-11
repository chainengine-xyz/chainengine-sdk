using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace ChainEngine.Model
{
    [Preserve]
    public class AuthSocket
    {
        [JsonProperty("error")] [CanBeNull] public string Error;

        [JsonProperty("walletAddress")] [CanBeNull] public string WalletAddress;
    }
}
