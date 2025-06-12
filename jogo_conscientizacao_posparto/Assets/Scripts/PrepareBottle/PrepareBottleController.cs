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

    void Start()
    {
        int current = PlayerPrefs.GetInt("FaseAtual", 1);

        btnWater.interactable = false;
        animWater.enabled = false;

        btnPowderedMilk.interactable = false;
        animPowderedMilk.enabled = false;

        btnFeedingBottle.interactable = false;
        animFeedingBottle.enabled = false;

        if (current == 1)
        {
            DialogueManager.Instance.StartDialogue(new string[] {
            "O bebê está com fome e precisa da sua ajuda para tomar seu leitinho!",
            "Prepare com cuidado: coloque a água...",
            "Adicione a medida certa de leite em pó...",
            "E agite bem a mamadeira antes de entregar ao bebê.",
            "Vamos começar!",
            "Antes de tudo, precisamos colocar a água na mamdeira para o bebê!"
        }, AtivarFase1);
        }
        else if (current == 2)
        {
            DialogueManager.Instance.StartDialogue(new string[] {
            "Muito bem! Agora é hora de colocar a quantidade certinha de leite em pó.",
            "Use a medida exata para garantir que o bebê receba o leite do jeitinho certo.",
            "Nem mais, nem menos – cuidado é essencial!"
        }, AtivarFase2);
        }
        else if (current == 3)
        {
            DialogueManager.Instance.StartDialogue(new string[] {
            "Agora é hora de fechar bem a mamadeira e agitar com força!",
            "Assim, o leite e a água se misturam direitinho e o bebê recebe um leitinho bem homogêneo e gostoso."
        }, AtivarFase3);
        }
        else if (current == 4)
        {
            DialogueManager.Instance.StartDialogue(new string[] {
            "Muito bem!",
            "Agora nosso bebê está pronto para receber o leitinho.",
        }, AtivarFase4);
        }
    }

    void IrParaMinigameAgua()
    {
        SceneManager.LoadScene("PrepareWater");
    }

    void IrParaMinileitePo()
    {
        SceneManager.LoadScene("PreparePowderedMilk");
    }

    void IrParaAgitarMamadeira()
    {
        SceneManager.LoadScene("FinishBottle");
    }

    void AtivarFase1()
    {
        btnWater.interactable = true;
        animWater.enabled = true;

        btnPowderedMilk.interactable = false;
        animPowderedMilk.enabled = false;

        btnFeedingBottle.interactable = false;
        animFeedingBottle.enabled = false;

        btnWater.onClick.AddListener(IrParaMinigameAgua);
    }

    void AtivarFase2()
    {
        btnPowderedMilk.interactable = true;
        animPowderedMilk.enabled = true;

        btnPowderedMilk.onClick.AddListener(IrParaMinileitePo);
    }

    void AtivarFase3()
    {
        btnFeedingBottle.interactable = true;
        animFeedingBottle.enabled = true;

        btnFeedingBottle.onClick.AddListener(IrParaAgitarMamadeira);
    }

    void AtivarFase4()
    {
        btnWater.interactable = false;
        animWater.enabled = false;

        btnPowderedMilk.interactable = false;
        animPowderedMilk.enabled = false;

        btnFeedingBottle.interactable = false;
        animFeedingBottle.enabled = false;

        PlayerPrefs.SetInt("FaseAtual", 1);

        SceneManager.LoadScene(""); //cena do mapa
    }

}
