using UnityEngine;

public class TerminalDebug : MonoBehaviour
{
  string _myLog = "*begin log";
  string _filename = "";
  bool _doShow = false;
  int _kChars = 700;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.F1)) { _doShow = !_doShow; }
  }

  void OnEnable() {
    Application.logMessageReceived += Log; 
  }

  void OnDisable() {
    Application.logMessageReceived -= Log; 
  }

  public void Log(string logString, string stackTrace, LogType type)
  {
    // for onscreen...
    _myLog = _myLog + "\n" + logString;
    if (_myLog.Length > _kChars) { _myLog = _myLog.Substring(_myLog.Length - _kChars); }

    // for the file ...
    if (_filename == "")
    {
      string d = System.Environment.GetFolderPath(
          System.Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
      System.IO.Directory.CreateDirectory(d);
      string r = Random.Range(1000, 9999).ToString();
      _filename = d + "/log-" + r + ".txt";
    }
    try { System.IO.File.AppendAllText(_filename, logString + "\n"); }
    catch { }
  }

  void OnGUI()
  {
    if (!_doShow) { return; }
    GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
        new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
    GUI.TextArea(new Rect(10, 10, 540, 370), _myLog);
  }
}
