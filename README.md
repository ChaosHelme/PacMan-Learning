# Pac-Man-Learning

Welcome to the **Pac-Man-Learning** repository!  
This project is a modern, testable, and extensible implementation of the classic Pac-Man game in C#, designed for the command line and built to help you learn:

- **C#**
- **Clean Code**
- **SOLID principles**
- **The Entity Component System (ECS) pattern**
- **Unit and integration testing**
- **Rich console UI with Spectre.Console**

---

## 🚀 Project Purpose

This repository is intended as a **learning resource** for developers interested in:
- Writing maintainable C# code
- Applying SOLID and Clean Code principles in real-world scenarios
- Understanding and implementing the ECS pattern
- Building testable and modular applications
- Experimenting with console rendering and input handling
- Leveraging Spectre.Console for beautiful, modern console UIs

---

## 🎮 Features

- **Classic Pac-Man gameplay**: Move, collect dots, avoid ghosts, win or lose!
- **Entity Component System architecture**: Decouples data, behavior, and identity for maximum flexibility and testability
- **Clean, SOLID codebase**: Each class and method has a clear, single responsibility
- **Two rendering modes**: Emoji (colorful, modern) and ASCII (maximum compatibility)
- **Command-line options**: Choose your render mode at startup
- **Comprehensive tests**: Unit and integration tests using NUnit and FluentAssertions
- **Easy extensibility**: Add new features, systems, or components with minimal changes

---

## 🧑‍💻 Learning Outcomes

By exploring this project, you will:

- **Understand the ECS pattern**: Learn how to separate entities, components, and systems for scalable game (or app) design
- **Apply SOLID principles**: See how Single Responsibility, Open/Closed, and other SOLID principles work in practice
- **Write and run tests**: Discover how to write unit, integration, and E2E tests for a console application
- **Handle command-line arguments**: Learn how to make your apps configurable and user-friendly
- **Work with console input/output**: See how to create interactive C# console applications
- **Use Spectre.Console**: Build visually appealing console UIs with modern .NET tools

---

## 🖥️ Console UI Powered by Spectre.Console

This project uses [Spectre.Console](https://spectreconsole.net) to create a beautiful, modern, and interactive console user interface.

**Why Spectre.Console?**
- Enables rich text formatting (colors, styles, emojis, tables, panels, and more).
- Provides widgets like tables, trees, progress bars, charts, and custom panels for a delightful console experience.
- Makes it easy to display game states, scores, and messages in a visually appealing way.
- Supports both simple ASCII and advanced emoji/Unicode rendering for cross-platform compatibility.

**Example Features Used:**
- Styled text and colored output for game messages and status.
- Panels and markup for the game over screen and UI elements.
- Emoji and ASCII rendering modes, switchable via the `--render-mode` command-line option.

**Getting Started with Spectre.Console:**

```
dotnet add package Spectre.Console
```

Learn more:
- [Spectre.Console Documentation](https://spectreconsole.net)
- [Spectre.Console GitHub](https://github.com/spectreconsole/spectre.console)
- [Spectre.Console Examples](https://spectreconsole.net/examples)

## 🏗️ Project Structure

```mermaid
graph TD
    A[PacMan.ECS]
    B[PacMan.Game]
    C[PacMan.Tests]
    B-->A
    C-->A
    C-->B
```

### PacMan.ECS

### PacMan.Game

### PacMan.Tests

---

## 🗺️ Maze Configuration with using a text simple text file

The structure and layout of the Pac-Man maze in this project are defined in an external, human-readable file called `maze.txt`. This file allows you to easily design, modify, or extend the maze without changing any code.

### How `maze.txt` Works

Each line in `maze.txt` represents a row in the maze. Each character in a line represents a cell at a specific (x, y) coordinate in the grid. The file is parsed at game startup, and the contents are used to generate the maze entities and features via the ECS.

#### **Legend: Maze Symbols**

| Symbol  | Meaning                                                        |
|---------|----------------------------------------------------------------|
| `#`     | Wall                                                           |
| `.`     | Dot (collectable)                                              |
| `@`     | Warp tunnel (teleport from one `@` to another in the same row) |
| (space) | Empty cell (walkable, no dot)                                  |
| other   | (Reserved for future features, e.g. power pellets)             |

#### **Example `maze.txt`**

See [maze.txt](src/PacMan.Game/maze.txt)

#### **How the Maze is Used**

- **Walls** (`#`): Entities with a `WallComponent` and a `PositionComponent` are created at these positions. These block movement.
- **Dots** (`.`): Entities with a `DotComponent` and a `PositionComponent` are created at these positions. These are collectable by the player.
- **Warp Tunnels** (`@`): When the player or a ghost moves onto a warp tunnel, they are instantly moved to the matching `@` in the same row, allowing classic Pac-Man wrap-around movement.
- **Empty Spaces**: Open paths for movement, no entity is created.

#### **Extending the Maze**

You can edit `maze.txt` to:
- Change the maze shape and size.
- Add or remove dots and walls.
- Place warp tunnels (`@`) wherever you want wrap-around movement.
- Reserve other symbols for future features (e.g., power pellets, fruits, ghost house).

#### **How the File is Loaded**

At game startup, the maze file is read by the `MazeConfigurationLoader`, which parses each character and records the positions of walls, dots, and warps. These positions are then used to create the corresponding entities in the ECS world.

---

## ⚙️ Continuous Integration with GitHub Actions

This project uses **GitHub Actions** for automated Continuous Integration (CI).  
Every push or pull request to the `main` branch will automatically:

1. **Check out the code**
2. **Set up .NET 9**
3. **Restore dependencies**
4. **Build the project**
5. **Run all tests**

This helps ensure that every change is automatically built and tested, keeping the codebase healthy and reliable.

### 📋 Workflow Overview

```mermaid
timeline
    title .NET CI Workflow
    Push or PR to main : Triggered by push or pull request to main branch
    Checkout code : actions/checkout@v4
    Setup .NET 9 : actions/setup-dotnet@v4
    Restore dependencies : dotnet restore src/
    Build : dotnet build src/ --no-restore
    Test : dotnet test src/ --no-build --verbosity normal
    CI Status : Reports success or failure to GitHub
```

### 📄 Workflow File

The workflow is defined in [dotnet.yaml](/.github/workflows/dotnet.yml)

---

## 🕹️ How to Run

1. **Clone the repository:**
    ```
    git clone https://github.com/ChaosHelme/PacMan-Learning.git
    cd pacman-learning
    ```

2. **Build and run the game:**
    ```
    dotnet run -- --render-mode emoji
    ```
    - Use `--render-mode ascii` for ASCII rendering (default if not specified).

3. **Controls:**
    - Use arrow keys or WASD to move
    - Press `Q` to quit

---

## 🧪 How to Test

1. **Run all tests:**
    ```
    dotnet test
    ```

    - The project includes unit and integration tests using [NUnit](https://nunit.org/) and [FluentAssertions](https://fluentassertions.com/).

---

## 🌱 Suggestions for Extensions & New Features

This project is a great starting point for learning, but there are many ways to extend it—both to more closely match the original Pac-Man gameplay and to explore advanced ECS concepts. Here are some ideas for further development:

---

### 🕹️ Classic Gameplay Features

- **Power Pellets / Energizers**  
  Add large dots ("power pellets") that let Pac-Man eat ghosts for a short time, as in the original game.
- **Ghost AI Personalities**  
  Implement unique movement patterns for each ghost (Blinky, Pinky, Inky, Clyde) to mimic their original behaviors.
- **Bonus Fruits & Items**  
  Occasionally spawn bonus items (fruits) for extra points.
- **Warp Tunnels**  
  Let Pac-Man and ghosts warp around the sides of the maze.
- **Multiple Levels & Increasing Difficulty**  
  Advance to new mazes or increase ghost speed as the player progresses.
- **Cutscenes/Intermissions**  
  Add short animations between levels for fun and authenticity.
- **High Score Table**  
  Track and display high scores.
- **Sound Effects**  
  Add simple sound cues for eating dots, power pellets, ghosts, and losing lives.

---

### 🛠️ Additional ECS & Advanced Features

- **Component Pooling**  
  Implement a pool for frequently created/destroyed components to improve performance and reduce garbage collection.
- **Event System**  
  Add an event or messaging system for decoupled communication between systems (e.g., "Pac-Man ate power pellet" event).
- **System Scheduling/Prioritization**  
  Allow systems to run in a specific order or only when needed.
- **Entity Queries**  
  Support more flexible queries (e.g., all entities with Position and Velocity, but without Stunned).
- **Prefab/Blueprint System**  
  Define reusable templates for entities (e.g., different ghost types or maze layouts).
- **Serialization & Save/Load**  
  Save and restore game state (entities and components) for persistence or debugging.
- **Inspector/Debug Console**  
  Add a debug view to inspect entities, components, and systems at runtime.
- **Component Removal Events**  
  Allow systems to react when components are removed from entities.

---

### 💡 Other Learning and Experimentation Ideas

- **Multithreaded Systems**  
  Explore running independent systems in parallel for performance.
- **Dependency Injection**  
  Use DI to further decouple systems and improve testability.
- **UI Improvements**  
  Add menus, pause screens, or settings using Spectre.Console's advanced widgets.
- **Port to GUI**  
  Try porting the ECS core to a GUI framework (like Avalonia, WinForms, or Unity) to see how the architecture scales.
- **AI or Bot Player**  
  Implement a simple AI to play Pac-Man or control ghosts for testing.

---

These extensions will not only make your Pac-Man game more complete and fun, but will also deepen your understanding of C#, ECS, and clean architecture.  
Feel free to [contribute](#-contributing) your own ideas or improvements!

---


## 📚 Further Reading

- [Game Programming Patterns: ECS](https://www.simplilearn.com/entity-component-system-introductory-guide-article)
- [SOLID Principles in C#](https://scotch.io/bar-talk/s-o-l-i-d-the-first-five-principles-of-object-oriented-design)
- [Clean Code by Robert C. Martin](https://www.oreilly.com/library/view/clean-code/9780136083238/)
- [Spectre Console Documentation](https://spectreconsole.net/)
- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions Documentation](https://fluentassertions.com/)

---

## 🤝 Contributing

Contributions, suggestions, and questions are welcome!  
Feel free to open issues or pull requests to help improve this learning resource.

---

## 📜 License

This project is licensed under the MIT License.  
See [LICENSE](LICENSE) for details.

---

**Happy Learning and Coding!**
