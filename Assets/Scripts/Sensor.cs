using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;

public class Sensor : MonoBehaviour
{
    public string group = "2";
    public string type = "motor_vehicle";
    public string component = "sensor";
    public string id = "0";

    public MQTT MQTT;
    private MqttClient client;

    private string subTopic;

    private void Awake()
    {
        client = MQTT.getClient();
    }

    // Start is called before the first frame update
    void Start()
    {
        subTopic = MQTT.team + "/" + type + "/" + group + "/" + component + "/" + id;
        client.Subscribe(new string[] { subTopic }, MQTT.qosLevels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Publish(string message)
    {
        MQTT.Publish(client, subTopic, message);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Publish("1");
    }

    private void OnTriggerExit(Collider collider)
    {
        Publish("0");
    }
}
