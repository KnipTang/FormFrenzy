using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class HighscoreFileIO : MonoBehaviour
{
    private UnityEngine.TextAsset _textFile;
    private string _Path;
    private string _Highscore;
    private void Start()
    {
        _Path = Path.Combine(Application.persistentDataPath, "HighScore.txt");

        if (File.Exists(_Path))
        {
            _Highscore = File.ReadAllText(_Path);
        }
        else
        {
            _textFile = Resources.Load<UnityEngine.TextAsset>("HighScore");
            if (_textFile != null)
            {
                _Highscore = _textFile.text;
            }
            else
            {
                _Highscore = "0";
            }
        }

        Debug.Log("Highscore: " + _Highscore);
    }
    public string ReadFile()
    {
        _Path = Path.Combine(Application.persistentDataPath, "HighScore.txt");
        if (File.Exists(_Path))
        {
            return File.ReadAllText(_Path);
        }
        else
        {
            _textFile = Resources.Load<UnityEngine.TextAsset>("HighScore");
            if (_textFile != null)
            {
                return _textFile.text;
            }
            else
            {
                return "0";
            }
        }
    }
    public void WriteFile(string text)
    {
        _Highscore = text;
        _Path = Path.Combine(Application.persistentDataPath, "HighScore.txt");

        if(File.Exists(_Path))
        {
            File.WriteAllText(_Path, _Highscore);
            Debug.Log("Highscore saved: " + _Highscore);
        }
    }

    public void ResetHighScore()
    {
        GameStats.Instance.CurrentHighScore = 0;
        GameStats.Instance.HitHighScore = false;
    }
}
