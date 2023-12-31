using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI finalScoreText;
    public GameObject titleScreen;
    public GameObject menuScreen;
    public GameObject settingScreen;
    public Button startButton;
    public Button restartButton;
    public Button settingButton;
    public Button returnButton;

    public List<GameObject> targetPrefabs;

    private int score;
    private int finalScore;
    private float countdownTime;
    private float spawnRate = 1.5f;
    private bool isGameActive;

    private float spaceBetweenSquares = 500f;
    private float minValueX = 300f;
    private float minValueY = 1000f; // y value of the center of the bottom-most square

    public void Start()
    {
        titleScreen.SetActive(true); // titleScreen on
        menuScreen.SetActive(false); // menuScreen off

        if (startButton != null)
        {
            startButton.onClick.AddListener(ActivateMenuScreen);
        }
        if (settingButton != null)
        {
            settingButton.onClick.AddListener(ActivateSettingScreen);
        }
        if (returnButton != null)
        {
            returnButton.onClick.AddListener(ActivateTitleScreen);
        }
    }

    private void ActivateMenuScreen()
    {
        menuScreen.SetActive(true);
        titleScreen.SetActive(false);
        settingScreen.SetActive(false);
    }
    private void ActivateSettingScreen()
    {
        settingScreen.SetActive(true);
        titleScreen.SetActive(false);
        menuScreen.SetActive(false);
    }
    private void ActivateTitleScreen()
    {
        titleScreen.SetActive(true);
        settingScreen.SetActive(false);
        menuScreen.SetActive(false);
    }
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        countdownTime = 10f;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        menuScreen.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false); // 追加：最初は非アクティブにする

    }

    public void Update()
    {
        if (isGameActive)
        {
            CountdownTimer();
        }

        if (countdownTime <= 0)
        {
            countdownTime = 0; 
            GameOver();
        }
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
        }
    }
    // Generate a random spawn position within the game area
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = Random.Range(minValueX, minValueX + spaceBetweenSquares);
        float spawnPosY = Random.Range(minValueY, minValueY + spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;
    }

    // Countdown the time
    public void CountdownTimer()
    {
        countdownTime -= Time.deltaTime;
        string timeText = "Time: " + countdownTime.ToString("F1"); //change number to string
        countdownText.text = timeText; // Update the time text
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString(); // Update the score text
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);  // set gameover text ti active
        restartButton.gameObject.SetActive(true); // Set restart button to active
        finalScore = score; // score into finanscore
        finalScoreText.text = "Your score " + finalScore.ToString(); // display finalscore
        finalScoreText.gameObject.SetActive(true);
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}