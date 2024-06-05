using UnityEngine;
using UnityEngine.Serialization;

namespace MainMenuLogic
{
    public class Circle : MonoBehaviour
    {
        public Vector3 startPos;
        [FormerlySerializedAs("EndPos")] public Vector3 endPos;
        public Vector3 CurrentPos => transform.position;

        public float time;

        private void Start()
        {
            startPos = transform.position;
            endPos = CurrentPos;
        }

        private void Update()
        {
            if (!(time < 1)) 
                return;
            transform.position = Vector3.Lerp(startPos, endPos, Sigmoid(time));
            time += Time.deltaTime * 2;
        }

        private static float Sigmoid(float x) => 2 / (1 + Mathf.Exp(-(x) * 3)) - .5f;
    }
}
