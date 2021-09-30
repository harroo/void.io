
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public static class ServerLayer {

    private static Thread clientCatchThread;

    public static void Init () {

        Console.Log("ServerLayer.Init(): Calling...");

        //start udp

        UdpCore.ipp = new IPEndPoint(IPAddress.Any, 2586);
        UdpCore.client = new UdpClient(UdpCore.ipp);

        //start tcp

        try {

            TcpListener listener = new TcpListener(IPAddress.Any, 2486);
            listener.Start();

            clientCatchThread = new Thread(()=>ClientCatchLoop(listener));
            clientCatchThread.Start();

            Console.Log(LogType.OK, "Server started successfully!");

            TcpCore.running = true;

        } catch (Exception ex) {

            Console.Log(LogType.ERROR, "Failed to start Server: <color=yellow>" + ex.Message + "</color>");
        }

        Console.Log(LogType.OK, "ServerLayer.Init(): Success!");
    }

    public static void Kill () {

        Console.Log("ServerLayer.Kill(): Calling...");

        Decache();

        clientCatchThread.Abort();

        TcpCore.running = false;

        Console.Log(LogType.OK, "ServerLayer.Kill(): Success!");
    }

    private static void ClientCatchLoop (TcpListener listener) {

        while (true) {

            TcpClient client = listener.AcceptTcpClient();

            Console.Log("Caught client: '<color=white>" + client.Client.RemoteEndPoint.ToString() + "</color>'");

            AddNewClient(client);
        }
    }

    private static Mutex mutex = new Mutex();
    private static List<TcpClient> newClientQueue = new List<TcpClient>();

    private static void AddNewClient (TcpClient client) {

        mutex.WaitOne(); try {

            newClientQueue.Add(client);

        } finally { mutex.ReleaseMutex(); }
    }
    private static TcpClient PopNewClient () {

        TcpClient newClient = null;

        mutex.WaitOne(); try {

            if (newClientQueue.Count != 0) {

                newClient = newClientQueue[0];
                newClientQueue.RemoveAt(0);
            }

        } finally { mutex.ReleaseMutex(); }

        return newClient;
    }

    private static void Decache () {

        clients.Clear();
        clientPoints.Clear();

        clientsToDisconnect.Clear();

        UdpCore.ClearQueues();
        TcpCore.ClearQueues();
    }

    private static List<TcpClient> clients = new List<TcpClient>();
    private static List<IPEndPoint> clientPoints = new List<IPEndPoint>();

    private static List<TcpClient> clientsToDisconnect = new List<TcpClient>();

    public static void Tick () {

        if (!TcpCore.running) return;

        try {

            UdpTick();
            TcpTick();

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "ServerLayer.Tick(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);
        }
    }

    private static void UdpTick () {

        while (UdpCore.client.Available != 0) {

            byte[] recvData = UdpCore.client.Receive(ref UdpCore.ipp);

            if (!clientPoints.Contains(UdpCore.ipp))
                clientPoints.Add(UdpCore.ipp);

            UdpCore.sendQueue.Add(recvData);
        }

        while (UdpCore.sendQueue.Count != 0) {

            byte[] data = UdpCore.sendQueue[0];
            UdpCore.sendQueue.RemoveAt(0);

            foreach (IPEndPoint targetPoint in clientPoints) {

                if (targetPoint == UdpCore.ipp) continue;

                UdpCore.client.Send(data, data.Length, targetPoint.Address.ToString(), targetPoint.Port);
            }

            UdpCore.recvQueue.Add(data);
        }
    }

    private static void TcpTick () {

        HandleNewClients();
        HandleLeavingClients();
        ListenToClients();
        TalkToClients();
    }

    private static void HandleNewClients () {

        while (true) {

            TcpClient client = PopNewClient();
            if (client == null) break;

            clients.Add(client);

            byte[] objectData = ServerSave.GetSave();
            client.GetStream().Write(BitConverter.GetBytes(objectData.Length), 0, 4);
            if (objectData.Length != 0) client.GetStream().Write(objectData, 0, objectData.Length);
        }
    }

    private static void HandleLeavingClients () {

        if (clientsToDisconnect.Count == 0) return;

        foreach (TcpClient client in clientsToDisconnect) {

            clients.Remove(client);

            try {

                client.Close();

            } catch (Exception ex) {

                Console.Log(LogType.WARN, "Failed to boot client: " + client.Client.RemoteEndPoint.ToString());
                Console.Log(LogType.ERROR, ex.Message);
            }
        }
        clientsToDisconnect.Clear();

        if (clients.Count == 0) {

            Decache();

            Console.Log("Zero clients online, cache cleared.");
        }
    }

    private static void ListenToClients () {

        foreach (TcpClient client in clients) {

            if (client.Available != 0) {

                byte[] psizebuf = new byte[4];
                client.GetStream().Read(psizebuf, 0, 4);
                int psize = BitConverter.ToInt32(psizebuf, 0);

                byte[] pbuf = new byte[psize];
                client.GetStream().Read(pbuf, 0, psize);

                TcpCore.sendQueue.Add(pbuf);
            }
        }
    }

    private static void TalkToClients () {

        while (TcpCore.sendQueue.Count != 0) {

            byte[] data = TcpCore.sendQueue[0];
            TcpCore.sendQueue.RemoveAt(0);

            foreach (TcpClient client in clients) {

                client.GetStream().Write(BitConverter.GetBytes(data.Length), 0, 4);

                client.GetStream().Write(data, 0, data.Length);
            }

            TcpCore.recvQueue.Add(data);
        }
    }
}
