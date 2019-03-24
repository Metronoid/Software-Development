using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MQTT : MonoBehaviour
{
    public MqttClient client;
    // The connection information
    public string brokerHostname = "broker.0f.nl";
    public int brokerPort = 8883;
    // listen on all the Topic
    public string subTopic = "7/motor_vehicle/+/light/+";

    void Awake()
    {
        if (brokerHostname != null)
        {
            Debug.Log("connecting to " + brokerHostname + ":" + brokerPort);
            Connect();

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, };
            client.Subscribe(new string[] { subTopic }, qosLevels);
        }
    }

    private void Connect()
    {
        Debug.Log("about to connect on '" + brokerHostname + "'");
        // Forming a certificate based on a TextAsset
        X509Certificate cert = new X509Certificate();
        client = new MqttClient(brokerHostname);
        string clientId = Guid.NewGuid().ToString();
        try
        {
            client.Connect(clientId);
            Debug.Log("Connected");
        }
        catch (Exception e)
        {
            Debug.LogError("Connection error: " + e);
        }
    }

    public static bool ValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received message from " + e.Topic + " : " + msg);
    }

    public void Publish(string _topic, string msg)
    {
        client.Publish(
            _topic, Encoding.UTF8.GetBytes(msg),
            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
