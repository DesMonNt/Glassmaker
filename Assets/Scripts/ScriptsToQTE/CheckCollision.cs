using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ScriptsToQTE
{
    public class CheckCollision : MonoBehaviour
    {
        private int _currentSum;
        private int _maxSum;
        private SpriteRenderer _sprite;
        private bool _isPulsing;

        private void Start() => _sprite = GetComponent<SpriteRenderer>();

        private void Update()
        {
            if (!Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), tag))) 
                return;
            if (!IsInRedZone()) 
                return;
            if (IsInGreenZone())
            {
                if (!_isPulsing) 
                    _ = Pulse(_sprite, Color.green, .1f);
                AccuracyText.CurrentSum += 3;
            }
            else if (IsInYellowZone())
            {
                if (!_isPulsing)
                    _ = Pulse(_sprite, Color.green, .1f);
                AccuracyText.CurrentSum += 2;
            }
            else
            {
                if (!_isPulsing)
                    _ = Pulse(_sprite, Color.green, .1f);
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
            if (_isPulsing)
                return;
            _isPulsing = true;
            var time = 0f;
            var startColor = graphic.color;

            while (time < totalTime / 2)
            {
                graphic.color += (color - startColor) * Time.deltaTime / totalTime;
                time += Time.deltaTime;

                await Task.Delay(10);
            }

            while (time < totalTime)
            {
                graphic.color -= (color - startColor) * Time.deltaTime / totalTime;
                time += Time.deltaTime;
                await Task.Delay(10);
            }

            _isPulsing = false;
        }
    }
}
