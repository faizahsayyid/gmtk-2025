using System.Collections.Generic;
using UnityEngine;

public class LoopRecordingLog
{
    public GameObject gameObject;
    public Vector3 position;
    public Quaternion rotation; 
    public bool flip;

    public LoopRecordingLog(GameObject gameObject, Vector3 position, Quaternion rotation, bool flip)
    {
        this.gameObject = gameObject;
        this.position = position;
        this.rotation = rotation;
        this.flip = flip;
    }
}

[CreateAssetMenu(fileName = "LooperRecording", menuName = "Scriptable Objects/LooperRecording")]
public class LooperRecording : ScriptableObject
{
    private Dictionary<int, List<LoopRecordingLog>> recording = new Dictionary<int, List<LoopRecordingLog>>();
    public void Record(int timeStamp, GameObject gameObject, Vector3 position, Quaternion rotation, bool flip)
    {
        LoopRecordingLog log = new LoopRecordingLog(gameObject, position, rotation, flip);
        if (!recording.ContainsKey(timeStamp))
        {
            recording[timeStamp] = new List<LoopRecordingLog>();
        }

        recording[timeStamp].Add(log);
    }

    public List<LoopRecordingLog> GetLogsAtTime(int timeStamp)
    {
        if (recording.ContainsKey(timeStamp))
        {
            return recording[timeStamp];
        }
        return new List<LoopRecordingLog>();
    }

    public void ResetRecording()
    {
        recording.Clear();
    }
}
