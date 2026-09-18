using UnityEngine;

public class FireShell : MonoBehaviour
{
    [SerializeField] private GameObject shell;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            Vector3 aimAt = CalculateTrajectory();
            if (aimAt != Vector3.zero)
                transform.forward = aimAt;
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(shell, turret.transform.position, turret.transform.rotation);
    }

    private Vector3 CalculateTrajectory()
    {
        Vector3 p = enemy.transform.position - transform.position;
        Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
        float s = shell.GetComponent<MoveShell>().speed;
        
        float a = Vector3.Dot(v, v) - s * s;
        float b = Vector3.Dot(p, v);
        float c = Vector3.Dot(p, p);
        float d = b * b - a * c;

        if (d < 0.1f) 
            return Vector3.zero;
        
        float sqrt = Mathf.Sqrt(d);
        float t1 = (-b - sqrt) / c;
        float t2 = (-b + sqrt) / c;

        float t = 0;
        if (t1 < 0 && t2 < 0)
            return Vector3.zero;
        else if (t1 < 0)
            t = t2;
        else if (t2 < 0)
            t = t1;
        else
            t = Mathf.Max(new float[] { t1, t2 });
        
        return t * p + v;
    }
}
