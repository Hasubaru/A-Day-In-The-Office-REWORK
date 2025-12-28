using UnityEngine;
using UnityEngine.InputSystem;
using ADayInTheOffice.Core;
using ADayInTheOffice.Data.Defs;
using ADayInTheOffice.Systems.Tasks;
using ADayInTheOffice.Systems.SceneFlow;

namespace ADayInTheOffice.Characters.Player
{
    public sealed class PlayerInteractor : MonoBehaviour
    {
        [Header("Assign")]
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private TaskDef _defaultTask;

        private WorkstationView _currentWorkstation;
        private ScenePortalView _currentPortal;

        private TaskSystem _tasks;

        private void Awake()
        {
            _tasks = ServiceRegistry.Get<TaskSystem>();
        }

        private void Update()
        {
            if (Keyboard.current == null) return;

            if (!Keyboard.current.eKey.wasPressedThisFrame) return;

            // Priority 1: Portal (door/bed)
            if (_currentPortal != null)
            {
                _currentPortal.Activate();
                return;
            }

            // Priority 2: Workstation task
            if (_currentWorkstation == null) return;
            if (_defaultTask == null) return;

            if (_defaultTask.RequiredWorkstation != WorkstationType.None &&
                _defaultTask.RequiredWorkstation != _currentWorkstation.Type)
                return;

            _tasks.StartTask(_defaultTask);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Portal first
            if (other.TryGetComponent(out ScenePortalView portal))
            {
                _currentPortal = portal;
                return;
            }

            // Workstation
            if (other.TryGetComponent(out WorkstationView ws))
            {
                _currentWorkstation = ws;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_currentPortal != null && other.gameObject == _currentPortal.gameObject)
                _currentPortal = null;

            if (_currentWorkstation != null && other.gameObject == _currentWorkstation.gameObject)
                _currentWorkstation = null;
        }

        private void OnValidate()
        {
            if (_trigger == null)
                _trigger = GetComponentInChildren<Collider2D>();
        }
    }
}
