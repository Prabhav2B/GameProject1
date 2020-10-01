using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementPreset", menuName ="PlayerSetting/Movement")]
public class PlayerMovementSettings : ScriptableObject
{
    [Range(.1f, 10f)]
    public float movementSpeed = 10;

    [Range(0.0f, 5.0f)]
    public float timeToReachFullSpeed = .5f;

    [Range(0.0f, 5.0f)]
    public float timeToFullyStop = .5f;

    [Range(0.0f, 5.0f)]
    public float gravityScale = .5f;
}
