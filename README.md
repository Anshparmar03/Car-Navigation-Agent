# Car Navigation Agent with Unity ML-Agents

This project implements a reinforcement learning-based car navigation agent in Unity using ML-Agents. The agent uses a `RayPerceptionSensorComponent3D` with 20 rays over 180° for obstacle detection and navigates a track with 6 sequential checkpoints. The reward system includes distance-based rewards, alignment bonuses, stuck penalties, and checkpoint rewards to ensure efficient navigation in complex environments.

## Features
- **Agent**: A car with continuous movement and turning actions, trained using PPO.
- **Sensors**: `RayPerceptionSensorComponent3D` with 20 rays for obstacle and checkpoint detection.
- **Reward System**:
  - Distance penalty: `-0.001 * distance` to the next checkpoint.
  - Alignment bonus: `+0.1 * alignment` for facing the checkpoint.
  - Stuck penalty: `-0.5` if stationary for 5 seconds, ends episode.
  - Checkpoint reward: `+1.0` per checkpoint, `+5.0` for completing all 6.
  - Collision penalty: `-1.0` for hitting obstacles, ends episode.
- **Environment**: Customizable track with obstacles and 6 sequential checkpoints.
- **Training**: Optimized PPO hyperparameters for fast convergence.

## Project Structure
```
CarNavigationAgent/
├── Assets/
│   ├── Scripts/
│   │   ├── CarAgent.cs           # Car agent behavior and rewards
│   │   └── CheckpointManager.cs  # Manages checkpoint progression
│   ├── Scenes/
│   │   └── CarNavigationScene.unity  # Main Unity scene
├── config/
│   └── car_ppo_config.yaml       # PPO training configuration
├── train_car_agent.py            # Python training script
└── README.md                     # This file
```

## Prerequisites
- **Unity**: 2020.3 or later with ML-Agents package (`com.unity.ml-agents`).
- **Python**: 3.8+ with `mlagents` and `torch` (`pip install mlagents torch`).
- **Git**: For cloning the repository.

## Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/CarNavigationAgent.git
   cd CarNavigationAgent
   ```

2. **Unity Setup**:
   - Open the project in Unity.
   - Import ML-Agents via Package Manager (`com.unity.ml-agents`).
   - Open `Assets/Scenes/CarNavigationScene.unity`.
   - Ensure the car has:
     - `Rigidbody` (mass: 1, freeze Y rotation and position).
     - `RayPerceptionSensorComponent3D` (20 rays, 180°, tags: "obstacle", "checkpoint", "wall").
     - `CarAgent.cs` script.
   - Assign 6 checkpoint GameObjects with the "checkpoint" tag to `CheckpointManager.cs`.

3. **Python Setup**:
   - Install dependencies: `pip install mlagents torch`.
   - Place `train_car_agent.py` and `config/car_ppo_config.yaml` in the project root.

4. **Scene Configuration**:
   - **Car**: Add `Rigidbody`, `Collider`, and `CarAgent.cs`.
   - **Checkpoints**: 6 GameObjects tagged "checkpoint", placed sequentially.
   - **Obstacles/Walls**: Tag as "obstacle" or "wall".
   - **Ground**: Add `Collider`, tag as "ground".

## Training the Agent
1. Run the training script:
   ```bash
   mlagents-learn config/car_ppo_config.yaml --run-id=CarNavRun --force
   ```
2. Monitor progress with TensorBoard:
   ```bash
   tensorboard --logdir results
   ```
3. Training stops after 5,000,000 steps or when the agent consistently completes the track.

## Testing the Agent
1. In Unity, set the `BehaviorParameters` component on the car to "Inference" mode.
2. Assign the trained model (`.onnx` file from `results/CarNavRun`) to the car.
3. Play the scene to test navigation.


## Usage
- **Training**: Run `mlagents-learn` to train the agent.
- **Testing**: Use the trained model in Unity for inference.
- **Customization**:
  - Modify the track layout or obstacles in the Unity scene.
  - Adjust rewards in `CarAgent.cs` for different behaviors.
  - Tune hyperparameters in `car_ppo_config.yaml` for performance.

## Contributing

Contributions are welcome! Fork the repo, create a branch, and submit a pull request.

## License
MIT License. See [LICENSE](LICENSE) for details.

