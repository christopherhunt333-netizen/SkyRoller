using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    float score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();

        if (Time.timeScale == 0f && Keyboard.current.spaceKey.isPressed) {
            RestartGame();
        }

        if (Time.timeScale == 0f && Keyboard.current.qKey.isPressed) {
            ReturnToMainMenu();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }

    void RestartGame() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
