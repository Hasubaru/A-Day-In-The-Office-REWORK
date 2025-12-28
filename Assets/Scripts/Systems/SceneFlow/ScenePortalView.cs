using UnityEngine;
using ADayInTheOffice.Core;

namespace ADayInTheOffice.Systems.SceneFlow
{
    public enum PortalTarget
    {
        Office,
        City,
        Home,
        Sleep
    }

    /// <summary>
    /// Put this on trigger objects (door/bed).
    /// Player presses E near it to trigger scene transitions.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class ScenePortalView : MonoBehaviour
    {
        public PortalTarget Target = PortalTarget.Home;

        private void Reset()
        {
            // Make sure collider is trigger for portal usage
            var col = GetComponent<Collider2D>();
            if (col != null) col.isTrigger = true;
        }

        public void Activate()
        {
            var flow = ServiceRegistry.Get<SceneFlowSystem>();

            switch (Target)
            {
                case PortalTarget.Office: flow.GoToOffice(); break;
                case PortalTarget.City: flow.GoToCity(); break;
                case PortalTarget.Home: flow.GoToHome(); break;
                case PortalTarget.Sleep: flow.SleepAndStartNewDay(); break;
            }
        }
    }
}
