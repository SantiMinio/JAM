using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] PhysicsController physics = null;
    [SerializeField] public Transform root = null;
    [SerializeField] float rotationspeed = 5f;

    [SerializeField] float speed = 5;

    public float initialSpeed;

    CDModule cd = new CDModule();

    float speedMultiplier = 1;

    public Vector3 Dir => root.forward;

    public Vector3 InputDir => new Vector3(xAxis, 0, yAxis);

    public Vector3 RotateDir;

    float xAxis;
    float yAxis;

    [Header("Obstacle Avoidance")]
    //[SerializeField] float avoidanceRadious = 2;
    [SerializeField] LayerMask avoidMask;
    //[SerializeField] float avoidWeight = 2;
    [SerializeField] int numberOfRays = 10;
    [SerializeField] float angle = 90;
    [SerializeField] float rayRange = 2;

    private void Awake()
    {
        initialSpeed = speed;
    }

    public void OnUpdate()
    {
        cd.UpdateCD();
    }

    public void DoSlow(float slow, string pp = null)
    {
        slows.Add(slow);

        slowMultiplier = slows.OrderBy(x => x).First();
    }

    public void StopSlow(float slow, string pp = null)
    {
        if (slows.Contains(slow))
            slows.Remove(slow);

        if (slows.Count > 0)
            slowMultiplier = slows.OrderBy(x => x).First();
        else
        {
            slowMultiplier = 1;
        }
    }

    float slowMultiplier = 1;

    List<float> slows = new List<float>();

    public void SetSpeedMultiplier(float spdMlt, float timeToEnd, System.Action OnEndSpeed)
    {
        cd.EndCDWithExecute("SpeedMultiplier");
        cd.AddCD("SpeedMultiplier", () => { speedMultiplier = 1; OnEndSpeed?.Invoke(); }, timeToEnd);
        speedMultiplier = spdMlt;
    }

    public void SetSpeedMultiplier(float spdMlt)
    {
        cd.EndCDWithExecute("SpeedMultiplier");
        speedMultiplier = spdMlt;
    }

    public Vector3 Move()
    {
        var normalizedAxis = InputDir;


        if (normalizedAxis.magnitude >= 1) normalizedAxis.Normalize();

        normalizedAxis.y = 0;
        normalizedAxis *= speed * speedMultiplier * slowMultiplier;

        physics.SetMovementVector(normalizedAxis);
        return normalizedAxis;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void Rotate()
    {
        if (RotateDir != Vector3.zero)
        {
            root.forward = Vector3.Slerp(root.forward, RotateDir.normalized, Time.deltaTime * rotationspeed);
        }
        else if (InputDir != Vector3.zero)
        {
            root.forward = Vector3.Slerp(root.forward, InputDir.normalized, Time.deltaTime * rotationspeed);
        }
           
    }

    public void LookAt(Vector3 dir)
    {
        if (dir != Vector3.zero)
            root.forward = dir;
    }

    public void StopMove()
    {
        physics.SetMovementVector(Vector3.zero);
    }

    public void SetAxisX(float x)
    {
        xAxis = x;
    }

    public void SetAxisY(float y)
    {
        yAxis = y;
    }

    public void SetRotAxisX(float x)
    {
        RotateDir.x = x;
    }

    public void SetRotAxisY(float y)
    {
        RotateDir.z = y;
        //Debug.Log("RotateDir: " + RotateDir);
    }

    public bool IsInputMovement()
    {
        return xAxis > 0.2f || xAxis < -0.2f || yAxis > 0.2f || yAxis < -0.2f;
    }

    public Vector3 ObstacleAvoidance(Vector3 dir)
    {

        dir.y = 0;
        var rotation = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 accum = Vector3.zero;

        //for (int i = 0; i < numberOfRays; i++)
        //{
        //    var rotationMod = Quaternion.AngleAxis(-(i / (float)numberOfRays - 1) * angle * 2 + angle, root.up);
        //    var direction = rotation * rotationMod * Vector3.forward;

        //    if (Physics.Raycast(root.transform.position, direction, out RaycastHit hit, rayRange, avoidMask))
        //    {
        //        dir = hit.collider.transform.forward;
        //        break;
        //    }
        //}

        for (int i = 0; i < numberOfRays; i++)
        {
            var rotationMod = Quaternion.AngleAxis(-(i / (float)numberOfRays - 1) * angle * 2 + angle, root.up);
            var direction = rotation * rotationMod * Vector3.forward;

            if (Physics.Raycast(root.transform.position, direction, rayRange, avoidMask))
            {
                dir -= direction;

                accum += direction;
            }
        }

        if (Physics.Raycast(root.transform.position, dir.normalized, out RaycastHit info, rayRange, avoidMask))
        {
            if (accum != Vector3.zero)
                dir -= accum;
            else
            {
                dir = root.right;
            }
            //Debug.Log(dir);
            //Debug.Log("de frenteee");
        }

        dir.y = 0;

        return dir;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = root.rotation;
            var rotationMod = Quaternion.AngleAxis(-(i / (float)numberOfRays - 1) * angle * 2 + angle, root.up);
            var direction = rotation * rotationMod * Vector3.forward;

            Gizmos.DrawLine(transform.position, transform.position + direction * rayRange);
        }
    }
}
