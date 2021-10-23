
using UnityEngine;

using TMPro;

using System;

public class LocalPlayerObject : Object {

    public SpriteRenderer spriteRenderer;

    public override string Type => "PLAYER_SHIP";

    public override void Asign () {

        ObjectManager.instance.CacheLocalObject(this);
    }

    public override byte[] GetData () {

        byte[] nameBuf = System.Text.Encoding.ASCII.GetBytes(GlobalValues.GetUsername());

        byte[] data = new byte[nameBuf.Length + 6];

        Buffer.BlockCopy(BitConverter.GetBytes(PlayerStats.textureIndex), 0, data, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)nameBuf.Length), 0, data, 4, 2);
        Buffer.BlockCopy(nameBuf, 0, data, 6, nameBuf.Length);

        return data;
    }

    private int textureIndexCache;
    public override void Tick () {

        if (textureIndexCache != PlayerStats.textureIndex) {

            textureIndexCache = PlayerStats.textureIndex;

            UdpStream.Send_ObjectUpdate(ID, GetData());
        }
    }
}
