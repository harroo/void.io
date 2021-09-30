
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class UdpLayer : UnityEngine.MonoBehaviour {

    public void Update () {

        while (UdpCore.recvQueue.Count != 0) {

            Process(UdpCore.recvQueue[0]);
            UdpCore.recvQueue.RemoveAt(0);
        }
    }

    private void Process (byte[] packet) {

        switch (packet[0]) {

            case UdpMids.UpdateObjectPos: {

                int id = BitConverter.ToInt32(packet, 1);

                float x = BitConverter.ToSingle(packet, 5);
                float y = BitConverter.ToSingle(packet, 9);
                float r = BitConverter.ToSingle(packet, 13);

                if (id != GlobalValues.LocalPlayerID) ObjectManager.instance.UpdateObjectPos(id, x, y, r);

                if (GlobalValues.Hosting) ServerSave.UpdateObjectPos(id, new UnityEngine.Vector3(x, y, r));

            break; }

            case UdpMids.ObjectUpdate: {

                int id = BitConverter.ToInt32(packet, 1);

                byte[] buf = new byte[packet.Length - 5];
                Buffer.BlockCopy(packet, 5, buf, 0, buf.Length);

                if (id != GlobalValues.LocalPlayerID) ObjectManager.instance.UpdateObject(id, buf);

                if (GlobalValues.Hosting) ServerSave.UpdateObject(id, buf);

            break; }
        }
    }
}

public static class UdpMids { //udp message ids

    public const byte UpdateObjectPos = 0;
    public const byte ObjectUpdate = 1;
}

public static class UdpStream {

    public static void Send_ObjectPosUpdate (byte[] data) {

        byte[] packetBuf = new byte[data.Length + 1];
        packetBuf[0] = UdpMids.UpdateObjectPos;
        Buffer.BlockCopy(data, 0, packetBuf, 1, data.Length);

        UdpCore.sendQueue.Add(packetBuf);
    }

    public static void Send_ObjectUpdate (int id, byte[] data) {

        byte[] packetBuf = new byte[5 + data.Length];
        packetBuf[0] = UdpMids.ObjectUpdate;
        Buffer.BlockCopy(BitConverter.GetBytes(id), 0, packetBuf, 1, 4);
        Buffer.BlockCopy(data, 0, packetBuf, 5, data.Length);

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

    public static void ClearQueues () {

        sendQueue.Clear();
        recvQueue.Clear();
    }
}
