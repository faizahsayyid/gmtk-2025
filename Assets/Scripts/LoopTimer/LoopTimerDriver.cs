using UnityEngine;

public class LoopTimerDriver : MonoBehaviour
{
    public LoopTimer loopTimer;

    // Update is called once per frame
    void Update()
    {
        loopTimer.UpdateTimer(Time.deltaTime);
    }
}
