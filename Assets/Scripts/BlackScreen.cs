using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerHealth playerHealth;
    public Image blackScreenImage;

    public float blackScreenDelay = 1f;
    void OnEnable()
    {
        playerHealth.OnShowBlackScreen += HandleShowBlackScreen;
    }

    void OnDisable()
    {
        playerHealth.OnShowBlackScreen -= HandleShowBlackScreen;
    }

    void HandleShowBlackScreen()
    { 
        StartCoroutine(ShowBlackScreen());
    }

    private IEnumerator ShowBlackScreen()
    {
        blackScreenImage.color = new Color(0, 0, 0, 1); // Set black screen to fully opaque
        yield return new WaitForSeconds(blackScreenDelay);
        playerHealth.ResetHealth();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
