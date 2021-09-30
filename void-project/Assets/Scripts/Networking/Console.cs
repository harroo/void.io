
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System;
using System.Threading;

public class Console : MonoBehaviour {

    private static Mutex mutex = new Mutex();
    private static List<string> chatList = new List<string>();
    private static void AddChatList (string msg) {

        mutex.WaitOne(); try {

            chatList.Add(msg);

        } finally { mutex.ReleaseMutex(); }
    }

    public static Console instance;
    private void Awake () { instance = this; }

    public Text field;

    public int messageCap = 32;

    private void Start () {

        Render();
    }

    private bool renderQueued;

    private void Update () {

        if (renderQueued) {

            Render();
            renderQueued = false;
        }
    }

    public void AddMessage (string msg) {

        AddChatList(msg);
        renderQueued = true;
    }

    public static void Log (LogType logType, string msg) {

        string type = "";
        switch (logType) {

            case LogType.NULL:  type = "[        ]"; break;
            case LogType.OK:    type = "[   <color=green>OK</color>   ]"; break;
            case LogType.ERROR: type = "[  <color=red>ERROR</color> ]"; break;
            case LogType.WARN:  type = "[  <color=orange>WARN</color>  ]"; break;
        }

        instance.AddMessage(type + ": " + msg);

        switch (logType) {

            case LogType.ERROR: Debug.LogError(msg); break;
            case LogType.WARN: Debug.LogWarning(msg); break;
            default: Debug.Log(msg); break;
        }
    }
    public static void Log (string msg) {

        Log(LogType.NULL, msg);
    }

    public static void Clear () {

        mutex.WaitOne(); try {

            chatList.Clear();

        } finally { mutex.ReleaseMutex(); }
    }

    private void Render () {

        mutex.WaitOne(); try {

            field.text = "";

            while (chatList.Count > messageCap)
                chatList.RemoveAt(0);

            for (int i = 0; i < chatList.Count; ++i) {

                field.text += chatList[i] + "\n";
            }

        } finally { mutex.ReleaseMutex(); }
    }
}

public enum LogType { NULL, OK, ERROR, WARN }
