using UnityEngine;
using UnityEngine.Serialization;

namespace MainMenuLogic
{
    public class Circle : MonoBehaviour
    {
        [FormerlySerializedAs("startPos")] public Vector3 startPosition;
        [FormerlySerializedAs("endPos")] [FormerlySerializedAs("EndPos")] public Vector3 endPosition;
        public Vector3 CurrentPosition => transform.position;

        public float time;

        private void Start()
        {
            startPosition = transform.position;
            endPosition = CurrentPosition;
        }

        private void Update()
        {
            if (!(time < 1)) 
                return;
            transform.position = Vector3.Lerp(startPosition, endPosition, Sigmoid(time));
            time += Time.deltaTime * 2;
        }

        private static float Sigmoid(float x) => 2 / (1 + Mathf.Exp(-(x) * 3)) - .5f;
    }
}
