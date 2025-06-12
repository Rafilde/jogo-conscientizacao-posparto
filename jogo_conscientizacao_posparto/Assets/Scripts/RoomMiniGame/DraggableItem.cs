using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class DraggableItem : MonoBehaviour {
    public Vector2Int sizeInCells;
    public bool[,] shapeMask;

    private GridManager grid;
    private Vector3 offset;
    private Vector3 stockPosition;
    private Vector3 pivotOffset;
    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f;

    private bool isPlaced;
    private Vector2Int lastGridPos;
    private bool[,] lastMask;
    private Vector2Int lastSize;

    void Awake() {
        grid = FindObjectOfType<GridManager>();
    }

    void Start() {
        stockPosition = transform.position;
        InitializeMask();
        RecalcPivot();
    }

    void OnMouseDown() {
        // detectar duplo clique sempre
        if (Time.time - lastClickTime <= doubleClickThreshold) {
            RotateItem();
            lastClickTime = 0f;
            return;
        }
        lastClickTime = Time.time;
        BeginDrag();
    }

    void OnMouseDrag() {
        Vector3 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mw.x, mw.y, 0f) + offset;
    }

    void OnMouseUp() {
        TryPlace(transform.position);
    }

    void InitializeMask() {
        if (shapeMask == null) {
            shapeMask = new bool[sizeInCells.x, sizeInCells.y];
            for (int x = 0; x < sizeInCells.x; x++)
                for (int y = 0; y < sizeInCells.y; y++)
                    shapeMask[x, y] = true;
        }
    }

    void RecalcPivot() {
        pivotOffset = new Vector3(sizeInCells.x * grid.cellSize / 2f,
                                  sizeInCells.y * grid.cellSize / 2f,
                                  0f);
    }

    void BeginDrag() {
        if (isPlaced) ClearLast();
        Vector3 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mw.x, mw.y, 0f);
    }

    bool TryPlace(Vector3 worldPos) {
        Vector2Int gp = grid.WorldToGrid(worldPos - pivotOffset);

        bool outsideX = gp.x + sizeInCells.x <= 0 || gp.x >= grid.cols;
        bool outsideY = gp.y + sizeInCells.y <= 0 || gp.y >= grid.rows;
        if (outsideX || outsideY) {
            // fora da grid: mantém onde soltou, sem marcar
            isPlaced = false;
            transform.position = worldPos;
            return true;
        }

        if (grid.CanPlace(gp, sizeInCells, shapeMask)) {
            return PlaceAt(gp);
        }

        var nearest = grid.FindNearestAvailableSpot(gp, sizeInCells, shapeMask);
        if (nearest.HasValue) {
            return PlaceAt(nearest.Value);
        }

        // dentro da grid mas não coube em lugar nenhum
        transform.position = stockPosition;
        isPlaced = false;
        return false;
    }

    void ClearLast() {
        grid.SetOccupied(lastGridPos, lastSize, false, lastMask);
        isPlaced = false;
    }

    bool PlaceAt(Vector2Int gp) {
        if (!grid.CanPlace(gp, sizeInCells, shapeMask)) return false;

        if (isPlaced) ClearLast();

        lastGridPos = gp;
        lastSize = sizeInCells;
        lastMask = (bool[,])shapeMask.Clone();

        transform.position = grid.GridToWorld(gp) + pivotOffset;
        grid.SetOccupied(gp, sizeInCells, true, shapeMask);
        isPlaced = true;

        if (grid.IsComplete()) {
        GameManager.Instance.OnGameComplete();
}
        return true;
    }

    void RotateItem() {
        // sempre gira, mesmo fora da grid
        // se estava encaixado, limpa
        if (isPlaced) ClearLast();

        // gera máscara rotacionada
        int w = sizeInCells.x, h = sizeInCells.y;
        bool[,] m = new bool[h, w];
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                if (shapeMask[x, y])
                    m[y, w - 1 - x] = true;
        shapeMask = m;
        sizeInCells = new Vector2Int(h, w);
        RecalcPivot();

        // gira o objeto
        transform.Rotate(0, 0, -90f);

        // tenta recolocar na mesma posição ou mais próxima
        if (isPlaced) {
            if (!PlaceAt(lastGridPos))
                TryPlace(transform.position);
        }

        lastClickTime = 0f;
    }

    public void SetShapeMask(bool[,] mask) {
        shapeMask = mask;
        sizeInCells = new Vector2Int(mask.GetLength(0), mask.GetLength(1));
        RecalcPivot();
    }
}
