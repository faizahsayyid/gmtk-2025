using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpawnDirection", menuName = "Scriptable Objects/PlayerSpawnDirection")]
public class PlayerSpawnDirection : ScriptableObject
{
    // Should be one of SceneDirections
    private string direction;

    public void SetSpawnDirection(string newDirection)
    {
        direction = newDirection;
    }
    
    public string GetSpawnDirection()
    {
        return direction;
    }
}
