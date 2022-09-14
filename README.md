# ChainEngine Unity SDK

ChainEngine SDK is an easy to use set of tools to enable game developers to quickly integrate their game to the Blockchain. Here you can find our [Documentation](https://docs.chainengine.xyz/docs/integration#unity-sdk).

## Installation

To install the ChainEngine SDK you can link it to your Unity Project using the Package Manager window, selecting the "add package from git url" option:

![](https://images2.imgbox.com/cd/a7/Z8rtraUt_o.png)

Once the SDK is imported you're ready to integrate your scripts with our SDK. You can also import our basic sample where we show the usage of the ChainEngine's SDK.

![](https://images2.imgbox.com/92/30/TppP6YHt_o.png)

To import our sample you just need to open the Package Manager window, find the ChainEngine's SDK package, and under the samples tab (which may vary due to Unity's version) and import it.

## SDK Initialization

Initializing the SDK client should be as simples as adding an object into your scene.

- drag the ChainEngine prefab into your scene.

![](https://images2.imgbox.com/6f/2f/iUacfe9I_o.png)

- fill in the Game Id property

![](https://images2.imgbox.com/c6/3f/mctim0zY_o.png)

- now you can start using the ChainEngine

```csharp
    private ChainEngineSDK client;

    private void Start()
    {
        client = ChainEngineSDK.Instance();
    }
```

*Note*: SDK's methods are asynchronous, which means that the game developer should always call them from async noted methods.

### CreateOrFetchPlayer
If you already knows the player wallet address you can use this method to create a player within ChainEngine

Parameters:
- walletAddress: Wallet address for the player wallet

Returns:
- An instance of Player.

```csharp
    public async void CreateOrFetchPlayer()
    {
        const string walletAddress = "0x92c762Bb5f8b2468965110AB2969CBc2b0D3806D";

        Player player = await client.CreateOrFetchPlayer(walletAddress);

        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");
    }
```

### Authentication
If you want to authenticate the player using his wallet you can use this method. Right now ChainEngine supports MetaMask, Coinbase, Trust Wallet and WalletConnect.

Important:
- as we don't know how long the player will take to authenticate you should subscribe to the actions `OnAuthSuccess` and `OnAuthFailure`, these actions once fired will return the authenticated player data or the authentication error message respectively.

```csharp
    private void OnEnable() {
        ChainEngineActions.OnAuthSuccess += OnAuthSuccess;
        ChainEngineActions.OnAuthFailure += OnAuthFailure;
    }

    private void OnDisable() {
        ChainEngineActions.OnAuthSuccess -= OnAuthSuccess;
        ChainEngineActions.OnAuthFailure -= OnAuthFailure;
    }

    public void WalletLogin()
    {
        client.WalletLogin();
    }

    private void OnAuthSuccess(Player player)
    {
        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");
    }

    private void OnAuthFailure(AuthError error)
    {
        Debug.Log(error);
    }
```

### GetNFT

Parameters:
- Id: The id of the NFT as issued by ChainEngine during minting.

Returns:
- An instance of NFT.

```csharp
public async void GetNFT()
{
    var nft = await client.GetNFT(id);

    Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
}
```

### GetPlayerNFTs

Returns:
- A instance of PlayerNftCollection containing a list of NFTs.

```csharp
public async void GetPlayerNFTs()
{
    var nfts = await client.GetPlayerNFTs();

    foreach (var nft in nfts.Items())
    {
        Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
    }
}
```

### SetApiMode
Sets the api mode to run on TestNet or MainNet. As TestNet is the default one, make sure to change it to MainNet on production builds.

```csharp
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
```

## Known Issues

### WebGL files not found while building in MacOS Monterrey

Unity 2020.x depends on Python 2.7 to compile WebGL games. In the most recently OS updated, Apple removed support for Python 2.7.

Follow this topic on how to solve this issue: [Unity 2020.3.28f1 webgl build failed on macos monterey 12.3](https://answers.unity.com/questions/1893841/unity-2020328f1-webgl-build-failed-on-macos-monter.html)
