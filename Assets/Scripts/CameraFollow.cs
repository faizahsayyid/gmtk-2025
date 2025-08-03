using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset; // Adjust this in the Inspector to set camera distance and height

    void LateUpdate()
    {
        GameObject player = GameObject.FindWithTag(Tags.Player);
        if (player != null)
        {
            Vector3 newPosition = player.transform.position + offset;
            transform.position = new Vector3(
                newPosition.x,
                newPosition.y,
                transform.position.z // Keep the camera's z position unchanged
            );
        }
    }
}
