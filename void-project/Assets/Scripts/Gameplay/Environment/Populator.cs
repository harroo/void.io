
using System;

using UnityEngine;

public class Populator : MonoBehaviour {

    private void Start () {

        for (int i = 0; i < UnityEngine.Random.Range(2, 6); ++i) {

            CreateStarSystem(
                UnityEngine.Random.Range(-64, 64),
                UnityEngine.Random.Range(-64, 64),
                UnityEngine.Random.Range(-99, 99)
            );
        }

        for (int i = 0; i < UnityEngine.Random.Range(4, 16); ++i) {

            CreateNebula(
                UnityEngine.Random.Range(-64, 64),
                UnityEngine.Random.Range(-64, 64),
                UnityEngine.Random.Range(-99, 99)
            );
        }
    }

    private void CreateStarSystem (float x, float y, int seed) {

        byte[] planetData = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(x), 0, planetData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(y), 0, planetData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(0), 0, planetData, 8, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(seed), 0, planetData, 12, 4);

        TcpStream.Send_CreateObject(new System.Random().Next(111111111, 999999999), 3, planetData);
    }

    private void CreateNebula (float x, float y, int seed) {

        byte[] nebulaData = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(x), 0, nebulaData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(y), 0, nebulaData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(0), 0, nebulaData, 8, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(seed), 0, nebulaData, 12, 4);

        TcpStream.Send_CreateObject(new System.Random().Next(111111111, 999999999), 4, nebulaData);
    }
}
