using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image loadingBar;
    private int _percent;
    private float _square;
    
    private void Start()
    {
        _percent = 0;
        _square = 0;
    }

    private void FixedUpdate()
    {
        _percent++;
        _square += GetFunc(_percent);
        loadingBar.fillAmount = _square / 44.01f;
    }

    private float GetFunc(int x) => (float)Math.Pow(Math.E, -((0.04 * x - 2) * (0.04 * x - 2)));
}
