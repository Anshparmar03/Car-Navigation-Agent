# CarNavigationScene Setup Guide for Unity ML-Agents Car Navigation Project

This guide explains how to create the `CarNavigationScene.unity` scene in Unity for the Car Navigation Agent project using ML-Agents. The scene includes a car with a `RayPerceptionSensorComponent3D`, a track with obstacles, and 6 sequential checkpoints, configured to work with the provided `CarAgent.cs` and `CheckpointManager.cs` scripts.

## Prerequisites
- **Unity**: Version 2020.3 or later with ML-Agents package (`com.unity.ml-agents`) installed via Package Manager.
- **Scripts**: Ensure `CarAgent.cs` and `CheckpointManager.cs` are in `Assets/Scripts/` (as provided in the project).
- **Assets**: Basic 3D models (e.g., car, cube, plane) available in Unity or imported.

## Step-by-Step Scene Setup

1. **Create a New Scene**:
   - In Unity, go to `File > New Scene` (select "Basic" template).
   - Save the scene as `Assets/Scenes/CarNavigationScene.unity`.

2. **Set Up the Ground**:
   - Create a Plane (`GameObject > 3D Object > Plane`).
   - Name it "Ground".
   - Scale: `(10, 1, 10)` to create a large flat surface.
   - Add a `Mesh Collider` (should be added by default).
   - Tag: Set to "ground" (create the tag if it doesn’t exist).
   - Position: `(0, 0, 0)`.

3. **Create the Car**:
   - Create a 3D model for the car (e.g., `GameObject > 3D Object > Cube` or import a car model).
   - Name it "Car".
   - Position: `(0, 1, 0)` (slightly above ground to avoid clipping).
   - Add Components:
     - **Rigidbody**:
       - Mass: `1`.
       - Constraints: Freeze Y Position and Y Rotation (to keep the car upright and on the ground).
     - **Box Collider** (or appropriate collider for your model).
     - **CarAgent.cs**: Attach the script (drag from `Assets/Scripts/CarAgent.cs`).
       - Set `Move Speed`: `10`.
       - Set `Turn Speed`: `100`.
     - **RayPerceptionSensorComponent3D** (from ML-Agents package):
       - Rays Per Direction: `10` (total 20 rays).
       - Max Ray Degrees: `180`.
       - Sphere Cast Radius: `0.5`.
       - Detectable Tags: Add "obstacle", "checkpoint", "wall".
     - **Behavior Parameters** (from ML-Agents):
       - Behavior Name: `CarAgent`.
       - Model: Leave empty for training; assign `.onnx` file for inference.
       - Actions: Continuous, size `2` (move and turn).
     - **Decision Requester**: Add component, set Decision Period to `5`.
   - Tag: Set to "Player".

4. **Create Checkpoints**):
   - Create 6 checkpoints (e.g., `GameObject > 3D Object > Cube` for each).
   - Name them "Checkpoint1" to "Checkpoint6".
   - Position them sequentially along a track path, e.g.:
     - Checkpoint1: `(5, 1, 0)`
     - Checkpoint2: `(10, 1, 5)`
     - Checkpoint3: `(10, 1, 10)`
     - Checkpoint4: `(5, 1, 15)`
     - Checkpoint5: `(0, 1, 10)`
     - Checkpoint6: `(0, 1, 5)`
   - Scale: `(2, 2, 2)` (adjust for visibility).
   - Add a `Box Collider` to each:
     - Check `Is Trigger`.
   - Tag: Set to "checkpoint" (create the tag if needed).

5. **Create Obstacles and Walls**:
   - Add obstacles (e.g., `GameObject > 3D Object > Cube` or custom models).
   - Name them "Obstacle1", "Obstacle2", etc.
   - Position them to create a challenging track, e.g.:
     - Obstacle1: `(7, 1, 2)`
     - Obstacle2: `(8, 1, 8)`
   - Add `Box Collider` to each (not a trigger).
   - Tag: Set to "obstacle".
   - Add walls (e.g., stretched cubes) around the track:
     - Example: Create a cube, scale to `(20, 2, 0.5)`, position at `(0, 1, 10)` for a boundary wall.
     - Tag: Set to "wall".
     - Add `Box Collider`.

6. **Set Up Checkpoint Manager**:
   - Create an empty GameObject (`GameObject > Create Empty`).
   - Name it "CheckpointManager".
   - Attach `CheckpointManager.cs`.
   - In the Inspector, set the `Checkpoints` array size to `6`.
   - Drag Checkpoint1 to Checkpoint6 into the array slots in order.

7. **Link Checkpoint Manager to Car**:
   - Select the Car GameObject.
   - In the `CarAgent` component, drag the `CheckpointManager` GameObject to the `Checkpoint Manager` field.

8. **Add Lighting and Camera**:
   - Ensure a `Directional Light` exists (`GameObject > Light > Directional Light`) for visibility.
   - Position: `(0, 10, 0)`, Rotation: `(50, -30, 0)`.
   - Adjust the Main Camera:
     - Position: `(0, 5, -10)` to follow the car.
     - Rotation: `(20, 0, 0)` for a good view angle.
     - Optionally, add a `Follow Camera` script to track the car dynamically.

9. **Configure Training Area**:
   - Create an empty GameObject named "TrainingArea".
   - Parent the Ground, Car, Checkpoints, Obstacles, and CheckpointManager under it for organization.
   - Ensure the TrainingArea is at `(0, 0, 0)`.

10. **Validate Setup**:
    - Verify all tags ("ground", "checkpoint", "obstacle", "wall", "Player") are set.
    - Check that the Car has all required components (`Rigidbody`, `CarAgent`, `RayPerceptionSensorComponent3D`, `BehaviorParameters`, `Decision Requester`).
    - Ensure Checkpoints are in the correct order in `CheckpointManager`.

## Scene Hierarchy Example
```
CarNavigationScene
├── TrainingArea
│   ├── Ground (Plane, tag: ground)
│   ├── Car (Cube/Model, tag: Player, with Rigidbody, CarAgent, RayPerceptionSensor3D, etc.)
│   ├── Checkpoint1 (Cube, tag: checkpoint, trigger collider)
│   ├── Checkpoint2 (Cube, tag: checkpoint, trigger collider)
│   ├── Checkpoint3 (Cube, tag: checkpoint, trigger collider)
│   ├── Checkpoint4 (Cube, tag: checkpoint, trigger collider)
│   ├── Checkpoint5 (Cube, tag: checkpoint, trigger collider)
│   ├── Checkpoint6 (Cube, tag: checkpoint, trigger collider)
│   ├── Obstacle1 (Cube, tag: obstacle)
│   ├── Obstacle2 (Cube, tag: obstacle)
│   ├── Wall1 (Cube, tag: wall)
│   └── CheckpointManager (Empty, with CheckpointManager.cs)
├── Directional Light
└── Main Camera
```

## Testing the Scene
1. **Heuristic Testing**:
   - In Unity, select the Car and set `Behavior Parameters` to "Heuristic Only".
   - Press Play and use arrow keys (Vertical for move, Horizontal for turn) to test car controls and checkpoint triggers.
2. **Training**:
   - Run `mlagents-learn config/car_ppo_config.yaml --run-id=CarNavRun --force` (as described in the project README).
   - Ensure the Unity Editor is open with the scene loaded or use a built executable.
3. **Inference**:
   - After training, assign the generated `.onnx` model to the Car’s `Behavior Parameters`.
   - Set to "Inference" mode and play the scene to test the trained agent.


