using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Iniciar jogo");
        // SceneManager.LoadScene("cena"); 
    }

    public void OpenSettings()
    {
        Debug.Log("Abrindo configurações...");
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
