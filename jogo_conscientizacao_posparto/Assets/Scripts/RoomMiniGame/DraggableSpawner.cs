// Assets/Scripts/DraggableSpawner.cs
using UnityEngine;

public class DraggableSpawner : MonoBehaviour {
    [Tooltip("Prefab de um DraggableItem com o script e sem valores de shapeMask configurados")]
    public GameObject draggablePrefab;

    [Tooltip("Posição no mundo onde o item será instanciado")]
    public Vector3 spawnPosition = Vector3.zero;

    void Start() {
        SpawnLShapedItem();
    }

    public void SpawnLShapedItem() {
        GameObject go = Instantiate(draggablePrefab, spawnPosition, Quaternion.identity);
        DraggableItem item = go.GetComponent<DraggableItem>();
        item.SetShapeMask(new bool[,] {
            { true, true },
            { true, false }
        });
    }
}
