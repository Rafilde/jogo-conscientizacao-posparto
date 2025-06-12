using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MosquitoClick : MonoBehaviour
{
    [Tooltip("Quanto de progresso (0–1) este mosquito dá ao ser eliminado.")]
    public float progressOnKill = 0.1f;

    private IdleFlight idleFlight;
    private UIController uiController;
    private bool isBusy = false;

    void Awake()
    {
        idleFlight = GetComponent<IdleFlight>();
        uiController = FindObjectOfType<UIController>();
    }

    void OnMouseDown()
    {
        if (isBusy) return;
        isBusy = true;
        // Pausa o voo
        idleFlight.enabled = false;
        // Inicia minigame de timing, com callbacks
        TimingMinigame.Instance.StartMinigame(OnSuccess, OnFail);
    }

    private void OnSuccess()
    {
        Debug.Log("MosquitoClick: SUCCESS callback chamado");
        // Adiciona progresso e destrói mosquito
        if (uiController != null)
        {
            uiController.AddProgress(progressOnKill);
            Debug.Log($"Progresso adicionado: {progressOnKill}");
        }
        Destroy(gameObject);
    }

    private void OnFail()
    {
        Debug.Log("MosquitoClick: FAIL callback chamado");
        // Retoma voo e libera clique
        idleFlight.enabled = true;
        isBusy = false;
    }
}