
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class ClientLayer {

    public static string serverAddress;
    public static int serverPortUdp;

    public static void Init () {

        Console.Log("ClientLayer.Init(): Calling...");

        serverAddress = GlobalValues.Address;
        serverPortUdp = 4566;

        //connect UDP

        UdpCore.ClearQueues();

        UdpCore.ipp = new IPEndPoint(IPAddress.Any, UnityEngine.Random.Range(40000, 50000));
        UdpCore.client = new UdpClient(UdpCore.ipp);

        UdpCore.client.Send(new byte[1]{69}, 1, serverAddress, serverPortUdp);

        //connect TCP

        TcpCore.client = new TcpClient(serverAddress, 4565);
        TcpCore.stream = TcpCore.client.GetStream();

        byte[] sizeBuf = new byte[4];
        TcpCore.stream.Read(sizeBuf, 0, 4);
        int size = BitConverter.ToInt32(sizeBuf, 0);

        if (size != 0) {

            byte[] packetBuf = new byte[size];
            TcpCore.stream.Read(packetBuf, 0, size);

            ObjectManager.instance.LoadData(packetBuf);
        }

        TcpCore.connected = true;

        Console.Log(LogType.OK, "ClientLayer.Init(): Success!");
    }

    public static void Kill () {

        Console.Log("ClientLayer.Kill(): Calling...");

        try {

            //kill tcp

            TcpCore.client.Close();
            TcpCore.stream.Close();

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "ClientLayer.Tick(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);
        }

        UdpCore.ClearQueues();
        TcpCore.ClearQueues();

        TcpCore.connected = false;

        Console.Log(LogType.OK, "ClientLayer.Kill(): Success!");
    }

    public static void Tick () {

        if (!TcpCore.connected) return;

        try {

            UdpTick();
            TcpTick();
            KeepAliveTick();

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "ClientLayer.Tick(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);

            Kill();

            Console.Log(LogType.OK, "Disconnect handled!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Crash");
        }
    }

    private static void UdpTick () {

        while (UdpCore.client.Available != 0) {

            byte[] recvData = UdpCore.client.Receive(ref UdpCore.ipp);

            UdpCore.recvQueue.Add(recvData);
        }

        while (UdpCore.sendQueue.Count != 0) {

            UdpCore.client.Send(UdpCore.sendQueue[0], UdpCore.sendQueue[0].Length, serverAddress, serverPortUdp);
            UdpCore.sendQueue.RemoveAt(0);
        }
    }

    private static void TcpTick () {

        if (TcpCore.client.Available > 4) {

            while (TcpCore.client.Available > 4) {

                byte[] recvSize = new byte[4];
                TcpCore.stream.Read(recvSize, 0, 4);
                int packetSize = BitConverter.ToInt32(recvSize, 0);

                byte[] recvData = new byte[packetSize];
                TcpCore.stream.Read(recvData, 0, recvData.Length);

                TcpCore.recvQueue.Add(recvData);
            }

        } else {

            if (TcpCore.sendQueue.Count != 0) {

                byte[] sendData = TcpCore.sendQueue[0];
                TcpCore.sendQueue.RemoveAt(0);

                byte[] sizeData = BitConverter.GetBytes(sendData.Length);
                TcpCore.stream.Write(sizeData, 0, 4);

                TcpCore.stream.Write(sendData, 0, sendData.Length);
            }
        }
    }

    private static float timer;
    private static void KeepAliveTick () {

        if (timer < 0) { timer = 1.0f;

            TcpStream.Send_KeepAlivePacket();

        } else timer -= UnityEngine.Time.deltaTime;
    }
}
