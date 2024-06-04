using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void RunFightScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
