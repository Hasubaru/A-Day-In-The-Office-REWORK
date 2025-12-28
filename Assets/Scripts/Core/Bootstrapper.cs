using UnityEngine;
using UnityEngine.SceneManagement;

namespace ADayInTheOffice.Core
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private string _officeSceneName = "Office";

        private void Awake()
        {
            SceneManager.LoadScene(_officeSceneName);
        }
    }
}
