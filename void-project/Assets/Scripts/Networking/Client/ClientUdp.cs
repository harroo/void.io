
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class ClientUdp : UnityEngine.MonoBehaviour {

    public void Update () {

        // if (!GlobalValues.RunningOnline) return;
        if (!MainClient.connected) return;

        UdpCore.Tick();

        while (UdpCore.recvQueue.Count != 0) {

            Process(UdpCore.recvQueue[0]);
            UdpCore.recvQueue.RemoveAt(0);
        }
    }

    private void Process (byte[] packet) {

        switch (packet[0]) {

            case 0: { //update object pos

                int id = BitConverter.ToInt32(packet, 1);

                float x = BitConverter.ToSingle(packet, 5);
                float y = BitConverter.ToSingle(packet, 9);
                float r = BitConverter.ToSingle(packet, 13);

                ObjectManager.instance.UpdateObjectPos(id, x, y, r);

            break; }
        }
    }
}

public static class UdpStream {

    public static void SendObjectPosUpdate (byte[] data) {

        byte[] packetBuf = new byte[data.Length + 1];
        packetBuf[0] = 0;
        Buffer.BlockCopy(data, 0, packetBuf, 1, data.Length);

        UdpCore.sendQueue.Add(packetBuf);
    }
}

public static class UdpCore {

    public static UdpClient client;
    public static IPEndPoint ipp;

    public static string serverAddress;
    public static int serverPort;

    public static List<byte[]> sendQueue = new List<byte[]>();
    public static List<byte[]> recvQueue = new List<byte[]>();

    public static void Initialize () {

        recvQueue.Clear();

        serverAddress = GameLoader.address;
        serverPort = 2586;

        ipp = new IPEndPoint(IPAddress.Any, UnityEngine.Random.Range(4000, 5000));
        client = new UdpClient(ipp);
    }

    public static void Tick () {

        try {

            while (UdpCore.client.Available != 0) {

                byte[] recvData = client.Receive(ref UdpCore.ipp);

                recvQueue.Add(recvData);
            }

            while (sendQueue.Count != 0) {

                client.Send(sendQueue[0], sendQueue[0].Length, serverAddress, serverPort);
                sendQueue.RemoveAt(0);
            }

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "UdpCore.Tick(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);
        }
    }
}
