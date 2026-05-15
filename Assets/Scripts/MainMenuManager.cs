using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quite called (won't work in editor)");
    }

}
