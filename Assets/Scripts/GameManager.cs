using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject obstacle;
    public float obstacleSpawnRate;
    public float maxObstacleSpawnHeight;
    public float minObstacleSpawnHeight;

    public float obstacleSpawnPositionX;

    public bool inGame;
    public GameObject resetButton;
    public GameObject exitButton;

    public Text scoreText;

    public float worldScrollingSpeed = 0.2f;

    private float score;
    private int coins;
    private int highScoreValue;

    public Text coinScoreText;

    public bool isImmortal;
    public float immortalityTime;
    public float immortalitySpeedBoost;

    public bool magnetActive;
    public float magnetSpeed;
    public float magnetDistance;
    public float magnetTime;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InitializeGame();

        //InvokeRepeating("SpawnObstacle", obstacleSpawnRate, obstacleSpawnRate);

        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }

        UpdateOnScreenScore();

        if(PlayerPrefs.HasKey("HighScoreValue"))
        {
            highScoreValue = PlayerPrefs.GetInt("HighScoreValue");
        }
        else
        {
            highScoreValue = 0;
            PlayerPrefs.SetInt("HighScoreValue", 0);
        }
    }

    private void InitializeGame()
    {
        //throw new System.NotImplementedException();

        inGame = true;

        //InvokeRepeating("SpawnObstacle", obstacleSpawnRate, obstacleSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.inGame) return;

        score += worldScrollingSpeed;
        UpdateOnScreenScore();
        
    }

    void UpdateOnScreenScore()
    {
        scoreText.text = score.ToString("0");
        coinScoreText.text = coins.ToString("0");
    }

    void SpawnObstacle()
    {
        var spawnPosition = new Vector3(obstacleSpawnPositionX, Random.Range(minObstacleSpawnHeight, maxObstacleSpawnHeight), 0f);
        Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }

    public void GameOver()
    {
        inGame = false;
        resetButton.SetActive(true);
        exitButton.SetActive(true);
        if (!SoundManager.instance.GetMuted())
        {
            SoundManager.instance.effectsSource.mute = true;
        }
        //CancelInvoke("SpawnObstacle");
        if ((int)score > highScoreValue)
        {
            highScoreValue = (int)score;
            PlayerPrefs.SetInt("HighScoreValue", highScoreValue);
        }
    }

    public void RestartGame()
    {
        if (!SoundManager.instance.GetMuted())
        {
            SoundManager.instance.PlayOnceClick();
            SoundManager.instance.effectsSource.mute = false;
        }
        
        SceneManager.LoadScene("Main");
    }

    public void ExitToMenu()
    {
        if (!SoundManager.instance.GetMuted())
        {
            SoundManager.instance.PlayOnceClick();
        }
        SceneManager.LoadScene("Menu");
    }

    public void CoinCollected(int value = 1)
    {
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);

        UpdateOnScreenScore();
    }

    public void CancelImmortality()
    {
        isImmortal = false;
        worldScrollingSpeed -= immortalitySpeedBoost;

    }

    public void ImmortalityCollected()
    {
        if(isImmortal)
        {
            CancelInvoke("CancelImmortality");
            CancelImmortality();
        }

        isImmortal = true;
        worldScrollingSpeed += immortalitySpeedBoost;
        Invoke("CancelImmortality", immortalityTime);
    }

    public void MagnetCollected()
    {
        if(magnetActive)
        {
            CancelInvoke("CancelMagnet");
        }

        magnetActive = true;
        Invoke("CancelMagnet", magnetTime);
    }

    private void CancelMagnet()
    {
        magnetActive = false;
    }
}
