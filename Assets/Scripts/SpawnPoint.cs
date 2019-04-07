using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Curve;

public class SpawnPoint : MonoBehaviour
{
    public GameObject Car;
    [Range(0.5f, 10)]
    public float minTime;
    [Range(0.5f, 10)]
    public float maxTime;

    private Vector3 Position;
    private float timeLeft;
    private bool spawn = true;

    public BGCurve[] routes;

    void Start()
    {
        Position = transform.position;
        timeLeft = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCar();
    }

    void SpawnCar()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && spawn)
        {
            var carComponent = Car.GetComponent<Car>();
            int index = Random.Range(0, routes.Length);
            carComponent.route = routes[index];
            Instantiate(Car, Position, transform.rotation);
            timeLeft = Random.Range(minTime, maxTime);
            spawn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spawn = true;
    }
}
