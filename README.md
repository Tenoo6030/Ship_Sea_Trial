# Ship Sea Trial

A sailing ship simulation created in Unity as a recruitment programming assignment.

The project demonstrates how wind, sail deployment, sail rotation, and water resistance influence ship movement without using Unity's built-in physics or collision systems.

## Features

- A sailing ship with one controllable sail.
- Sail deployment ranging from 0% to 100%.
- Sail rotation limited to ±45°.
- Custom movement calculations based on wind and water resistance.
- Gradual acceleration and deceleration.
- Two UI labels displaying sail deployment and rotation.
- Keyboard controls using Unity's Input System.
- Event-driven communication using C# delegates.

## Technologies

- **Unity:** 6000.5.11f1
- **Language:** C#
- **Input:** Unity Input System
- **UI:** UI Toolkit
- **Platform:** Windows
- **Scripting Backend:** IL2CPP
- **Managed Stripping Level:** High
- **IL2CPP Code Generation:** Faster Runtime

## Project Architecture

The project is divided into three main scripts, each responsible for a specific part of the simulation.

### SailController

Handles sail controls and manages sail deployment and rotation.

Responsibilities:
- Processes input from the player.
- Limits sail deployment to 0–100%.
- Limits sail rotation to ±45°.
- Updates sail parameters.

### ShipController

Responsible for calculating the ship's movement.

Responsibilities:
- Calculates wind force acting on the sail.
- Calculates the force acting along the ship's forward direction.
- Applies quadratic water resistance.
- Calculates acceleration and updates speed.
- Updates the ship's position without Rigidbody physics.
- Uses delegates to react to sail parameter changes.

### ShipHUD

Responsible for displaying sail information to the player.

Responsibilities:
- Displays the current sail deployment percentage.
- Displays the current sail rotation angle.
- Uses delegates to update UI values when sail parameters change.

### Communication Between Components

The project uses C# delegates (`Action<float>`) to communicate changes in sail parameters.

This allows the movement and UI components to react to sail adjustments without requiring them to check the sail values every frame.

## Movement Simulation

The ship uses a simplified mathematical movement model implemented in C#.

### Wind Force

The wind vector is projected onto the sail's direction using `Vector3.Dot`.

The resulting sail force depends on:
- Wind direction and magnitude.
- Sail orientation.
- Sail deployment.

The wind contribution is calculated using a quadratic relationship with the projected wind component.

### Water Resistance

Water resistance is calculated using a quadratic drag model:

`drag = dragCoefficient * Abs(speed) * speed`

The resistance acts against the ship's movement and increases with speed.

### Acceleration and Movement

The net force is calculated by subtracting water resistance from the ship's driving force.

Acceleration is calculated using Newton's second law:

`acceleration = netForce / mass`

The acceleration is projected onto the ship's forward direction. Speed and position are then updated using `Time.deltaTime`.

When the sail is fully retracted, the ship gradually slows down due to water resistance. Very low speeds are set to zero to prevent unnecessary residual movement.

### Simulation Limitations

The simulation intentionally uses a simplified model:

- The ship moves along its forward axis.
- Sideways drift is not simulated.
- Ship turning is not simulated.
- Aerodynamic lift and realistic fluid dynamics are not calculated.
- Unity's built-in physics and collision systems are not used.

## Controls

The sail can be controlled using WASD or the arrow keys.

| Key | Action |
|---|---|
| W / Up Arrow | Unfurl the sail |
| S / Down Arrow | Furl the sail |
| A / Left Arrow | Rotate the sail left |
| D / Right Arrow | Rotate the sail right |

## How to Run

1. Clone or download the repository.
2. Open the project using Unity Hub.
3. Select Unity version **6000.5.11f1**.
4. Open the main project scene.
5. Enter Play Mode.
6. Use WASD or the arrow keys to control the sail.

## Demonstration

The demonstration video shows sail deployment, rotation, acceleration, and deceleration caused by water resistance.

[Watch the demonstration](Documentation/Ship_Demonstration.mp4)

## Project Status

Completed as a recruitment programming assignment.
