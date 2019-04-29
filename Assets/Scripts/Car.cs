using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

public class Car : TCar
{
    public float speed = 5;
    private float startingSpeed;
    public float turnSpeed = 5;
    public float marge = 1;
    public BGCurve route;

    public float distance = 0;
    public BGCcMath math;

    private RaycastHit hit;
    private bool pause = false;

    private void Awake()
    {
        math = route.GetComponent<BGCcMath>();
        startingSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            distance += speed * Time.deltaTime;
            Vector3 tangent;
            Vector3 position = math.CalcPositionAndTangentByDistance(distance, out tangent);
            transform.rotation = Quaternion.LookRotation(tangent);
            transform.position = position;
        }

        int layerMask = 1 << 2;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Stop();
            pause = true;
        }
        else
        {
            if (pause)
            {
                Drive();
                pause = false;
            }
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Deathzone")
        {
            Destroy(gameObject);
        }
    }

    public override void Stop()
    {
        speed = 0;
    }

    public override void Drive()
    {
        speed = startingSpeed;
    }
}
