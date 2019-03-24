using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : TCar
{
    public float speed = 5;
    RaycastHit hit;
    private bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            transform.Translate(speed * Vector3.forward * Time.deltaTime);
            // Does the ray intersect any objects excluding the player layer
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
        speed = 5;
    }
}
