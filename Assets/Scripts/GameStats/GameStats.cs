using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    private static GameStats instance;
    public static GameStats Instance { get { return instance; } }
    [SerializeField] private float _currentScore = 0;
    [SerializeField] private float _currentHighScore = 0;
    [SerializeField] private float _currentLives = 3;
    [SerializeField] private float _maxLives = 3;
    [SerializeField] private float _wallSpeed = 2.0f;
    [SerializeField] private float _wallSpeedIncrease = 0.0f;
    [SerializeField] private float _powerUpSpeed = 2.0f;
    private bool _HitHighScore;

    private HighscoreFileIO _HighscoreFileIO;

    public bool HitHighScore
    {
        get { return _HitHighScore; } 
        set { _HitHighScore = value; }
    }

    public float CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;

            if (CurrentHighScore < _currentScore)
            {
                CurrentHighScore = _currentScore;

                if(!HitHighScore)
                {
                    UI_HighScoreBanner UI_HighScoreBanner = FindAnyObjectByType<UI_HighScoreBanner>();
                    if (UI_HighScoreBanner != null)
                    {
                        UI_HighScoreBanner.PlayerBanner();
                    }
                    HitHighScore = true;
                }
            }

            UI_Score UI_Score = FindAnyObjectByType<UI_Score>();
            if (UI_Score != null)
                UI_Score.UpdateScore();
        }
    }
    public float CurrentHighScore
    {
        get
        {
            return _currentHighScore;
        }
        set
        {
            _currentHighScore = value;

            UI_DIsplayHighscore UI_Score = FindAnyObjectByType<UI_DIsplayHighscore>();
            if (UI_Score != null)
                UI_Score.UpdateScore();

            _HighscoreFileIO.WriteFile(_currentHighScore.ToString());
        }
    }
    public float CurrentLives
    {
        get
        {
            return _currentLives;
        }
        set
        {
            // Ensure that the new value does not exceed _maxLives
            _currentLives = Mathf.Min(value, _maxLives);
            UI_Hearts UI_hc = FindAnyObjectByType<UI_Hearts>();
            if (UI_hc != null)
                UI_hc.UpdateHearts();
        }
    }
    public float WallSpeed
    {
        get
        {
            return _wallSpeed;
        }
        set
        {
            _wallSpeed = value;
        }
    }

    public float WallSpeedIncrease
    {
        get
        {
            return _wallSpeedIncrease;
        }
        set
        {
            _wallSpeedIncrease = value;
        }
    }

    public float PowerUpSpeed
    {
        get
        {
            return _powerUpSpeed;
        }
        set
        {
            _powerUpSpeed = value;
        }
    }

    //public delegate void ScoreChange();
    //public event ScoreChange OnScoreChanged;
    void Start()
    {
        if (instance != null && instance != this)
        {
           // Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        _HighscoreFileIO = FindAnyObjectByType<HighscoreFileIO>();
        CurrentHighScore = float.Parse(_HighscoreFileIO.ReadFile());
    }
    public void ResetStats()
    {
        _HighscoreFileIO = FindAnyObjectByType<HighscoreFileIO>();
        CurrentHighScore = float.Parse(_HighscoreFileIO.ReadFile());

        HitHighScore = false;

        CurrentScore = 0;
        CurrentLives = 3;

        WallSpeed = 3.0f;
        WallSpeedIncrease = 0.0f;
        PowerUpSpeed = 5.0f;
    }
}
