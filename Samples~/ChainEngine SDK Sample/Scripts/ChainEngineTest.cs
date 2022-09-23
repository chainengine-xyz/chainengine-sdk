using ChainEngine.Shared.Exceptions;
using ChainEngine.Actions;
using ChainEngine.Model;
using ChainEngine.Types;
using UnityEngine.UI;
using ChainEngine;
using UnityEngine;
using System;

public class ChainEngineTest : MonoBehaviour
{
    private const string WALLET_ADDRESS = "0x7736C454D88B153E44cB5752209131705A804E65";
    private const string NFT_ID = "c894fcaa-b7e4-4ee0-bf45-9980d3e358d2";
    
    public Button authButton;
    public Button trustWalletButton;
    public Button metamaskLoginButton;
    public Button coinbaseLoginButton;
    public Button logoutButton;
    public Button getPlayerNftsButton;
    public Button getNftButton;
    public Button transferNftButton;

    private bool? isPlayerAuthenticated = null;
    private ChainEngineSDK client;

    private void Start()
    {
        client = ChainEngineSDK.Instance();
    }

    private void Update()
    {
        ButtonsEnablerHandler();
    }

    private void OnEnable() {
        ChainEngineActions.OnAuthSuccess += OnAuthSuccess;
        ChainEngineActions.OnAuthFailure += OnAuthFailure;
    }

    private void OnDisable() {
        ChainEngineActions.OnAuthSuccess -= OnAuthSuccess;
        ChainEngineActions.OnAuthFailure -= OnAuthFailure;
    }

    public async void CreateOrFetchPlayer()
    {
        await client.CreateOrFetchPlayer(WALLET_ADDRESS);

        Debug.Log($"Player Id: {client.Player?.Id}\n" +
                  $"Wallet Address: {client.Player?.WalletAddress}");
    }

    public void PlayerAuthentication()
    {
        client.PlayerAuthentication();
    }

    public void TrustWalletLogin()
    {
        client.PlayerAuthentication(WalletProvider.TrustWallet);
    }

    public void MetamaskLogin()
    {
        client.PlayerAuthentication(WalletProvider.Metamask);
    }

    public void CoinbaseLogin()
    {
        client.PlayerAuthentication(WalletProvider.Coinbase);
    }

    public void Logout()
    {
        client.PlayerLogout();
    }

    public async void GetPlayerNFTs()
    {
        var nfts = await client.GetPlayerNFTs();

        foreach (var nft in nfts.Items())
        {
            Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
            Debug.Log($"Image: {nft.Metadata.Image}");
        }
    }

    public async void GetNFT()
    {
        var nft = await client.GetNFT(NFT_ID);
        
        Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
        Debug.Log($"Image: {nft.Metadata.Image}");
    }

    public void TransferNft()
    {
        client.TransferNft(WALLET_ADDRESS, NFT_ID, 1);
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

    private void OnAuthSuccess(Player player)
    {
        Debug.Log($"Player Id: {player?.Id}\n" +
                  $"Wallet Address: {player?.WalletAddress}\n" +
                  $"Token: {player?.Token}\n");
    }

    private void OnAuthFailure(AuthError error)
    {
        Debug.Log(error);
    }

    private void ButtonsEnablerHandler()
    {
        if (isPlayerAuthenticated == client.IsPlayerAuthenticated) return;
        isPlayerAuthenticated = client.IsPlayerAuthenticated;
        
        authButton.enabled = !(bool)isPlayerAuthenticated;
        trustWalletButton.enabled = !(bool)isPlayerAuthenticated;
        metamaskLoginButton.enabled = !(bool)isPlayerAuthenticated;
        coinbaseLoginButton.enabled = !(bool)isPlayerAuthenticated;
        logoutButton.enabled = (bool)isPlayerAuthenticated;
        getPlayerNftsButton.enabled = (bool)isPlayerAuthenticated;
        getNftButton.enabled = (bool)isPlayerAuthenticated;
        transferNftButton.enabled = (bool)isPlayerAuthenticated;
    }
}
