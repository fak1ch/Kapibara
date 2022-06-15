using UnityEngine;
using UnityEngine.UI;

public class Utils : MonoSingleton<Utils>
{
    [SerializeField] private Text _debugLog;
    private string _textLog = "DEBUG LOG: \n";

    public void DebugLog(string msg)
    {
        _textLog += "\n" + msg + "\n";
        _debugLog.text = _textLog;
        Debug.Log(msg);
    }

    public void DebugLog(Vector2 vector)
    {
        _textLog += "\n" + vector.ToString() + "\n";
        _debugLog.text = _textLog;
        Debug.Log(vector.ToString());
    }

    public void DebugLog(float value)
    {
        _textLog += "\n" + value + "\n";
        _debugLog.text = _textLog;
        Debug.Log(value);
    }
}
