using UnityEngine;

public class LoopTimerDriver : MonoBehaviour
{
    public LoopTimer loopTimer;


    // Start is called before the first frame update
    void Start()
    {
        loopTimer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        loopTimer.UpdateTimer(Time.deltaTime);
    }
}
