using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShakeMamadeiraGame : MonoBehaviour
{
    public RectTransform mamadeiraTransform;
    public Image barraProgresso;

    [Header("Configuração de Dificuldade")]
    public float aumentoBasePorClique = 0.06f;
    public float reducaoBasePorSegundo = 0.1f;

    [Header("Efeito Visual")]
    public float deslocamentoIntensidade = 10f;

    private float progresso = 0f;
    private Vector3 posOriginal;

    void Start()
    {
        posOriginal = mamadeiraTransform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float ganho = aumentoBasePorClique * Mathf.Lerp(1f, 0.5f, progresso);
            progresso += ganho;
            progresso = Mathf.Clamp01(progresso);

            Vector3 deslocamento = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                0f
            ) * deslocamentoIntensidade;

            mamadeiraTransform.localPosition = posOriginal + deslocamento;
        }
        else
        {
            float perda = reducaoBasePorSegundo * Mathf.Lerp(1f, 2f, progresso);
            progresso -= perda * Time.deltaTime;
            progresso = Mathf.Clamp01(progresso);

            mamadeiraTransform.localPosition = Vector3.Lerp(
                mamadeiraTransform.localPosition,
                posOriginal,
                5f * Time.deltaTime
            );
        }

        barraProgresso.fillAmount = progresso;

        if (progresso >= 1f)
        {
            SceneManager.LoadScene("NomeDaProximaCena");
        }
    }
}
