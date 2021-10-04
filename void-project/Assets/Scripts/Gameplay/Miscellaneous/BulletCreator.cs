
using System;

using UnityEngine;

public static class BulletCreator {

    public static void CreateBullet (int bulletType, Vector3 pos, Vector3 rot) {

        byte[] bulletData = new byte[12];
        Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, bulletData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, bulletData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(rot.z), 0, bulletData, 8, 4);

        TcpStream.Send_CreateObject(new System.Random().Next(111111111, 999999999), bulletType, bulletData);
    }
}