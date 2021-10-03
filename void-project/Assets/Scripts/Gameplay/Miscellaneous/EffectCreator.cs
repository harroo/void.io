
using System;

using UnityEngine;

public static class EffectCreator {

    public static void CreateEffect (int effectType, Vector3 pos, Vector3 rot) {

        byte[] effectData = new byte[12];
        Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, effectData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, effectData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(rot.z), 0, effectData, 8, 4);

        TcpStream.Send_CreateEffectObject(new System.Random().Next(111111111, 999999999), effectType, effectData);
    }
}
