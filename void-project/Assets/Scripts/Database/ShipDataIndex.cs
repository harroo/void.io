
using System;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

public static class ShipIndex {

    [RuntimeInitializeOnLoadMethod]
    public static void InitializeShipData () {

        List<ShipData> shipList = new List<ShipData>();

        shipList.Add(new ShipData());

        foreach (Type type in Assembly.GetAssembly(typeof(ShipData)).GetTypes()) {

			if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ShipData))) {

                shipList.Add((ShipData)Activator.CreateInstance(type));
            }
        }

        shipDataBuffer = shipList.ToArray();

        Debug.Log("Total ships Loaded: " + shipList.Count.ToString());
    }

    public static ShipData Get (int shipId) {

        if (shipData.ContainsKey(shipId))
            return shipData[shipId];
        else
            return new ShipData();
    }

    public static ShipData[] shipDataBuffer;

    public static void InitializeShips () {

        if (shipData.Count != 0) return;

        for (int i = 0; i < shipDataBuffer.Length; ++i) {

            if (shipDataBuffer[i].shipId == -1) continue;

            shipData.Add(shipDataBuffer[i].shipId, shipDataBuffer[i]);
        }

        shipDataBuffer = new ShipData[0];
    }

    public static Dictionary<int, ShipData> shipData
        = new Dictionary<int, ShipData>();
}
