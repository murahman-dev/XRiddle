# XRiddle

An Augmented Reality project exploring the coexistence of AR Foundation and Vuforia's Image Tracking API within a single Unity application through controlled lifecycle isolation.

## About

XRiddle is a two-mode AR experience where image targets trigger puzzle content and surface detection handles world placement. The project was built to investigate whether AR Foundation's plane detection and Vuforia's image tracking can run together in one app without conflicts, and how to manage the lifecycle of both frameworks cleanly.

XRiddle began as the AR interaction layer I built for Labyrinth of Echoes, a team immersive installation project at Hochschule Darmstadt. After the semester ended, I stripped the installation-specific content, cleaned and refactored the code, and released the interaction layer as this standalone project.

Target images were sourced from [Unsplash](https://unsplash.com/).

## How It Works

- AR Foundation handles plane detection for placing objects on real-world surfaces
- Vuforia handles image tracking for recognizing printed targets and displaying puzzle content
- ARSession stays active throughout, while VuforiaBehaviour, ARPlaneManager, and the placement raycast are toggled between modes
- Canvas reparenting prevents Vuforia's DefaultObserverEventHandler from disabling tracked UI when a target is lost
- Only one tracked canvas is visible at a time, so scanning a new target swaps content instead of stacking it
- Puzzle stages are defined in a word list, so stage count and required word length derive from the data

## Built With

- Unity 3D
- C#
- AR Foundation
- Vuforia
- Visual Studio

## Platforms

- Android
- iOS-ready architecture

## Getting Started

### Prerequisites

- Unity (version 2022.3.58f1)
- Vuforia Engine 10.29.6, download the package directly from [developer.vuforia.com](https://developer.vuforia.com/downloads/sdk) and place it in the project's `Packages` folder, since it is referenced there as a local package and is not included in this repository
- Vuforia license key (free tier available at [developer.vuforia.com](https://developer.vuforia.com/))
- Safe Area Helper from [Unity Asset Store](https://assetstore.unity.com/packages/tools/gui/safe-area-helper-130488), free but not included in this repository, install it into `Assets/CrystalFramework`
- Android device with ARCore support
- Git

### Setup

1. Clone the repository
   ```
   git clone https://github.com/murahman-dev/XRiddle.git
   ```
2. Download Vuforia Engine 10.29.6 and add it to the project's `Packages` folder
3. Import Safe Area Helper from the Asset Store
4. Open the project in Unity
5. Add your Vuforia license key in the Vuforia Configuration settings
6. Build and deploy to an Android device

## Walkthrough

- [Video Walkthrough](https://www.youtube.com/watch?v=hQJ7_7wAVwc)

## Download

A playable build is available on [itch.io](https://mrahman.itch.io/xriddle).

## License

This project is open-source under the [MIT License](LICENSE).

See [NOTICE](NOTICE.md) for the full third-party attribution.

## Contact

Mesbah Ur Rahman
- Email: mesbah@murahman.com
- LinkedIn: [linkedin.com/in/mesbah-ur-rahman997](https://www.linkedin.com/in/mesbah-ur-rahman997)