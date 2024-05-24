using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CheckCollision : MonoBehaviour
{
    private int _currentSum;
    private int _maxSum;
    private SpriteRenderer _sprite;
    private bool _isPulsing;
    [FormerlySerializedAs("_isUpdateMax")] [SerializeField] private bool isUpdateMax;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), tag))) 
            return;
        if (!IsInRedZone()) 
            return;
        if (IsInGreenZone())
        {
            Debug.Log("green");
            
            if (!_isPulsing)
            {
                Debug.Log("greenadf");

                Pulse(_sprite, Color.green, .1f);
            }
            AccuracyText.CurrentSum += 3;
        }
        else if (IsInYellowZone())
        {
            Debug.Log("yellow");

            if (!_isPulsing)
                Pulse(_sprite, Color.green, .1f);
            AccuracyText.CurrentSum += 2;
        }
        else
        {
            Debug.Log("else");
            if (!_isPulsing)
                Pulse(_sprite, Color.green, .1f);
            AccuracyText.CurrentSum++;
        }
    }

    private bool IsInGreenZone() =>
        transform.position.x is <= 36 and >= -36 && transform.position.y is <= 41 and >= -12;
        
    private bool IsInYellowZone() =>
        transform.position.x is <= 50 and >= -50 && transform.position.y is <= 64 and >= -41;

    private bool IsInRedZone() =>
        transform.position.x is <= 77 and >= -77 && transform.position.y is <= 88 and >= -73;

    private async Task Pulse(SpriteRenderer graphic, Color color, float totalTime)
    {
        
            Debug.Log("pulsing");

            if (_isPulsing)
                return;
            _isPulsing = true;
            var time = 0f;
            var startColor = graphic.color;
            Debug.Log(time < totalTime / 2);
            Debug.Log("hi))))");


            while (time < totalTime / 2)
            {
                Debug.Log(graphic.color);

                graphic.color += (color - startColor) * Time.deltaTime / totalTime;
                time += Time.deltaTime;
                Debug.Log(graphic.color);

                await Task.Delay(10);
            }

            Debug.Log(graphic.color);

            while (time < totalTime)
            {
                graphic.color -= (color - startColor) * Time.deltaTime / totalTime;
                time += Time.deltaTime;
                await Task.Delay(10);
            }

            _isPulsing = false;
        
    }
}
