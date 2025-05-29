using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PrepareBottleController : MonoBehaviour
{
    public Button btnWater;
    public Button btnPowderedMilk;
    public Button btnFeedingBottle;

    public Animator animWater;
    public Animator animPowderedMilk;
    public Animator animFeedingBottle;

    public TextMeshProUGUI introText;

    void Start()
{
    int current = PlayerPrefs.GetInt("FaseAtual", 1); 

    if (current == 1)
    {
        introText.text = "Vamos preparar a mamadeira do bebê. Primeiro, precisamos ferver a água. Clique na água para começar!";

        btnWater.interactable = true;
        btnPowderedMilk.interactable = false;
        btnFeedingBottle.interactable = false;

        animWater.enabled = true;
        animPowderedMilk.enabled = false;
        animFeedingBottle.enabled = false;

        btnWater.onClick.AddListener(IrParaMinigameAgua);
    }
    else if (current == 2)
    {
        introText.text = "Agora vamos adicionar o leite em pó. Clique no leite para continuar!";

        btnWater.interactable = false;
        btnPowderedMilk.interactable = true;
        btnFeedingBottle.interactable = false;

        animWater.enabled = false;
        animPowderedMilk.enabled = true;
        animFeedingBottle.enabled = false;

        // btnLeite.onClick.AddListener(IrParaMinigameLeite);
    }
    else if (current == 3)
    {
        introText.text = "Hora de agitar a mamadeira! Clique na mamadeira para terminar.";

        btnWater.interactable = false;
        btnPowderedMilk.interactable = false;
        btnFeedingBottle.interactable = true;

        animWater.enabled = false;
        animPowderedMilk.enabled = false;
        animFeedingBottle.enabled = true;

        //btnMamadeira.onClick.AddListener(IrParaMinigameAgitar);
    }
}


    void IrParaMinigameAgua()
    {
        SceneManager.LoadScene("MinigameFerverAgua");
    }
}
