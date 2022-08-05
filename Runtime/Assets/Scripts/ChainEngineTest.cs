using ChainEngineSDK.Actions;
using ChainEngineSDK.Model;
using ChainEngineSDK;
using UnityEngine;

public class ChainEngineTest : MonoBehaviour
{
    private ChainEngineClient client;

    private void Start()
    {
        client = ChainEngineClient.Instance();
    }

    private void OnEnable() {
        ChainEngineActions.OnReceivePlayerWallet += OnPlayerLoginWithWallet;
    }
    
    private void OnDisable() {
        ChainEngineActions.OnReceivePlayerWallet -= OnPlayerLoginWithWallet;
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

    public void MetamaskLogin()
    {
        client.WalletLogin();
    }

    private void OnPlayerLoginWithWallet(Player player)
    {
        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");
    }
}
