using UnityEngine;

namespace ADayInTheOffice.World.Grid
{
    public sealed class GridMap
    {
        public Vector2Int WorldToCell(Vector3 worldPos) => Vector2Int.RoundToInt(worldPos);
    }
}
