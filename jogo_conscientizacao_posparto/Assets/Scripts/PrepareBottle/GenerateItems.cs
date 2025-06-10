using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    public float timer = 0.7f;
    public float minTimer = 0.3f; // Tempo mínimo entre os spawns
    public float timerReductionRate = 0.01f; // Taxa de redução do tempo
    public GameObject[] prefabs; // [0] = copo vazio, [1] = garrafa cheia
    public RectTransform canvasRect;

    private float currentTime;

    void Start()
    {
        currentTime = timer;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            Spawn();
            timer = Mathf.Max(minTimer, timer - timerReductionRate); // Reduz o tempo gradualmente
            currentTime = timer; 
        }
    }

    void Spawn()
    {
        if (prefabs.Length < 2 || canvasRect == null)
        {
            Debug.LogWarning("Prefabs ou CanvasRect faltando.");
            return;
        }

        int chance = Random.Range(0, 100);
        GameObject prefab = (chance < 80) ? prefabs[0] : prefabs[1]; // 80% para posição 0, 20% para posição 1

        GameObject obj = Instantiate(prefab, canvasRect);
        RectTransform rt = obj.GetComponent<RectTransform>();
        float x = Random.Range(-250f, 250f);
        rt.anchoredPosition = new Vector2(x, 700f);
    }
}