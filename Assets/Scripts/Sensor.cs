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
    public bool bridge = false;

    public MQTT MQTT;
    private MqttClient client;

    private string subTopic;
    private string bridgeTopic;

    // Start is called before the first frame update
    void Start()
    {
        client = MQTT.client;
        subTopic = MQTT.team + "/" + type + "/" + group + "/" + component + "/" + id;
        client.Subscribe(new string[] { subTopic }, MQTT.qosLevels);

        if (bridge)
        {
            bridgeTopic = MQTT.team + "/vessel/3/" + component + "/1";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Publish(string topic, string message)
    {
        MQTT.Publish(topic, message);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (bridge && collider.gameObject.tag == "Boat")
        {
            MQTT.Publish(bridgeTopic, "1");
            return;
        }
        MQTT.Publish(subTopic, "1");
    }

    private void OnTriggerExit(Collider collider)
    {
        if (bridge && collider.gameObject.tag == "Boat")
        {
            MQTT.Publish(bridgeTopic, "0");
            return;
        }
        MQTT.Publish(subTopic, "0");
    }
}
