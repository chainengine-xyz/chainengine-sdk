using ChainEngine.Actions;
using ChainEngine.Model;
using ChainEngine.Types;
using ChainEngine;
using UnityEngine;

public class ChainEngineTest : MonoBehaviour
{
    private ChainEngineSDK client;

    private void Start()
    {
        client = ChainEngineSDK.Instance();
    }

    private void OnEnable() {
        ChainEngineActions.OnPlayerLoginWithWallet += OnPlayerLoginWithWallet;
    }

    private void OnDisable() {
        ChainEngineActions.OnPlayerLoginWithWallet -= OnPlayerLoginWithWallet;
    }

    public void SetApiModeFalsy()
    {
        client.SetApiMode(false);

        Debug.Log($"SDK API Mode {client.ApiMode}");
    }

    public void SetApiModeTruly()
    {
        client.SetApiMode(true);

        Debug.Log($"SDK API Mode {client.ApiMode}");
    }
    public void SwitchApiMode()
    {
        client.SwitchApiMode();

        Debug.Log($"SDK API Mode {client.ApiMode}");
    }

    public async void CreateOrFetchPlayer()
    {
        const string walletAddress = "0xBd11AB64a50665B52c721e05a2B2342d0299601f";

        Player player = await client.CreateOrFetchPlayer(walletAddress);

        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");
    }

    public void WalletLogin()
    {
        client.WalletLogin();
    }

    public void TrustWalletLogin()
    {
        client.WalletLogin(WalletProvider.TrustWallet);
    }

    public void MetamaskLogin()
    {
        client.WalletLogin(WalletProvider.Metamask);
    }

    public void CoinbaseLogin()
    {
        client.WalletLogin(WalletProvider.Coinbase);
    }

    private void OnPlayerLoginWithWallet(Player player)
    {
        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");
    }
}
