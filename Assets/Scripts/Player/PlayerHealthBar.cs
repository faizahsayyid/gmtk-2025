using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public RectTransform bar;

    public PlayerHealth playerHealth;

    private float originalWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalWidth = bar.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        bar.sizeDelta = new Vector2(originalWidth * playerHealth.GetHealthPercentage(), bar.sizeDelta.y);
    }
}
