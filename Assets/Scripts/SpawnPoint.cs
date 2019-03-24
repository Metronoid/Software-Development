using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject Car;
    [Range(0.5f, 10)]
    public float minTime;
    [Range(0.5f, 10)]
    public float maxTime;

    private Vector3 Position;
    private float timeLeft;
    public bool spawn = true;

    void Start()
    {
        Position = this.transform.position;
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
