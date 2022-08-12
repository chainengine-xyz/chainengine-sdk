using ChainEngine.Shared.Exceptions;
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
        ChainEngineActions.OnWalletAuthSuccess += OnWalletAuthSuccess;
        ChainEngineActions.OnWalletAuthFailure += OnWalletAuthFailure;
    }

    private void OnDisable() {
        ChainEngineActions.OnWalletAuthSuccess -= OnWalletAuthSuccess;
        ChainEngineActions.OnWalletAuthFailure -= OnWalletAuthFailure;
    }

    public async void CreateOrFetchPlayer()
    {
        const string walletAddress = "0xb86d5053eE5260B6A4AFc2D65f485f468B635e80";

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

    public async void GetPlayerNFTs()
    {
        var nfts = await client.GetPlayerNFTs();

        foreach (var nft in nfts.Items())
        {
            Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
        }
    }

    public async void GetNFT()
    {
        var nft = await client.GetNFT("fbf72fb5-377f-418f-be76-52854d1a8e47");
        
        Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
    }
    
    public void SetTestNetMode()
    {
        client.SetTestNetMode();

        Debug.Log($"SDK API Mode {client.ApiMode}");
    }

    public void SetMainNetMode()
    {
        client.SetMainNetMode();

        Debug.Log($"SDK API Mode {client.ApiMode}");
    }

    private void OnWalletAuthSuccess(Player player)
    {
        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");
    }

    private void OnWalletAuthFailure(WalletAuthenticationError error)
    {
        Debug.Log(error);
    }
}
