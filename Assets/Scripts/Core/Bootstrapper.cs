using UnityEngine;
using UnityEngine.SceneManagement;
using ADayInTheOffice.Game;

namespace ADayInTheOffice.Core
{
    /// <summary>
    /// Put this in the Bootstrap scene. It creates the persistent GameContext then loads Office scene.
    /// </summary>
    public sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private string _officeSceneName = "Office";

        private void Awake()
        {
            if (GameContextView.Exists)
            {
                LoadOffice();
                return;
            }

            var go = new GameObject("GameContext");
            go.AddComponent<GameContextView>();
            DontDestroyOnLoad(go);

            LoadOffice();
        }

        private void LoadOffice()
        {
            if (!string.IsNullOrWhiteSpace(_officeSceneName))
                SceneManager.LoadScene(_officeSceneName);
        }
    }
}
