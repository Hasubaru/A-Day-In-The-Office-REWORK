using UnityEngine;
using ADayInTheOffice.Core;

namespace ADayInTheOffice.Game
{
    /// <summary>
    /// MonoBehaviour bridge that creates GameContext and ticks it.
    /// Keep this as the ONLY always-running Update tick source.
    /// </summary>
    public sealed class GameContextView : MonoBehaviour
    {
        public static bool Exists { get; private set; }

        private GameContext _ctx;

        private void Awake()
        {
            if (Exists)
            {
                Destroy(gameObject);
                return;
            }

            Exists = true;
            _ctx = new GameContext();
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
