# Hackathon: AI Reality

A first-person Unity prototype where the world is a simulation full of glitches, and the player's main tool is a **hologram terminal** that lets them type "bug commands" to bend the rules of reality: run faster, slow down time, float, blink through walls, or turn invisible.

You can't use a command until you've found it. Malfunctioning NPC bots scattered around the world each carry one bug, and interacting with a bot teaches you its command.

## Core Idea

1. **Find a glitchy bot.** Each bot behaves strangely in its own way (running in circles, teleporting and spinning, jumping in low gravity, flickering in and out of existence).
2. **Interact with it** (`E`) to discover the bug it carries. The command is revealed on screen.
3. **Open the terminal** (`T`) and type the command to use that bug yourself.
4. **Manage your energy.** Every command costs energy, and the more drained you are, the more likely the terminal is to malfunction and fire a random bug you already know.

## Controls

| Input | Action |
|---|---|
| `W` `A` `S` `D` | Move |
| Mouse | Look / turn |
| `E` | Interact (bots, gun pickup) |
| `T` | Toggle the hologram terminal |
| Typing (terminal open) | Enter a command |
| Left click | Shoot (after picking up the gun) |

## Bug Commands

| Command | Energy cost | Effect |
|---|---|---|
| `/blink` | 15 | Teleports the player forward. Passes through thin walls, stops at thick ones |
| `/speed` | 20 | Movement speed x1.5 for the duration |
| `/lowg` | 25 | Lowers global gravity for the duration |
| `/slow` | 30 | Slows time (`Time.timeScale`) for the duration |
| `/invis` | 40 | Makes the player mostly transparent for the duration |
| `/dodge` | 50 | Defined and unlockable, effect not implemented yet |
| `/split` | 30 | Defined and unlockable, effect not implemented yet |

Effects last 5 seconds by default (configurable in the Inspector).

### Energy and malfunctions

- Max energy is 100 and regenerates at 10 per second.
- Not enough energy: the terminal shows an error and nothing happens.
- Typing a command you haven't discovered shows `UNKNOWN COMMAND`.
- Malfunction chance depends on current energy:

| Energy | Malfunction chance |
|---|---|
| Above 80 | 0% |
| 30 to 80 | 30% |
| Below 30 | 50% |

A malfunction triggers a random bug from the ones you have already discovered, so the game never springs unknown mechanics on you.

## The Bots

| Script | Behavior |
|---|---|
| `SpeedBot.cs` (`CircleRunningBot`) | Runs in a circle around its spawn point |
| `BlinkBot.cs` (`InstantBlinkTurnBot`) | Teleports forward, waits, then instantly turns 180 degrees, repeating |
| `LowGBot.cs` (`LowGravityJumpBot`) | Jumps floatily at intervals under its own low gravity |
| `InvisBot.cs` | Toggles a target object between visible and invisible on a timer |
| `NPCChase.cs` | Simple NPC that walks toward a target |

Every discoverable bot exposes a `myBugID` (must match the command's bug ID in the terminal) and a `revealedCommandText` (the text shown to the player) in the Inspector.

## Project Structure

```
Assets/
├── Scenes/
│   ├── MainScene.unity        # Main game scene
│   └── SampleScene.unity
├── Scripts/
│   ├── CLI.cs                 # HologramTerminal: typing, commands, malfunctions
│   ├── BugProgress.cs         # Static store of discovered bugs
│   ├── EnergySystem.cs        # Energy pool and regeneration
│   ├── EnergyUI.cs            # Energy display
│   ├── PlayerControler.cs     # Movement and look
│   ├── Shooting.cs            # Raycast shooting
│   ├── GunPickup.cs           # Pickup that enables shooting
│   ├── InteractPromptManager.cs / InteractableRegistry.cs   # "Press E" prompt
│   ├── SpeedBot.cs, BlinkBot.cs, LowGBot.cs, InvisBot.cs, NPCChase.cs
│   └── Animation Controling Scripts/DoorTriggerer.cs
├── CameraControler.cs         # First / third person camera
├── InputSystem_Actions.inputactions
└── Prefabs, Materials, Settings, ...
```

## Getting Started

### Requirements

- **Unity 6000.4.10f1** (see `ProjectSettings/ProjectVersion.txt`)
- Universal Render Pipeline 17.4.0
- Input System 1.19.0 (the project uses the new Input System)

### Run it

```bash
git clone https://github.com/RudarLaki/Hackathon-AI-Reality.git
cd Hackathon-AI-Reality
git checkout dimitrije-progress
```

1. Open the folder in Unity Hub with the editor version above.
2. Open `Assets/Scenes/MainScene.unity`.
3. Press **Play**.

## Known Limitations

This is a hackathon prototype, so some parts are unfinished:

- `/dodge` and `/split` can be unlocked but do not do anything yet.
- Shooting currently only logs what the ray hits; there is no damage system.
- Bug discovery progress (`BugProgress`) is static and resets every time the game restarts.

## Credits

Third-party assets used in the project:

- Julien Tonsuso
- Modular Lowpoly Streets (Free)
- SimplePoly City: Low Poly Assets
- Simple Town Lite
- Studio Horizon
- Supercyan Character Pack and Free Forest samples
- Vegetation Spawner
- ithappy

Check each asset's own license before redistributing.

## Authors

Built at a hackathon by [RudarLaki](https://github.com/RudarLaki) and [Dimitrije Stoilkovic](https://github.com/Dimitrije-Stoilkovic).
