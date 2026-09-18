using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
