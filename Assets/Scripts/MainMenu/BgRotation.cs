using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BgRotation : MonoBehaviour
{
    private float speed = 2f;
    [SerializeField]
    private float direction = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var rotationZ = Quaternion.AngleAxis(Time.deltaTime * speed * direction, new Vector3(0, 0, 1));
        transform.rotation *= rotationZ;
    }
}
