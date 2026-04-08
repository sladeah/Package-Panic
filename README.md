# Package Panic

## About the Game
Package Panic is a small multiplayer game built in Unity. You play as a worker in a busy warehouse. Packages fall from the sky. You must push them into the delivery zone to score points. The player with the most points wins!

## How to Open the Project
1. Download or clone this repository to your computer.
2. Open Unity Hub.
3. Click "Add" or "Open" and select the `Package-Panic` folder.
4. When Unity opens, go to the `Scenes` folder.
5. Double-click the scene named `Warehouse_Level_A` to open the map.

## How to Test Multiplayer
To test the game, you need to run two versions at the same time:

1. In Unity, go to **File > Build and Run**. 
2. A new game window will pop up. Click the **Start Host** button on the screen.
3. Go back to the Unity Editor. Press the **Play** button at the very top.
4. In the Editor's Game view, click the **Start Client** button.
5. You will now see two players! 
6. Use **W, A, S, D** to move your player. Push a falling package onto the big delivery pad to score points.

* **Singleton Pattern:** Found in `Scripts/GameManager.cs`. This keeps track of the match time and the player scores.
* **Delegate:** Found in `Scripts/GameManager.cs` and `Scripts/ScoreUI.cs`. When a package is delivered, the delegate tells the UI to update the score on the screen.
* **Object Pool Pattern:** Found in `Scripts/PackagePool.cs`. This creates a pool of packages at the start of the game. It reuses them when they are delivered so the game runs smoothly over the network.
