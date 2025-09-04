void Start()
{
    rb = GetComponent<Rigidbody>();
    startPosition = transform.position;
    startRotation = transform.rotation;
    lastPosition = startPosition;
}

public override void OnEpisodeBegin()
{
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    transform.position = startPosition;
    transform.rotation = startRotation;
    checkpointManager.ResetCheckpoints();
    stuckTimer = 0f;
    moveDistance = 0f;
    lastPosition = transform.position;
}

public override void CollectObservations(VectorSensor sensor)
{
    // Add car position and velocity
    sensor.AddObservation(transform.position);
    sensor.AddObservation(rb.velocity);
    // Add next checkpoint position
    sensor.AddObservation(checkpointManager.GetNextCheckpointPosition());
    // RayPerceptionSensor3D handles raycast observations
}

public override void OnActionReceived(ActionBuffers actions)
{
    // Continuous actions: move (forward/back), turn (left/right)
    float move = actions.ContinuousActions[0];
    float turn = actions.ContinuousActions[1];

    // Apply movement
    Vector3 movement = transform.forward * move * moveSpeed * Time.deltaTime;
    rb.AddForce(movement, ForceMode.VelocityChange);
    transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

    // Reward: Distance to next checkpoint
    float distanceToCheckpoint = Vector3.Distance(transform.position, checkpointManager.GetNextCheckpointPosition());
    AddReward(-distanceToCheckpoint * 0.001f); // Small penalty for distance

    // Reward: Alignment to checkpoint
    Vector3 directionToCheckpoint = (checkpointManager.GetNextCheckpointPosition() - transform.position).normalized;
    float alignment = Vector3.Dot(transform.forward, directionToCheckpoint);
    AddReward(alignment * 0.1f); // Bonus for facing checkpoint

    // Penalty: Stuck detection
    float distanceMoved = Vector3.Distance(transform.position, lastPosition);
    moveDistance += distanceMoved;
    if (distanceMoved < 0.1f)
    {
        stuckTimer += Time.deltaTime;
        if (stuckTimer > stuckThreshold)
        {
            AddReward(-0.5f); // Stuck penalty
            EndEpisode();
        }
    }
    else
    {
        stuckTimer = 0f;
    }
    lastPosition = transform.position;

    // Penalty: Collision with obstacle or wall
    if (IsCollidingWithObstacle())
    {
        AddReward(-1.0f);
        EndEpisode();
    }
}

private bool IsCollidingWithObstacle()
{
    // RayPerceptionSensor3D handles collision detection via tags
    return false; // Placeholder; ML-Agents sensor handles this
}

public void CheckpointReached()
{
    AddReward(1.0f); // Reward for reaching checkpoint
    checkpointManager.NextCheckpoint();
    stuckTimer = 0f; // Reset stuck timer
}

public void GoalReached()
{
    AddReward(5.0f); // Large reward for completing all checkpoints
    EndEpisode();
}

public override void Heuristic(in ActionBuffers actionsOut)
{
    var continuousActionsOut = actionsOut.ContinuousActions;
    continuousActionsOut[0] = Input.GetAxis("Vertical"); // Move
    continuousActionsOut[1] = Input.GetAxis("Horizontal"); // Turn
}

}
