using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform characterTransform;

    void Update()
    {
        transform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, -10);
    }
}
