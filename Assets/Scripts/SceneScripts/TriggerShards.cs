using UnityEngine;

namespace SceneScripts
{
    public class TriggerShards : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) 
                Instantiate(canvas, new Vector3(0, 0, 0), new Quaternion());
        }
    }
}
