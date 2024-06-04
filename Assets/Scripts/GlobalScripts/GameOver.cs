using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalScripts
{
    public class GameOver : MonoBehaviour
    {
        public void Over() => SceneManager.LoadScene("Previous");
    }
}
