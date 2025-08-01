using System.Collections.Generic;
using UnityEngine;

public class LooperRecordingTimeStamp
{
    public float timeStamp;
    public GameObject gameObject;

    public LooperRecordingTimeStamp(float timeStamp, GameObject gameObject)
    {
        this.timeStamp = timeStamp;
        this.gameObject = gameObject;
    }
 }

[CreateAssetMenu(fileName = "LooperRecording", menuName = "Scriptable Objects/LooperRecording")]
public class LooperRecording : ScriptableObject
{
    private List<LooperRecordingTimeStamp> recording = new List<LooperRecordingTimeStamp>();
    public void Record(float timeStamp, GameObject gameObject)
    {
        LooperRecordingTimeStamp newTimeStamp = new LooperRecordingTimeStamp(timeStamp, gameObject);
        recording.Add(newTimeStamp);
    }
}
