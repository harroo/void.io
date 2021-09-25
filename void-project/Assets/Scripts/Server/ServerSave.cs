
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class ServerSave {

    private static Dictionary<int, byte[]> cardBuffers = new Dictionary<int, byte[]>();

    public static byte[] GetSave () {

        List<byte> buffer = new List<byte>();

        foreach (byte[] ba in cardBuffers.Values) {

            byte[] sizeBuf = BitConverter.GetBytes(ba.Length);
            foreach (byte b in sizeBuf) buffer.Add(b);

            foreach (byte b in ba) buffer.Add(b);
        }

        Console.Log("Sent save buffer, size: " + buffer.Count.ToString());

        return buffer.ToArray();
    }

    public static void Save () {

        try {

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, cardBuffers);

            File.WriteAllBytes(".s.bin", memoryStream.ToArray());

            Console.Log(LogType.OK, "Saved! Size: " + memoryStream.ToArray().Length.ToString());

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "Failed to Save!");
            Console.Log(LogType.ERROR, ex.Message);
        }
    }

    public static void Load () {

        try {

            if (!File.Exists(".s.bin")) {

                cardBuffers = new Dictionary<int, byte[]>();

                Console.Log("No save file found, new one has been started.");

                return;
            }

            byte[] data = File.ReadAllBytes(".s.bin");

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            memoryStream.Write(data, 0, data.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            cardBuffers = (Dictionary<int, byte[]>)binaryFormatter.Deserialize(memoryStream);

            Console.Log(LogType.OK, "Loaded save file. Size: " + data.Length.ToString());

        } catch (Exception ex) {

            Console.Log(LogType.WARN, "Failed to Load!");
            Console.Log(LogType.ERROR, ex.Message);
        }
    }

    public static void CreateNewObject (int cardID) {

        if (cardBuffers.ContainsKey(cardID)) return;

        cardBuffers.Add(cardID, new byte[0]);

        Save();
    }

    public static void DeleteObject (int cardID) {

        if (!cardBuffers.ContainsKey(cardID)) return;

        cardBuffers.Remove(cardID);
    }

    public static void UpdateObject (int cardID, byte[] data) {

        if (!cardBuffers.ContainsKey(cardID)) return;

        cardBuffers[cardID] = data;

        // Save();
    }
}
