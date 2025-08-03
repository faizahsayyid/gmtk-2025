using UnityEngine;

public class LoopTimerBar : MonoBehaviour
{

    public RectTransform bar;
    public GameObject text;
    public LoopTimer loopTimer;

    private float originalWidth;

    private void Awake()
    {
        originalWidth = bar.sizeDelta.x;
        HandleHideBar();
    }

    private void OnEnable()
    {
        loopTimer.onTimerTick += UpdateBar;
        loopTimer.OnTimerStopped += HandleHideBar;
    }

    private void OnDisable()
    {
        loopTimer.onTimerTick -= UpdateBar;
        loopTimer.OnTimerStopped -= HandleHideBar;
    }

    private void HandleHideBar()
    {
        bar.sizeDelta = new Vector2(0, bar.sizeDelta.y);
        text.SetActive(false);
    }

    private void UpdateBar(int seconds)
    {
        if (loopTimer.isRunning)
        {
            text.SetActive(true);
            float percentage = (float)seconds / loopTimer.duration;
            bar.sizeDelta = new Vector2(originalWidth * percentage, bar.sizeDelta.y);
        }
        else
        {
            bar.sizeDelta = new Vector2(0, bar.sizeDelta.y);
        }
    }
}
