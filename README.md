# 2D Space Shooter Game (Unity)

A 2D space shooter game developed in Unity as part of a university Game Fundamentals coursework project.  
The game features player ship selection, multiple enemy types with different attack behaviours, real-time UI updates, a boss battle, and a pickup system.

## Gameplay Overview
- The player starts by selecting a ship.
- During gameplay, multiple enemy types spawn with different shooting styles and movement behaviours.
- Enemies spawn continuously until a boss appears.
- The boss fight introduces a unique enemy with a dedicated health bar.
- Defeating the boss results in a win state.
- The game ends if the player’s health reaches zero.

## Core Features
- **Ship Selection System**
  - Player chooses a ship before entering the game.
  - Selected ship prefab is instantiated at runtime.

- **Enemy Spawning System**
  - Multiple enemy prefabs with different behaviours.
  - Enemies spawn at random spawn points at fixed intervals.

- **Boss Battle System**
  - Boss spawns after a time delay.
  - Dedicated boss health bar and UI elements.
  - Background music stops when the boss appears.
  - All existing enemies are cleared on boss spawn.

- **Health & UI System**
  - Real-time player health tracking using sliders and text.
  - Pilot portrait reacts dynamically to player health:
    - Normal state
    - Injured state
    - Dead state
  - Health and score updates are reflected instantly in the UI.

- **Pickup & Scoring System**
  - Collectable items increase score or trigger UI feedback.
  - Score is tracked and displayed in real time.

- **Game States**
  - Game over screen when player dies.
  - Victory screen when the boss is defeated.

## Technical Implementation
- Implemented entirely in **C#** using Unity’s component-based architecture.
- Gameplay logic handled through MonoBehaviour scripts.
- Scene-based execution using Unity Editor.
- UI updates handled in real time using Unity UI and TextMeshPro.
- Timers and spawning logic managed using `Time.deltaTime`.

## Libraries & Unity Systems Used
The following Unity libraries and systems are used in the project:

- `UnityEngine`
  - Core engine functionality
  - GameObjects, Transforms, collision handling, instantiation

- `UnityEngine.UI`
  - UI components such as sliders and images
  - Health bars and HUD elements

- `TMPro (TextMeshPro)`
  - High-quality UI text rendering
  - Health, score, item, and boss UI text

- `UnityEngine.SceneManagement`
  - Scene transitions for win and game over states

- `System.Collections`
  - Timers and gameplay flow control

- `Animator`
  - Pilot image animation and state transitions

## How to Run the Project
1. Open **Unity Hub**
2. Click **Add project from disk**
3. Select the project root folder (contains `Assets`, `Packages`, `ProjectSettings`)
4. Open the main gameplay scene from `Assets/Scenes`
5. Click **Play** in the Unity Editor

## Screenshots
Screenshots of gameplay, UI, and boss battles are available in the `screenshots/` folder.

## Notes
- This project runs via Unity Editor scenes and does not include a standalone executable.
- Developed as a coursework project to demonstrate gameplay mechanics, UI systems, and C# scripting in Unity.
