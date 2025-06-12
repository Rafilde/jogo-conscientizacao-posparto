using UnityEngine;

[ExecuteAlways]
public class GridManager : MonoBehaviour {
    public int rows = 5;
    public int cols = 8;
    public float cellSize = 1f;
    public Color gridColor = Color.gray;
    public bool showGrid = true;

    private bool[,] occupied;

    void Awake() {
        occupied = new bool[rows, cols];
    }

    public Vector2Int WorldToGrid(Vector3 worldPos) {
        Vector3 localPos = worldPos - transform.position;
        int x = Mathf.FloorToInt(localPos.x / cellSize);
        int y = Mathf.FloorToInt(localPos.y / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos) {
        return transform.position + new Vector3(gridPos.x * cellSize, gridPos.y * cellSize, 0f);
    }

    public void SetOccupied(Vector2Int start, Vector2Int size, bool value, bool[,] shapeMask = null) {
        for (int x = 0; x < size.x; x++) {
            for (int y = 0; y < size.y; y++) {
                if (shapeMask == null || shapeMask[x, y]) {
                    occupied[start.y + y, start.x + x] = value;
                }
            }
        }
    }

    public bool CanPlace(Vector2Int start, Vector2Int size, bool[,] shapeMask = null) {
        if (start.x < 0 || start.y < 0 ||
            start.x + size.x > cols || start.y + size.y > rows)
            return false;

        for (int x = 0; x < size.x; x++) {
            for (int y = 0; y < size.y; y++) {
                if (shapeMask == null || shapeMask[x, y]) {
                    if (occupied[start.y + y, start.x + x])
                        return false;
                }
            }
        }
        return true;
    }

    public Vector2Int? FindNearestAvailableSpot(Vector2Int start, Vector2Int size, bool[,] shapeMask = null) {
        float bestDistance = float.MaxValue;
        Vector2Int? bestPos = null;

        for (int y = 0; y <= rows - size.y; y++) {
            for (int x = 0; x <= cols - size.x; x++) {
                Vector2Int pos = new Vector2Int(x, y);
                if (CanPlace(pos, size, shapeMask)) {
                    float dist = Vector2Int.Distance(start, pos);
                    if (dist < bestDistance) {
                        bestDistance = dist;
                        bestPos = pos;
                    }
                }
            }
        }
        return bestPos;
    }

    /// <summary>
    /// Atualiza a ocupação removendo a antiga e marcando a nova.
    /// </summary>
    public void UpdateOccupied(Vector2Int oldPos, bool[,] oldMask, Vector2Int newPos, bool[,] newMask) {
        SetOccupied(oldPos, new Vector2Int(oldMask.GetLength(0), oldMask.GetLength(1)), false, oldMask);
        SetOccupied(newPos, new Vector2Int(newMask.GetLength(0), newMask.GetLength(1)), true, newMask);
    }

    void OnDrawGizmos() {
        if (!showGrid) return;
        Gizmos.color = gridColor;
        Vector3 origin = transform.position;

        for (int x = 0; x <= cols; x++) {
            Vector3 start = origin + Vector3.right * x * cellSize;
            Vector3 end   = start + Vector3.up * rows * cellSize;
            Gizmos.DrawLine(start, end);
        }
        for (int y = 0; y <= rows; y++) {
            Vector3 start = origin + Vector3.up * y * cellSize;
            Vector3 end   = start + Vector3.right * cols * cellSize;
            Gizmos.DrawLine(start, end);
        }
    }


    public bool IsComplete() {
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {
                if (!occupied[y, x]) return false;
            }
        }
        return true;
    }

}
