using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaCenaButton : MonoBehaviour
{
    [Tooltip("Nome exato da cena configurada no Build Settings")]
    public string nomeCena;

    public void TrocarCena()
    {
        if (!string.IsNullOrEmpty(nomeCena))
            SceneManager.LoadScene(nomeCena);
        else
            Debug.LogWarning("Nome da cena não está definido!");
    }
}
