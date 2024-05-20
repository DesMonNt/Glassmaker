using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void RunFightScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
