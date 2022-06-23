using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Utils : MonoSingleton<Utils>
{
    [SerializeField] private Text _debugLog;
    private string _textLog = "DEBUG LOG: \n";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("RandomMaze");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadSceneAsync("RandomMaze");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Playground");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadSceneAsync("Playground");
        }
    }

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
