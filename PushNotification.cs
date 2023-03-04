using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushNotification : MonoBehaviour
{
    public void Start()
    {
        // Code for create Notification
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        // for notification send
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        // for notification received
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }
}
