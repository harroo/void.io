
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

public static class ClientLoop {

    private static Mutex mutex = new Mutex();

    private static List<TcpClient> newClientQueue = new List<TcpClient>();
    public static void QueueNewClient (TcpClient client) {

        mutex.WaitOne(); try {

            newClientQueue.Add(client);

        } finally { mutex.ReleaseMutex(); }
    }
    public static void PopCheckNewClients () {

        mutex.WaitOne(); try {

            while (newClientQueue.Count != 0) {

                TcpClient newClient = newClientQueue[0];
                newClientQueue.RemoveAt(0);

                clients.Add(newClient);

                ChatSend("<color=green>User joined!</color>");

                //send object data
                byte[] objectData = ServerSave.GetSave();
                newClient.GetStream().Write(BitConverter.GetBytes(objectData.Length), 0, 4);
                if (objectData.Length != 0) newClient.GetStream().Write(objectData, 0, objectData.Length);
            }

        } finally { mutex.ReleaseMutex(); }
    }

    private static List<TcpClient> clients = new List<TcpClient>();

    private static List<TcpClient> clientsToDisconnect = new List<TcpClient>();

    private static List<SendNode> sendQueue = new List<SendNode>();

    public static void Decache () {

        ServerSave.Save();

        clients.Clear();
        clientsToDisconnect.Clear();
        sendQueue.Clear();
    }

    public static void Tick () {

        PopCheckNewClients();

        foreach (var client in clients) {

            if (client.Available > 0) { //if data to recv

                //recv packet size
                byte[] psizebuf = new byte[4];
                client.GetStream().Read(psizebuf, 0, 4);
                int psize = BitConverter.ToInt32(psizebuf, 0);

                //recv packet
                byte[] pbuf = new byte[psize];
                client.GetStream().Read(pbuf, 0, psize);

                //process the packet
                SendNode rdata = Process(client, pbuf);
                if (rdata != null) sendQueue.Add(rdata);
            }
        }

        //disconnect all clients needed to disconnect
        if (clientsToDisconnect.Count != 0) {

            foreach (var client in clientsToDisconnect) {

                clients.Remove(client);

                ChatSend("<color=orange>User left.</color>");

                try {

                    client.Close();

                } catch (Exception ex) {

                    Console.Log(LogType.WARN, "Failed to boot client: " + client.Client.RemoteEndPoint.ToString());
                    Console.Log(LogType.ERROR, ex.Message);
                }
            }
            clientsToDisconnect.Clear();

            if (clients.Count == 0) { //all clients offline, clear cache

                sendQueue.Clear();

                Console.Log("Zero clients online, cache cleared.");
            }

            ServerSave.Save();
        }

        //send msg if needed
        if (sendQueue.Count != 0) {

            foreach (var sn in sendQueue) {

                foreach (var client in clients) {

                    if (client == sn.clientToIgnore) continue;

                    client.GetStream().Write(sn.buffer, 0, sn.buffer.Length);
                }
            }
            sendQueue.Clear();
        }
    }

    public static void Send (NetworkStream stream, byte pid, byte[] data) {

        stream.Write(BitConverter.GetBytes(data.Length + 1), 0, 2);
        stream.Write(new byte[1]{pid}, 0, 1);
        if (data.Length != 0) stream.Write(data, 0, data.Length);
    }

    public static void ChatSend (string msg) {

        byte[] msgBuf = System.Text.Encoding.ASCII.GetBytes(msg);

        byte[] rbuf = new byte[5 + msgBuf.Length];
        Buffer.BlockCopy(BitConverter.GetBytes(1 + msgBuf.Length), 0, rbuf, 0, 4);
        rbuf[4] = 3;
        Buffer.BlockCopy(msgBuf, 0, rbuf, 5, msgBuf.Length);

        sendQueue.Add(new SendNode(rbuf));
    }

    private static SendNode Process (TcpClient client, byte[] packet) {

        switch (packet[0]) {

            case 0: { //create object

                //read object id
                int objectID = BitConverter.ToInt32(packet, 1);

                //make buffer create res packet
                byte[] rbuf = new byte[9];
                Buffer.BlockCopy(BitConverter.GetBytes(5), 0, rbuf, 0, 4);
                rbuf[4] = 0;
                Buffer.BlockCopy(BitConverter.GetBytes(objectID), 0, rbuf, 5, 4);

                ServerSave.CreateNewObject(objectID);

                Console.Log(client.Client.RemoteEndPoint.ToString() + ": Created Object: " + objectID.ToString());

                //return it
                return new SendNode(rbuf);
            }

            case 1: { //delete object

                //read object id
                int objectID = BitConverter.ToInt32(packet, 1);

                //make buffer delete res packet
                byte[] rbuf = new byte[9];
                Buffer.BlockCopy(BitConverter.GetBytes(5), 0, rbuf, 0, 4);
                rbuf[4] = 1;
                Buffer.BlockCopy(BitConverter.GetBytes(objectID), 0, rbuf, 5, 4);

                ServerSave.DeleteObject(objectID);

                Console.Log(client.Client.RemoteEndPoint.ToString() + ": Deleted Object: " + objectID.ToString());

                //return it
                return new SendNode(rbuf);
            }

            case 2: { //update object

                //read object id
                int objectID = BitConverter.ToInt32(packet, 1);

                //read object buffer data
                byte[] objectBuf = new byte[packet.Length - 5];
                Buffer.BlockCopy(packet, 5, objectBuf, 0, objectBuf.Length);

                //make buffer update packet
                byte[] rbuf = new byte[9 + objectBuf.Length];
                Buffer.BlockCopy(BitConverter.GetBytes(5 + objectBuf.Length), 0, rbuf, 0, 4);
                rbuf[4] = 2;
                Buffer.BlockCopy(BitConverter.GetBytes(objectID), 0, rbuf, 5, 4);
                Buffer.BlockCopy(objectBuf, 0, rbuf, 9, objectBuf.Length);

                ServerSave.UpdateObject(objectID, objectBuf);

                // Console.Log(client.Client.RemoteEndPoint.ToString() + ": Updated Object: " + objectID.ToString());

                //return it
                return new SendNode(rbuf, client);
            }

            case 3: { //disconnect

                //log
                Console.Log(client.Client.RemoteEndPoint.ToString() + ": Disconnected");

                //disconect em
                clientsToDisconnect.Add(client);

            break; }

            case 4: { //chat message

                byte[] msgBuf = new byte[packet.Length - 1];
                Buffer.BlockCopy(packet, 1, msgBuf, 0, msgBuf.Length);
                string msg = System.Text.Encoding.ASCII.GetString(msgBuf);

                //log
                Console.Log(client.Client.RemoteEndPoint.ToString() + ": Sent: <color=white>'" + msg + "'</color>");

                //make return packet
                byte[] rbuf = new byte[5 + msgBuf.Length];
                Buffer.BlockCopy(BitConverter.GetBytes(1 + msgBuf.Length), 0, rbuf, 0, 4);
                rbuf[4] = 3;
                Buffer.BlockCopy(msgBuf, 0, rbuf, 5, msgBuf.Length);

                //return it
                return new SendNode(rbuf);
            }
        }

        return null;
    }
}

public class SendNode {

    public byte[] buffer;
    public TcpClient clientToIgnore;

    public SendNode (byte[] a) { buffer = a; }
    public SendNode (byte[] a, TcpClient b) { buffer = a; clientToIgnore = b; }
}
