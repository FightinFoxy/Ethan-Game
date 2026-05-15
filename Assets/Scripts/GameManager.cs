using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainMenuButton;

    private bool gameEnded = false;
    private bool isPaused = false;
  

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        gameOverPanel.SetActive(true);
        mainMenuButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TriggerWin()
    {
        if (gameEnded) return;
        gameEnded = true;

        mainMenuButton.SetActive(true);
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
      public void TogglePause()
    {
        if(gameEnded) return;
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        mainMenuButton.SetActive(true);
        pausePanel.SetActive(isPaused);
    }

    public void DisableInteraction()
    {
        TowerPlacer placer = FindFirstObjectByType<TowerPlacer>();
        if(placer != null) placer.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

        public void ReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu called");
        Time.timeScale = 1f; //Reset gameplay timer
        SceneManager.LoadScene("MainMenu");
    }

    
}