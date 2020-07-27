using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager GetInstance() { return instance; }

    private GameObject player;
    private PlayerMovement playerMovement;

    public int coins = 0;
    public int coinPurse = 0;
    private float timer = 0.0f;
    public float objectSpeed = -0.1f;
    private float maxObjectSpeed = -0.25f;
    public bool incSpeed = false;
    public bool stopTimer = false;
    public int lastFlight = 0;
    public int longestFlight = 0;
    

    private float intervalToggleTime = 2f;
    [SerializeField] private float timeUntilSpeedIncrease;
    [SerializeField] private float speedIncreaseInterval = 0.02f;

    public Text coinsCollectedLabel;
    public Text distanceLabel;
    public Text lastFlightText;
    public Text coinPurseText;
    public Text longestFlightText;

    void Start()
    {
        //Singleton - There should only ever be ONE GameStatus!
        if (instance != null)
        {
            //instance already set!
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameManager.DontDestroyOnLoad(this.gameObject);

        timeUntilSpeedIncrease = intervalToggleTime;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (lastFlightText == null)
            {
                lastFlightText = GameObject.FindWithTag("LastFlightText").GetComponent<Text>();
                lastFlightText.text = PlayerPrefs.GetInt("LastFlight", 0).ToString();
            }
            if (coinPurseText == null)
            {
                coinPurseText = GameObject.FindWithTag("CoinPurseText").GetComponent<Text>();
                coinPurseText.text = PlayerPrefs.GetInt("CoinPurse", 0).ToString();
            }
            if (longestFlightText == null)
            {
                longestFlightText = GameObject.FindWithTag("LongestFlightText").GetComponent<Text>();
                longestFlightText.text = PlayerPrefs.GetInt("LongestFlight", 0).ToString();
            }
        }
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            if (distanceLabel == null)
            {
                distanceLabel = GameObject.FindWithTag("DistanceLabel").GetComponent<Text>();
            }
            if (coinsCollectedLabel == null)
            {
                coinsCollectedLabel = GameObject.FindWithTag("CoinsCollectedLabel").GetComponent<Text>();
            }
            if (player == null || playerMovement == null)
            {
                player = GameObject.FindWithTag("Player");
                playerMovement = player.GetComponent<PlayerMovement>();
            }

            incSpeed = false;
            if (objectSpeed > maxObjectSpeed)
            {
                IncreaseSpeed();
            } 

            if (playerMovement.isDead)
            {
                stopTimer = true;
                StartCoroutine(GameOver());
            }
            else if (!stopTimer)
            {
                timer += Time.deltaTime;
            }
            distanceLabel.text = GetSeconds().ToString() + " m";
        }     
    }

    void IncreaseSpeed()
    {
        timeUntilSpeedIncrease -= Time.deltaTime;
        if(timeUntilSpeedIncrease <= 0)
        {
            objectSpeed -= speedIncreaseInterval;
            timeUntilSpeedIncrease = intervalToggleTime;
            incSpeed = true;
        }
        
    }

    public float GetObjectSpeed()
    {
        return objectSpeed;
    }

    public void AddToScore(int amt)
    {
        coins += amt;
        coinsCollectedLabel.text = coins.ToString();
    }

    private int GetSeconds()
    {
        return (int)((timer / 60)*60 + (timer%60));
    }

    private IEnumerator GameOver()
    {
        playerMovement.isDead = false;
        //Reset everything here.

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
        Reset();
        
    }

    private void Reset() 
    {
        lastFlight = GetSeconds();
        PlayerPrefs.SetInt("LastFlight", lastFlight);
        if (lastFlight > longestFlight)
        {
            longestFlight = lastFlight;
            PlayerPrefs.SetInt("LongestFlight", longestFlight);
        }
        timer = 0.0f;
        coinPurse += coins;
        PlayerPrefs.SetInt("CoinPurse", coinPurse);
        coins = 0;
        objectSpeed = -0.1f;
        incSpeed = false;
        stopTimer = false;
    }
}
