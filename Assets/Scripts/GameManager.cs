using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;

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
    }

    public void TriggerGameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TriggerWin()
    {
        if (gameEnded) return;
        gameEnded = true;

        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
      public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
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

    
}