
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class ServerSave {

    private static Dictionary<int, byte[]> objectBuffers = new Dictionary<int, byte[]>();
    private static Dictionary<int, int> objectTypes = new Dictionary<int, int>();

    public static byte[] GetSave () {

        List<byte> buffer = new List<byte>();

        foreach (KeyValuePair<int, byte[]> kvp in objectBuffers) {

            byte[] sizeBuf = BitConverter.GetBytes(kvp.Value.Length);
            foreach (byte b in sizeBuf) buffer.Add(b);

            byte[] typeBuf = BitConverter.GetBytes(objectTypes[kvp.Key]);
            foreach (byte b in typeBuf) buffer.Add(b);

            byte[] idBuf = BitConverter.GetBytes(kvp.Key);
            foreach (byte b in idBuf) buffer.Add(b);

            foreach (byte b in kvp.Value) buffer.Add(b);
        }

        Console.Log("Sent save buffer, size: " + buffer.Count.ToString());

        return buffer.ToArray();
    }

    public static void CreateNewObject (int objectID, int type) {

        if (objectBuffers.ContainsKey(objectID)) return;

        objectBuffers.Add(objectID, new byte[0]);
        objectTypes.Add(objectID, type);
    }
    public static void CreateNewObject (int objectID, int type, byte[] data) {

        if (objectBuffers.ContainsKey(objectID)) return;

        objectBuffers.Add(objectID, data);
        objectTypes.Add(objectID, type);
    }

    public static void DeleteObject (int objectID) {

        if (!objectBuffers.ContainsKey(objectID)) return;

        objectBuffers.Remove(objectID);
        objectTypes.Remove(objectID);
    }

    public static void UpdateObject (int objectID, byte[] data) {

        if (!objectBuffers.ContainsKey(objectID)) return;

        objectBuffers[objectID] = data;
    }
}
