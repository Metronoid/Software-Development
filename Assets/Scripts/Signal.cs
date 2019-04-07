using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;

public class Signal : MonoBehaviour
{
    public string group = "2";
    public string type = "motor_vehicle";
    public string component = "light";
    public string id = "0";

    private enum State { Red, Orange, Green, Out };
    private State currentState;
    Renderer rend;
    TCar subscriber;
    private State setState;
    public MQTT MQTT;
    private string subTopic;
    private MqttClient client;

    private void Awake()
    {
        client = MQTT.getClient();
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        EnterState(State.Red);

        subTopic = MQTT.team + "/" + type + "/" + group + "/" + component + "/" + id;
        client.Subscribe(new string[] { subTopic }, MQTT.qosLevels);

        client.MqttMsgPublishReceived += signal_MqttMsgPublishReceived;
    }

    private void OnTriggerEnter(Collider collider)
    {
        subscriber = collider.GetComponent<TCar>();
    }

    private void EnterState(State state)
    {
        Color color = Color.white;
        currentState = state;
        if (currentState == State.Red) color = Color.red;
        if (currentState == State.Orange) color = new Color(255,165,0);
        if (currentState == State.Green) color = Color.green;
        rend.material.SetColor("_Color", color);
    }

    void signal_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        if (msg == "2") setState = State.Green;
        if (msg == "1") setState = State.Orange;
        if (msg == "0") setState = State.Red;
    }

    // Update is called once per frame
    void Update()
    {
        if (subscriber)
        {
            if (currentState == State.Red) subscriber.Stop();
            if (currentState == State.Orange) subscriber.Stop();
            if (currentState == State.Green) subscriber.Drive();
        }
        if (setState != State.Out)
        {
            if(setState == State.Green) EnterState(State.Green);
            if (setState == State.Orange) EnterState(State.Orange);
            if (setState == State.Red) EnterState(State.Red);
            setState = State.Out;
        }
    }
}
