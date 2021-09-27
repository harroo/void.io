
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class ServerInterface : MonoBehaviour {

    private Thread clientCatchThread;
    // private Thread tickLoopThread;

    private void Start () {

        Console.Log("Starting server...");

        try {

            TcpListener listener = new TcpListener(IPAddress.Any, 2486);
            listener.Start();

            UdpServerLoop.Init();

            clientCatchThread = new Thread(()=>ClientCatchLoop(listener));
            clientCatchThread.Start();

            // tickLoopThread = new Thread(()=>TickLoop());
            // tickLoopThread.Start();

            Console.Log(LogType.OK, "Server started successfully!");

            GlobalValues.RunningOnline = true;

        } catch (Exception ex) {

            Console.Log(LogType.ERROR, "Failed to start server: <color=yellow>" + ex.Message + "</color>");
        }

        UdpCore.Initialize();

        int localPlayerID = UnityEngine.Random.Range(111111111, 999999999);
        PlayerManager.instance.SetPlayerStart(localPlayerID);
        ServerSave.CreateNewObject(localPlayerID, 0, PlayerManager.instance.GetPlayerData());
    }

    private void OnDestroy () {

        MainServer.Decache();

        clientCatchThread.Abort();
        // tickLoopThread.Abort();

        GlobalValues.RunningOnline = false;
    }

    private void ClientCatchLoop (TcpListener listener) {

        while (true) {

            TcpClient client = listener.AcceptTcpClient();

            Console.Log("Caught client: '<color=white>" + client.Client.RemoteEndPoint.ToString() + "</color>'");

            MainServer.QueueNewClient(client);
        }
    }

    private void Update () {

        if (!GlobalValues.RunningOnline) return;

        // while (true) {

            // try {

                MainServer.Tick();

            // } catch (Exception ex) {
            //
            //     Console.Log(LogType.WARN, "MainServer.Tick(): Caused System.Exception!");
            //     Console.Log(LogType.ERROR, ex.Message);
            // }

            try {

                UdpServerLoop.Tick();

            } catch (Exception ex) {

                Console.Log(LogType.WARN, "UdpLoop.Tick(): Caused System.Exception!");
                Console.Log(LogType.ERROR, ex.Message);
            }
        // }
    }
}
