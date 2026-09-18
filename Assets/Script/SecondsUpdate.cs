using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    private float timeStartsOffset = 0f;
    private bool gotStartTime = false;
    [SerializeField] private float speed = 0.5f;

    private void Update()
    {
        if (!gotStartTime)
        {
            timeStartsOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, speed * (Time.realtimeSinceStartup - timeStartsOffset));
    }
}
