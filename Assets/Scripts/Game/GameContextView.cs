using UnityEngine;
using ADayInTheOffice.Core;
using ADayInTheOffice.Data.Defs;

namespace ADayInTheOffice.Game
{
    public sealed class GameContextView : MonoBehaviour
    {
        public static bool Exists { get; private set; }

        [Header("Configs")]
        [SerializeField] private TimeConfig _timeConfig;

        private GameContext _ctx;

        private void Awake()
        {
            if (Exists)
            {
                Destroy(gameObject);
                return;
            }

            Exists = true;
            DontDestroyOnLoad(gameObject);

            if (_timeConfig == null)
            {
                Debug.LogError("GameContextView: TimeConfig is NOT assigned.");
                return;
            }

            _ctx = new GameContext(_timeConfig);
            _ctx.Initialize();
        }

        private void Update()
        {
            _ctx?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            Exists = false;
            ServiceRegistry.Clear();
        }
    }
}
