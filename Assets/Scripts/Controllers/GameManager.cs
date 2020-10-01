using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Vector2 _borders;
    public static GameManager _instance;
    [SerializeField]
    Text _scoreText;
    [SerializeField]
    GameObject _gameOverMenu;
    int _score=0;
    [SerializeField]
    RawImage[] _hp;
    [SerializeField]
    Text _highScore;
   
    void Awake()
    {
        for (int i = 0; i < _hp.Length; i++)
        {
            _hp[i].gameObject.SetActive(true);
        }
        if(_scoreText==null)
        {
            _scoreText = GameObject.Find("Score").GetComponent<Text>();
        }
        if (_gameOverMenu == null)
        {
            _gameOverMenu = GameObject.Find("GameOveMenu");
        }
        _gameOverMenu.gameObject.SetActive(false);
        Player.OnPlayerDeath += UpdatePlayerHealth;
        Asteroid.OnDeath += UpdateScoreUI;
        _instance = this;
    }

    private void UpdateScoreUI(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }
    private void UpdatePlayerHealth(Player _player)
    {
       int _health = _player.GetComponent<Player>().GetHealth();
        _hp[_health].gameObject.SetActive(false);
        if(_health<=0)
        {
            CheckHighScore();
            _gameOverMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void CheckHighScore()
    {
        if (PlayerPrefs.GetInt("HighScore") < _score)
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }
        _highScore.text = "Your high score: " +PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void RestartLevel()
    {
        Player.OnPlayerDeath -= UpdatePlayerHealth;
        Asteroid.OnDeath -= UpdateScoreUI;
        Time.timeScale = 1;
        _gameOverMenu.SetActive(false);
       SceneManager.LoadScene(1);
    }

    public Vector2 GetBorders()
    {
        return _borders;
    }
}
