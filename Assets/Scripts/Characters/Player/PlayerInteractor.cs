using UnityEngine;
using UnityEngine.InputSystem;
using ADayInTheOffice.Core;
using ADayInTheOffice.Data.Defs;
using ADayInTheOffice.Systems.Tasks;

namespace ADayInTheOffice.Characters.Player
{
    public sealed class PlayerInteractor : MonoBehaviour
    {
        [Header("Assign")]
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private TaskDef _defaultTask;

        private WorkstationView _current;
        private TaskSystem _tasks;

        private void Awake()
        {
            _tasks = ServiceRegistry.Get<TaskSystem>();
        }

        private void Update()
        {
            if (Keyboard.current == null) return;
            if (_current == null) return;
            if (_defaultTask == null) return;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (_defaultTask.RequiredWorkstation != WorkstationType.None &&
                    _defaultTask.RequiredWorkstation != _current.Type)
                    return;

                _tasks.StartTask(_defaultTask);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out WorkstationView ws))
                _current = ws;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_current != null && other.gameObject == _current.gameObject)
                _current = null;
        }

        private void OnValidate()
        {
            if (_trigger == null)
                _trigger = GetComponentInChildren<Collider2D>();
        }
    }
}
