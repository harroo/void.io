
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class TcpLayer : UnityEngine.MonoBehaviour {

    public void Update () {

        while (TcpCore.recvQueue.Count != 0) {

            try {

                Process(TcpCore.recvQueue[0]);

            } catch (Exception ex) {

                Console.Log(LogType.WARN, "TcpLayer.Process(): Caused System.Exception!");
                Console.Log(LogType.ERROR, ex.Message);
            }

            TcpCore.recvQueue.RemoveAt(0);
        }
    }

    private void Process (byte[] packet) {

        switch (packet[0]) {

            case TcpMids.ObjectPosUpdate: {

                int id = BitConverter.ToInt32(packet, 1);

                float x = BitConverter.ToSingle(packet, 5);
                float y = BitConverter.ToSingle(packet, 9);
                float r = BitConverter.ToSingle(packet, 13);

                if (id != GlobalValues.LocalPlayerID) ObjectManager.instance.UpdateObjectPos(id, x, y, r);

                if (GlobalValues.Hosting) ServerSave.UpdateObjectPos(id, new UnityEngine.Vector3(x, y, r));

            break; }

            case TcpMids.CreateObject: {

                int id = BitConverter.ToInt32(packet, 1);

                int type = BitConverter.ToInt32(packet, 5);

                ObjectManager.instance.CreateObject(id, type);

                if (GlobalValues.Hosting) ServerSave.CreateNewObject(id, type);

            break; }

            case TcpMids.ObjectUpdate: {

                int id = BitConverter.ToInt32(packet, 1);

                byte[] buf = new byte[packet.Length - 5];
                Buffer.BlockCopy(packet, 5, buf, 0, buf.Length);

                if (id != GlobalValues.LocalPlayerID) ObjectManager.instance.UpdateObject(id, buf);

                if (GlobalValues.Hosting) ServerSave.UpdateObject(id, buf);

            break; }

            case TcpMids.DeleteObject: {

                int id = BitConverter.ToInt32(packet, 1);

                ObjectManager.instance.DeleteObject(id);

                if (GlobalValues.Hosting) ServerSave.DeleteObject(id);

            break; }

            case TcpMids.CreateObjectWithMeta: {

                int id = BitConverter.ToInt32(packet, 1);

                int type = BitConverter.ToInt32(packet, 5);

                byte[] sizeBuf = new byte[4];
                Buffer.BlockCopy(packet, 9, sizeBuf, 0, sizeBuf.Length);
                int size = BitConverter.ToInt32(sizeBuf, 0);

                byte[] meta = new byte[size];
                Buffer.BlockCopy(packet, 13, meta, 0, meta.Length);

                if (id != GlobalValues.LocalPlayerID) ObjectManager.instance.CreateObject(id, type, meta);

                if (GlobalValues.Hosting) ServerSave.CreateNewObject(id, type, meta);

            break; }

            case TcpMids.ChatMessage: {

                byte[] buf = new byte[packet.Length - 1];
                Buffer.BlockCopy(packet, 1, buf, 0, buf.Length);
                string msg = System.Text.Encoding.Unicode.GetString(buf);

                Console.Log("Chat: " + msg);

            break; }
        }
    }
}

public static class TcpMids { //tcp message ids

    public const byte CreateObject = 0;
    public const byte ObjectUpdate = 1;
    public const byte ObjectPosUpdate = 2;
    public const byte DeleteObject = 3;
    public const byte CreateObjectWithMeta = 4;
    public const byte ChatMessage = 5;
    public const byte KeepAlive = 6;
}

public static class TcpStream {

    public static void Send_ObjectPosUpdate (byte[] data) {

        byte[] packetBuf = new byte[data.Length + 1];
        packetBuf[0] = TcpMids.ObjectPosUpdate;
        Buffer.BlockCopy(data, 0, packetBuf, 1, data.Length);

        TcpCore.sendQueue.Add(packetBuf);
    }

    public static void Send_CreateObject (int id, int type) {

        byte[] packetBuf = new byte[9];
        packetBuf[0] = TcpMids.CreateObject;
        Buffer.BlockCopy(BitConverter.GetBytes(id), 0, packetBuf, 1, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(type), 0, packetBuf, 5, 4);

        TcpCore.sendQueue.Add(packetBuf);
    }

    public static void Send_DeleteObject (int id) {

        byte[] packetBuf = new byte[5];
        packetBuf[0] = TcpMids.DeleteObject;
        Buffer.BlockCopy(BitConverter.GetBytes(id), 0, packetBuf, 1, 4);

        TcpCore.sendQueue.Add(packetBuf);
    }

    public static void Send_CreateObject (int id, int type, byte[] data) {

        byte[] packetBuf = new byte[13 + data.Length];
        packetBuf[0] = TcpMids.CreateObjectWithMeta;
        Buffer.BlockCopy(BitConverter.GetBytes(id), 0, packetBuf, 1, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(type), 0, packetBuf, 5, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(data.Length), 0, packetBuf, 9, 4);
        Buffer.BlockCopy(data, 0, packetBuf, 13, data.Length);

        TcpCore.sendQueue.Add(packetBuf);
    }

    public static void Send_ChatMessage (string msg) {

        byte[] msgBuf = System.Text.Encoding.Unicode.GetBytes(msg);

        byte[] packetBuf = new byte[1 + msgBuf.Length];
        packetBuf[0] = TcpMids.ChatMessage;
        Buffer.BlockCopy(msgBuf, 0, packetBuf, 1, msgBuf.Length);

        TcpCore.sendQueue.Add(packetBuf);
    }

    public static void Send_ObjectUpdate (int id, byte[] data) {

        byte[] packetBuf = new byte[5 + data.Length];
        packetBuf[0] = TcpMids.ObjectUpdate;
        Buffer.BlockCopy(BitConverter.GetBytes(id), 0, packetBuf, 1, 4);
        Buffer.BlockCopy(data, 0, packetBuf, 5, data.Length);

        TcpCore.sendQueue.Add(packetBuf);
    }

    public static void Send_KeepAlivePacket () {

        TcpCore.sendQueue.Add(new byte[1]{TcpMids.KeepAlive});
    }
}

public static class TcpCore {

    public static TcpClient client;
    public static NetworkStream stream;

    public static bool connected;
    public static bool running;

    public static List<byte[]> sendQueue = new List<byte[]>();
    public static List<byte[]> recvQueue = new List<byte[]>();

    public static void ClearQueues () {

        sendQueue.Clear();
        recvQueue.Clear();
    }
}
