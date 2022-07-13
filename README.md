# ChainEngine SDK Unity

ChainEngine SDK is a easy to use set of tools to enable game developers to quikly integrate their game to the Blockchain.

## Instalation

To install the API you should download this repositóry anywhere in your computer and link it to your Unity Project using the Package Manager window, selecting the "add from disk" option.

Once the SDK is importated you're ready to integrate your script to the SDK.

![](https://i.imgur.com/q5DGkaq.png)


## SDK Initialization

Initializing the SDK should be as simples as calling it's instatiator method.

Parameters:
- accountId: Id of the Account
- gameId: Id of the Game

```var client = new ChainEngineClient(accountId, gameId)```

After calling this method you should be able to interact with the SDK.

*Note*: SDK's methods are asynchronous, which means that the game developer should always call them from async noted methods.

### CreatePlayer

Parameters:
- walletAddress: Wallet address for the player wallet

Returns:
- An instance of Player. 

```var player = await client.CreatePlayer(walletAddress)```

### SetPlayerKey

Note: Calling this method is required to perform player-related actions and allows the game developer to identify which player is connected to the SDK instance.

Parameters:
- apikey: Apikey generated during player creation

Returns: Nothing

```client.SetPlayerKey(apikey)```

### GetPlayerInfo

Returns:
- An instance of Player.

```var player = await client.GetPlayerInfo()```

### GetNFT

Parameters:
- Id: The id of the NFT as issued by ChainEngine during minting.

Returns:
- An instance of NFT.

```var nft = await client.GetNFT(id)```

### GetPlayerNFTs

Returns:
- A instance of PlayerNftCollection containing a list of NFTs.

```var nfts = await client.GetPlayerNFTs()```

## Todo

- [ ] Add authentication section to documentation
- [ ] Add support for wallet selection in WebGL
- [ ] Add support for Unity 2021.x

## Known Issues

### WebGL files not found while building in MacOS Monterrey

Unity 2020.x depends on Python 2.7 to compile WebGL games. In the most recently OS updated, Apple removed support for Python 2.7.

Follow this topic on how to solve this issue: [Unity 2020.3.28f1 webgl build failed on macos monterey 12.3](https://answers.unity.com/questions/1893841/unity-2020328f1-webgl-build-failed-on-macos-monter.html)