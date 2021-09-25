
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

            UnityEngine.Debug.Log(ex.Message);
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

        ClientUIMethods.instance.connectionStatus.text = "<color=orange>Disconnected.</color>";
        ClientUIMethods.instance.Cleanup();
    }

    public static void Tick () {

        // try {

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

        // } catch (Exception ex) {
        //
        //     UnityEngine.Debug.LogError(ex.Message);
        //
        //     CrashFromServer();
        // }
    }

    public static void Process (byte[] packet) {

        switch (packet[0]) {

            case 0: { //create card

                //read card id
                int cardID = BitConverter.ToInt32(packet, 1);

                ObjectManager.instance.CreateObject(cardID);

            break; }

            case 1: { //delete card

                //read card id
                int cardID = BitConverter.ToInt32(packet, 1);

                ObjectManager.instance.DeleteObject(cardID);

            break; }

            case 2: { //update card

                //read card id
                int cardID = BitConverter.ToInt32(packet, 1);

                //read card buffer data
                byte[] cardBuf = new byte[packet.Length - 5];
                Buffer.BlockCopy(packet, 5, cardBuf, 0, cardBuf.Length);

                ObjectManager.instance.UpdateObject(cardID, cardBuf);

            break; }

            case 3: { //msg

                //read msg buffer data
                byte[] msgBuf = new byte[packet.Length - 1];
                Buffer.BlockCopy(packet, 1, msgBuf, 0, msgBuf.Length);
                string msg = System.Text.Encoding.ASCII.GetString(msgBuf);

                Chat.instance.AddMessage(msg);

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

            UnityEngine.Debug.LogError(ex.Message);

            CrashFromServer();
        }
    }

    public static void SendObjectUpdate (int cardID, byte[] cardData) {

        try {

            if (!connected) return;

            stream.Write(BitConverter.GetBytes(cardData.Length + 5), 0, 4);
            stream.Write(new byte[1]{2}, 0, 1);
            stream.Write(BitConverter.GetBytes(cardID), 0, 4);
            stream.Write(cardData, 0, cardData.Length);

        } catch (Exception ex) {

            UnityEngine.Debug.LogError(ex.Message);

            CrashFromServer();
        }
    }

    public static void SendObjectCreate (int cardID) {

        Send(0, BitConverter.GetBytes(cardID));

        Chat.Send("Created a Object.");
    }

    public static void SendObjectDelete (int cardID) {

        Send(1, BitConverter.GetBytes(cardID));

        Chat.Send("Deleted a Object.");
    }

    public static void SendChatMessage (string msg) {

        try {

            if (!connected) return;

            byte[] msgBuf = System.Text.Encoding.ASCII.GetBytes(msg);

            stream.Write(BitConverter.GetBytes(msgBuf.Length + 1), 0, 4);
            stream.Write(new byte[1]{4}, 0, 1);
            stream.Write(msgBuf, 0, msgBuf.Length);

        } catch (Exception ex) {

            UnityEngine.Debug.LogError(ex.Message);

            CrashFromServer();
        }
    }
}
