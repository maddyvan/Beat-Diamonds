using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private int score = 0;
    private bool gameover = false;
    private bool gameWon = false;
    public bool playingGame = false;

    public AudioSource audioSource;

    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text timeText;
    public TMPro.TMP_Text endOfGameMessage;

    public TMPro.TMP_Text level1HighScore;
    public TMPro.TMP_Text level2HighScore;
    public TMPro.TMP_Text level3HighScore;

    public GameObject endOfGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (playingGame)
        {
            UpdateScoreText();
        }
        if (!playingGame)
        {
            level1HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Level 1 Highscore", 0).ToString();
            level2HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Level 2 Highscore", 0).ToString();
            level3HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Level 3 Highscore", 0).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playingGame && !audioSource.isPlaying)
        {
            gameWon = true;
            GameOver();
        } else if (playingGame)
        {
            UpdateTimeText();
        }
    }

    private void LoadHighScores()
    {
        if(PlayerPrefs.HasKey("Level 2 Highscore"))
        {
            level1HighScore.text = "Key!";
        } else
        {
            level1HighScore.text = "no key :(";
        }
        level1HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Level 1 Highscore", 0).ToString();
        level2HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Level 2 Highscore", 0).ToString();
        level3HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Level 3 Highscore", 0).ToString();
    }

    private void UpdateTimeText()
    {
        float timeLeft = audioSource.clip.length - audioSource.time;
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string time = string.Format("{0:0}:{1:00}", minutes, seconds);
        timeText.text = "Time: " + time;
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadLevelAfterDelay(scene));
    }

    IEnumerator LoadLevelAfterDelay(string scene)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    public void UpdateScoreBy(int amount)
    {
        score += amount;
        UpdateScoreText();
        CheckGameOver();
    }

    private void GameOver()
    {
        gameover = true;
        playingGame = false;
        endOfGameObjects.SetActive(true);

        if (gameWon)
        {
            endOfGameMessage.text = "Congrats! You Won!";
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " Highscore", 0) < score)
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " Highscore", score);
            }
        } else
        {
            endOfGameMessage.text = "Sorry, you lost. Try again.";
        }
    }

    private void CheckGameOver()
    {
        if (score < 0)
        {
            gameWon = false;
            GameOver();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public bool IsPlayingGame()
    {
        return playingGame;
    }

    public bool IsGameOver()
    {
        return gameover;
    }
}
