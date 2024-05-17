using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image loadingBar;
    [FormerlySerializedAs("_percent")] public int percent;
    private float _square;
    
    private void Start()
    {
        percent = 0;
        _square = 0;
    }

    private void FixedUpdate()
    {
        percent++;
        _square += GetFunc(percent);
        loadingBar.fillAmount = _square / 44.01f;
        if (percent == 100)
            SceneManager.LoadScene("FightingScene");
    }

    private float GetFunc(int x) => (float)Math.Pow(Math.E, -((0.04 * x - 2) * (0.04 * x - 2)));
}
