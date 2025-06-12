// CleaningCloth.cs
using UnityEngine;

// Script para o pano que o jogador arrasta e aplica limpeza
public class CleaningCloth : MonoBehaviour
{
    [Tooltip("Velocidade de limpeza aplicada ao objeto durante o movimento.")]
    public float scrubRate = 50f;

    private Vector3 offset;
    private Camera mainCam;
    private Vector3 lastMouseWorldPos;

    void Start()
    {
        mainCam = Camera.main;
    }

    void OnMouseDown()
    {
        // Calcula offset para manter o ponto de clique relativo
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
        lastMouseWorldPos = transform.position;
    }

    void OnMouseDrag()
    {
        // Move o pano conforme o mouse
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = new Vector3(worldPoint.x, worldPoint.y, transform.position.z) + offset;

        // Calcula distância percorrida desde o último frame
        float dist = Vector2.Distance(lastMouseWorldPos, newPos);

        // Atualiza posição do pano
        transform.position = newPos;

        // Detecta todas as sujeiras sob o pano e aplica limpeza proporcional
        Collider2D[] hits = Physics2D.OverlapPointAll(newPos);
        foreach (Collider2D hit in hits)
        {
            Cleanable cleanable = hit.GetComponent<Cleanable>();
            if (cleanable != null)
            {
                cleanable.Scrub(dist * scrubRate * Time.deltaTime);
            }
        }

        // Atualiza última posição para o próximo cálculo
        lastMouseWorldPos = newPos;
    }
}
