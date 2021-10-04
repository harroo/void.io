
using System;

using UnityEngine;

public static class BulletCreator {

    public static void CreateBullet (int bulletType, int damage, Vector3 pos, Vector3 rot) {

        byte[] bulletData = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, bulletData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, bulletData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(rot.z), 0, bulletData, 8, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(damage), 0, bulletData, 12, 4);

        TcpStream.Send_CreateObject(UnityEngine.Random.Range(-999999999, 999999999), bulletType, bulletData);
    }
}
