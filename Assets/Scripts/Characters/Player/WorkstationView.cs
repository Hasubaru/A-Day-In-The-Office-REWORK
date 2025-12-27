using UnityEngine;
using ADayInTheOffice.Data.Defs;

namespace ADayInTheOffice.Characters.Player
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class WorkstationView : MonoBehaviour
    {
        public WorkstationType Type = WorkstationType.DevDesk;
    }
}
