using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DIsplayHighscore : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private string _TextInfront;
    private void Start()
    {
        UpdateScore();
    }
    public void UpdateScore()
    {
        _scoreText.text = _TextInfront + GameStats.Instance.CurrentHighScore.ToString();
    }
}
