using UnityEngine;

public class MoveShell : MonoBehaviour
{
    public float speed = 20f;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
