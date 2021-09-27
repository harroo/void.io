
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public static class UdpServerLoop {

    private static List<IPEndPoint> clientPoints = new List<IPEndPoint>();

    private static IPEndPoint listenPoint;
    private static UdpClient serverClient;

    public static void Init () {

        listenPoint = new IPEndPoint(IPAddress.Any, 2586);
        serverClient = new UdpClient(listenPoint);
    }

    public static void Tick () {

        if (serverClient.Available == 0) return;

        byte[] data = serverClient.Receive(ref listenPoint);

        if (!clientPoints.Contains(listenPoint))
            clientPoints.Add(listenPoint);

        foreach (IPEndPoint targetPoint in clientPoints) {

            if (targetPoint == listenPoint) continue;

            serverClient.Send(data, data.Length, targetPoint.Address.ToString(), targetPoint.Port);
        }

        // while (UdpCore.sendQueue.Count != 0) {
        //
        //     foreach (IPEndPoint targetPoint in clientPoints) {
        //
        //         if (targetPoint == listenPoint) continue;
        //
        //         serverClient.Send(data, data.Length, targetPoint.Address.ToString(), targetPoint.Port);
        //     }
        //
        //     UdpCore.sendQueue.RemoveAt(0);
        // }
    }

    public static void Phase () {

        Console.Log(LogType.OK, "Cleared '" + clientPoints.Count + "' cached Udp EndPoints.");

        clientPoints = new List<IPEndPoint>();
    }
}
