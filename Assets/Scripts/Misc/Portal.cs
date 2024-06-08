using UnityEngine;

namespace Misc
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private GameObject coordinatesObject;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) 
                return;
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            player.position = coordinatesObject.transform.position;
        }
    }
}
