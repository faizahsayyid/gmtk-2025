using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSceneExit : MonoBehaviour
{
    public PlayerSpawnDirection spawnDirection;
    private Scene currentScene;
    private SceneConfig sceneConfig = new SceneConfig();

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"PlayerSceneExit: OnTriggerEnter2D with {collision.gameObject.name}");
        if (collision.CompareTag(Tags.SceneLeftExit) || collision.CompareTag(Tags.SceneRightExit))
        {
            string direction = collision.CompareTag(Tags.SceneLeftExit) ? SceneDirections.Left : SceneDirections.Right;
            string nextSceneName = sceneConfig.GetNextScene(currentScene.name, direction);

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                spawnDirection.SetSpawnDirection(direction);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
