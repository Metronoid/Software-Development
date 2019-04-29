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
    // The connection information
    public string brokerHostname = "broker.0f.nl";
    public int brokerPort = 8883;
    public byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, };
    public string team = "7";
    public MqttClient client;

    public void Awake()
    {
        client = getClient();
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

    public MqttClient getClient()
    {
        MqttClient client = new MqttClient(brokerHostname);
        string clientId = Guid.NewGuid().ToString();
        try
        {
            client.Connect(clientId);
        }
        catch (Exception e)
        {
            Debug.LogError("Connection error: " + e);
        }
        return client;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
