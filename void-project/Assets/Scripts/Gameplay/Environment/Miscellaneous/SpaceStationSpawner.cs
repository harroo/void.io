
using System;

using UnityEngine;

public class SpaceStationSpawner : MonoBehaviour {

    public int countMax, countMin;

    private void Start () {

        int count = UnityEngine.Random.Range(countMin, countMax);

        for (int i = 0; i < count; ++i) {

            CreateSpaceStation(
                UnityEngine.Random.Range(-64, 64),
                UnityEngine.Random.Range(-64, 64)
            );
        }
    }

    private void CreateSpaceStation (float x, float y) {

        byte[] stationData = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(x), 0, stationData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(y), 0, stationData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(UnityEngine.Random.Range(0f, 180f)), 0, stationData, 8, 4);

        TcpStream.Send_CreateObject(UnityEngine.Random.Range(-999999999, 999999999), 12, stationData);
    }
}
