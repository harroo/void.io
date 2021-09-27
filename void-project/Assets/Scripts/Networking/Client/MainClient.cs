
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class MainClient {

    public static TcpClient client;
    public static NetworkStream stream;

    public static string serverAddress;
    public static bool connected;

    public static void ConnectToServer (string address, int port) {

        if (connected) return;

        serverAddress = address;

        try {

            //connect
            client = new TcpClient(address, port);
            stream = client.GetStream();

            byte[] sizeBuf = new byte[4];
            stream.Read(sizeBuf, 0, 4);
            int size = BitConverter.ToInt32(sizeBuf, 0);

            if (size != 0) {

                byte[] packetBuf = new byte[size];
                stream.Read(packetBuf, 0, size);

                ObjectManager.instance.LoadData(packetBuf);
            }

            connected = true;

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "MainClient.ConnectToServer(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);
        }
    }

    public static void DisconnectFromServer () {

        if (!connected) return;

        try {

            //send disconnect
            Send((byte)3, new byte[0]);

        } catch {}
        try {

            client.Close();
            stream.Close();

        } catch {}

        connected = false;
    }

    public static void CrashFromServer () {

        DisconnectFromServer();

        Console.Log(LogType.WARN, "<color=orange>Connection loosed.</color>");
        ClientInterface.instance.Cleanup();
    }

    public static void Tick () {

        try {

            if (!connected) return;

            if (client.Available > 0) {

                //recv packet size
                byte[] psizebuf = new byte[4];
                client.GetStream().Read(psizebuf, 0, 4);
                int psize = BitConverter.ToInt32(psizebuf, 0);

                //recv packet
                byte[] pbuf = new byte[psize];
                stream.Read(pbuf, 0, psize);

                //process the packet
                Process(pbuf);
            }

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "MainClient.Tick(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);

            CrashFromServer();
        }
    }

    public static void Process (byte[] packet) {

        switch (packet[0]) {

            case 0: { //create obj

                //read obj id
                int objID = BitConverter.ToInt32(packet, 1);

                //read type
                int type = BitConverter.ToInt32(packet, 5);

                ObjectManager.instance.CreateObject(objID, type);

            break; }

            case 1: { //delete obj

                //read obj id
                int objID = BitConverter.ToInt32(packet, 1);

                ObjectManager.instance.DeleteObject(objID);

            break; }

            case 2: { //update obj

                //read obj id
                int objID = BitConverter.ToInt32(packet, 1);

                //read obj buffer data
                byte[] objBuf = new byte[packet.Length - 5];
                Buffer.BlockCopy(packet, 5, objBuf, 0, objBuf.Length);

                ObjectManager.instance.UpdateObject(objID, objBuf);

            break; }

            case 3: { //msg

                //read msg buffer data
                byte[] msgBuf = new byte[packet.Length - 1];
                Buffer.BlockCopy(packet, 1, msgBuf, 0, msgBuf.Length);
                string msg = System.Text.Encoding.ASCII.GetString(msgBuf);

                Console.Log(msg);

            break; }

            case 4: { //create object with data

                //read obj id
                int objID = BitConverter.ToInt32(packet, 1);

                //read type
                int type = BitConverter.ToInt32(packet, 5);

                //read meta size
                byte[] sizeBuf = new byte[4];
                Buffer.BlockCopy(packet, 9, sizeBuf, 0, sizeBuf.Length);
                int size = BitConverter.ToInt32(sizeBuf, 0);

                //read meta
                byte[] meta = new byte[size];
                Buffer.BlockCopy(packet, 13, meta, 0, meta.Length);

                //create obj
                ObjectManager.instance.CreateObject(objID, type, meta);

            break; }
        }
    }

    public static void Send (byte pid, byte[] data) {

        try {

            if (!connected) return;

            stream.Write(BitConverter.GetBytes(data.Length + 1), 0, 4);
            stream.Write(new byte[1]{pid}, 0, 1);
            if (data.Length != 0) stream.Write(data, 0, data.Length);

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "MainClient.Send(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);

            CrashFromServer();
        }
    }

    public static void SendObjectUpdate (int objID, byte[] objData) {

        try {

            if (!connected) return;

            stream.Write(BitConverter.GetBytes(objData.Length + 5), 0, 4);
            stream.Write(new byte[1]{2}, 0, 1);
            stream.Write(BitConverter.GetBytes(objID), 0, 4);
            stream.Write(objData, 0, objData.Length);

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "MainClient.SendObjectUpdate(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);

            CrashFromServer();
        }
    }

    public static void SendObjectCreate (int objID, int type) {

        stream.Write(BitConverter.GetBytes(9), 0, 4);
        stream.Write(new byte[1]{5}, 0, 1);
        stream.Write(BitConverter.GetBytes(objID), 0, 4);
        stream.Write(BitConverter.GetBytes(type), 0, 4);
    }

    public static void SendObjectCreate (int objID, int type, byte[] data) {

        stream.Write(BitConverter.GetBytes(data.Length + 13), 0, 4);
        stream.Write(new byte[1]{5}, 0, 1);
        stream.Write(BitConverter.GetBytes(objID), 0, 4);
        stream.Write(BitConverter.GetBytes(type), 0, 4);
        stream.Write(BitConverter.GetBytes(data.Length), 0, 4);
        stream.Write(data, 0, data.Length);
    }

    public static void SendObjectDelete (int objID) {

        Send(1, BitConverter.GetBytes(objID));
    }

    public static void SendChatMessage (string msg) {

        try {

            if (!connected) return;

            byte[] msgBuf = System.Text.Encoding.ASCII.GetBytes(msg);

            stream.Write(BitConverter.GetBytes(msgBuf.Length + 1), 0, 4);
            stream.Write(new byte[1]{4}, 0, 1);
            stream.Write(msgBuf, 0, msgBuf.Length);

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "MainClient.SendChatMessage(): Caused System.Exception!");
            Console.Log(LogType.ERROR, ex.Message);

            CrashFromServer();
        }
    }
}
