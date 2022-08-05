# ChainEngine SDK Unity

ChainEngine SDK is a easy to use set of tools to enable game developers to quickly integrate their game to the Blockchain.

## Installation

To install the API you can download this repository anywhere in your computer and link it to your Unity Project using the Package Manager window, selecting the "add from disk" option or "add package from git url":

![](https://images2.imgbox.com/cd/a7/Z8rtraUt_o.png)

Once the SDK is imported you're ready to integrate your script to the SDK.

![](https://i.imgur.com/q5DGkaq.png)

## SDK Initialization

Initializing the SDK should be as simples as adding an object into your scene.

- drag the ChainEngineSDK prefab into your scene.

![](https://images2.imgbox.com/6f/2f/iUacfe9I_o.png)

- fill in the Game Id property

![](https://images2.imgbox.com/c6/3f/mctim0zY_o.png)

- now you can start using the ChainEngineSDK

```csharp
    private ChainEngineClient client;

    private void Start()
    {
        client = ChainEngineClient.Instance();
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

### WalletLogin
If you want to authenticate the player using his wallet you can use this method. Right now ChainEngineSDK supports MetaMask, Coinbase and WalletConnect.

Returns:
- void

Important:
- as we don't know how long the player will take to authenticate you should subscribe to the action `ChainEngineActions.OnReceivePlayerWallet`, this action once fired will return the authenticated player data.

```csharp
    private void OnEnable() {
        ChainEngineActions.OnReceivePlayerWallet += OnPlayerLoginWithWallet;
    }
    
    private void OnDisable() {
        ChainEngineActions.OnReceivePlayerWallet -= OnPlayerLoginWithWallet;
    }
    
    public void WalletLogin()
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
```

### GetNFT

Parameters:
- Id: The id of the NFT as issued by ChainEngine during minting.

Returns:
- An instance of NFT.

```csharp
var nft = await client.GetNFT(id)
```

### GetPlayerNFTs

Returns:
- A instance of PlayerNftCollection containing a list of NFTs.

```csharp
var nfts = await client.GetPlayerNFTs()
```

## Todo

- [ ] Add authentication section to documentation

## Known Issues

### WebGL files not found while building in MacOS Monterrey

Unity 2020.x depends on Python 2.7 to compile WebGL games. In the most recently OS updated, Apple removed support for Python 2.7.

Follow this topic on how to solve this issue: [Unity 2020.3.28f1 webgl build failed on macos monterey 12.3](https://answers.unity.com/questions/1893841/unity-2020328f1-webgl-build-failed-on-macos-monter.html)
