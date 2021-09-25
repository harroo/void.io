
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System;
using System.Threading;

public class Console : MonoBehaviour {

    private List<string> chatList = new List<string>();

    public static Console instance;
    private void Awake () { instance = this; }

    public Text field;

    public int messageCap = 32;

    private Mutex mutex = new Mutex();
    private List<string> msgQ = new List<string>();
    public void QueueMessage (string msg) {

        mutex.WaitOne(); try {

            msgQ.Add(msg);

        } finally { mutex.ReleaseMutex(); }
    }
    private void Update () {

        mutex.WaitOne(); try {

            while (msgQ.Count != 0) {

                AddMessage(msgQ[0]); msgQ.RemoveAt(0);;
            }

        } finally { mutex.ReleaseMutex(); }
    }

    public void AddMessage (string msg) {

        chatList.Add(msg);

        Render();
    }

    public static void Log (LogType logType, string msg) {

        string type = "";
        switch (logType) {

            case LogType.NULL:  type = "[        ]"; break;
            case LogType.OK:    type = "[   <color=green>OK</color>   ]"; break;
            case LogType.ERROR: type = "[  <color=red>ERROR</color> ]"; break;
            case LogType.WARN:  type = "[  <color=orange>WARN</color>  ]"; break;
        }

        instance.QueueMessage(type + ": " + msg);
    }
    public static void Log (string msg) {

        Log(LogType.NULL, msg);
    }

    private void Render () {

        field.text = "";

        while (chatList.Count > messageCap)
            chatList.RemoveAt(0);

        for (int i = 0; i < chatList.Count; ++i) {

            field.text += chatList[i] + "\n";
        }
    }
}

public enum LogType { NULL, OK, ERROR, WARN }
