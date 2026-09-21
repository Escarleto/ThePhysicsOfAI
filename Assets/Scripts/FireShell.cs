using UnityEngine;

public class FireShell : MonoBehaviour
{
    [SerializeField] private GameObject shell;
    [SerializeField] private GameObject turret;
    [SerializeField] private Transform turretBase;
    [SerializeField] private GameObject enemy;
    private float speed = 15f;
    private float rotSpeed = 5f;
    private float moveSpeed = 1f;

    private void Update()
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotSpeed * Time.deltaTime);
        float? angle = RotateTurret();
        if (angle != null)
            Fire();
        else
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);

    }

    private void Fire()
    {
        GameObject newShell = Instantiate(shell, turret.transform.position, turret.transform.rotation);
        newShell.GetComponent<Rigidbody>().linearVelocity = speed * turretBase.forward;
    }

    private float? RotateTurret()
    {
        float? angle = CalculateAngle(false);
        if (angle != null)
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angle, 0, 0);
        }
        return angle;
    }

    private float? CalculateAngle(bool low)
    {
        Vector3 targetDir = enemy.transform.position - transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude - 1;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg;
            else
                return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;
        }
        else 
            return null;
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
