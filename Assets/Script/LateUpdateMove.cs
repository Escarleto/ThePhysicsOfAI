using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateUpdateMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private void LateUpdate()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
