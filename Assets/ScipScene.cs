using UnityEngine;
using UnityEngine.SceneManagement;

public class ScipScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown) 
            SceneManager.LoadScene("Tower exploration");
    }
}
