using UnityEngine;

namespace MainMenuLogic
{
    public class BgRotation : MonoBehaviour
    {
        private const float Speed = 2f;

        [SerializeField]
        private float direction = 1;

        private void Update()
        {
            var rotationZ = Quaternion.AngleAxis(Time.deltaTime * Speed * direction, new Vector3(0, 0, 1));
            transform.rotation *= rotationZ;
        }
    }
}
