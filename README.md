
# Super Intern

**Super Intern** is a Unity-based 3D action-adventure game where players explore a dynamic internship environment, gaining superpowers to overcome challenges, solve puzzles, and avoid hazards. Your goal: complete all tasks to become the ultimate super intern!

---

##  How to Play

1. **Starting the Game**:
   Open the Unity project and launch the `Menu` scene under `Assets/Scenes/Menu`.

2. **Controls**:

   * Movement: `W`, `A`, `S`, `D` or arrow keys
   * Run: `Left Shift`
   * Jump: `Space`
   * Interact: `E` (used to collect powers, operate terminals, etc.)

3. **Gameplay Progression**:

   * Explore rooms, collect badges to gain superpowers.
   * Use high jumps to reach new areas or activate elevators via computers.
   * Follow visual cues like **pink walls** or interactive **terminals**.
   * Acquire new abilities:

     * Level 1: High Jump
     * Level 2: Speed Boost
     * Level 3: Wall Phasing
     * Level 4: Slow Enemies
   * Solve challenges and defeat enemies using your skills.
   * Monitor health and lives — lose all and restart from the beginning.

4. **Level 2 Special Challenge**:

   * Collect 3 hidden **energy bars** to unlock the door.
   * Use phasing and speed skills to avoid hazards (e.g. giant rolling ball).
   * Watch out — falling or losing health may cost you a life.

---

## Features & Technical Implementation

| Feature                             | Description                                                                                             |
| ----------------------------------- | ------------------------------------------------------------------------------------------------------- |
| ✅ **3D Game World**                 | Built entirely in 3D using Unity; no 2D/side-scroller components.                                       |
| ✅ **Third-Person View**             | Player character is always visible; no first-person camera used.                                        |
| ✅ **Player-Controlled Character**   | Fully rigged humanoid character with animated reactions to input (root motion, jumping, running, etc.). |
| ✅ **Game Feel**                     | Rich feedback through audio cues, camera motion, UI prompts, and visual effects.                        |
| ✅ **Interactive Environments**      | Elevators, terminals, doors all interactable; key to progression.                                       |
| ✅ **Rigidbody Physics Simulation**  | Physics-driven objects and collisions used throughout levels.                                           |
| ✅ **Real-Time AI with Pathfinding** | Enemies use state machines and dynamic steering/path planning.                                          |
| ✅ **Meaningful Player Choices**     | Players can choose how and when to use powers (with cooldowns) to solve problems.                       |
| ✅ **Checkpoints and Lives System**  | Player has 3 lives with respawn checkpoints.                                                            |
| ✅ **UI Menus**                      | Start screen, pause menu, and game over screens implemented.                                            |
| ✅ **Controller Support**            | PS4 controller (recommended) supported for analog input.                                                |

---

* Developed by Team BlueCrab
* Project for Video Game Design
