using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Sensor : MonoBehaviour
{
    public string id = "0";
    public Signal signal;
    public MQTT MQTT;

    // Start is called before the first frame update
    void Start()
    {
        MQTT.client.MqttMsgPublishReceived += sensor_MqttMsgPublishReceived;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        signal.Subscribe(collider.gameObject.GetComponent<TCar>());
        MQTT.Publish("7/motor_vehicle/1/sensor/1", id);
    }

    void sensor_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        if (msg == "2") signal.setState = Signal.State.Green;
        if (msg == "1") signal.setState = Signal.State.Orange;
        if (msg == "0") signal.setState = Signal.State.Red;
    }
}
