using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSceneExit : MonoBehaviour
{
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private Vector3 topEdge;
    private Vector3 bottomEdge;

    private Scene currentScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera camera = Camera.main;
        leftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        rightEdge = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
        topEdge = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));
        bottomEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        string direction = IsTouchingEdge();
        if (direction != null)
        {
            SceneConfig sceneConfig = new SceneConfig();
            string nextSceneName = sceneConfig.GetNextScene(currentScene.name, direction);
            if (nextSceneName != null)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    string IsTouchingEdge()
    {
        Vector3 position = transform.position;

        if (position.x <= leftEdge.x)
        {
            return SceneDirections.Left;
        }
        else if (position.x >= rightEdge.x)
        {
            return SceneDirections.Right;
        }
        else if (position.y >= topEdge.y)
        {
            return SceneDirections.Up;
        }
        else if (position.y <= bottomEdge.y)
        {
            return SceneDirections.Down;
        }
        
        return null;
    }
}
