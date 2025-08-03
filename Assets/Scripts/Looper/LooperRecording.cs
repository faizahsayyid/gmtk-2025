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

public class LoopDefenseLog
{
    public float castTime;

    public LoopDefenseLog(float castTime)
    {
        this.castTime = castTime;
    }
}

[CreateAssetMenu(fileName = "LooperRecording", menuName = "Scriptable Objects/LooperRecording")]
public class LooperRecording : ScriptableObject
{
    private Dictionary<int, List<LoopRecordingLog>> recording = new Dictionary<int, List<LoopRecordingLog>>();
    private Dictionary<int, LoopDefenseLog> defenseLogs = new Dictionary<int, LoopDefenseLog>();
    public void Record(int timeStamp, GameObject gameObject, Vector3 position, Quaternion rotation, bool flip)
    {
        LoopRecordingLog log = new LoopRecordingLog(gameObject, position, rotation, flip);
        if (!recording.ContainsKey(timeStamp))
        {
            recording[timeStamp] = new List<LoopRecordingLog>();
        }

        recording[timeStamp].Add(log);
    }

    public void RecordDefense(int timeStamp, float castTime)
    {
        if (!defenseLogs.ContainsKey(timeStamp))
        {
            defenseLogs[timeStamp] = new LoopDefenseLog(castTime);
        }
    }

    public List<LoopRecordingLog> GetLogsAtTime(int timeStamp)
    {
        if (recording.ContainsKey(timeStamp))
        {
            return recording[timeStamp];
        }
        return new List<LoopRecordingLog>();
    }

    public LoopDefenseLog DefenseLogAtTime(int timeStamp)
    {
        if (defenseLogs.ContainsKey(timeStamp))
        {
            return defenseLogs[timeStamp];
        }
        return null;
    }

    public void ResetRecording()
    {
        recording.Clear();
    }
}
