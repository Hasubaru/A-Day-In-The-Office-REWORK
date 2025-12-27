using UnityEngine;

namespace ADayInTheOffice.World.Grid
{
    /// <summary>
    /// Sprint 1 placeholder. Sprint 3 will implement proper footprint occupancy.
    /// </summary>
    public sealed class OccupancyGrid
    {
        public bool CanPlace(Vector2Int[] cells) => true;
        public void Reserve(int id, Vector2Int[] cells) { }
        public void Release(int id) { }
    }
}
