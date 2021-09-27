
using UnityEngine;

using TMPro;

using System;

public class PlayerObject : Object {

    public TextMeshPro usernameDisplay;

    public SpriteRenderer spriteRenderer;

    public string username {

        get { return usernameDisplay.text; }
        set { usernameDisplay.text = value; }
    }

    public int textureIndex;

    public override void Config (byte[] data) {

        textureIndex = BitConverter.ToInt32(data, 0);
        spriteRenderer.sprite = PlayerRenderingAssets.Get(textureIndex);

        if (data.Length == 4) return;

        ushort nameSize = BitConverter.ToUInt16(data, 4);

        byte[] nameBuf = new byte[nameSize];
        Buffer.BlockCopy(data, 6, nameBuf, 0, nameSize);
        username = System.Text.Encoding.ASCII.GetString(nameBuf);
    }

    public override byte[] GetData () {

        byte[] nameBuf = System.Text.Encoding.ASCII.GetBytes(username);

        byte[] data = new byte[nameBuf.Length + 6];

        Buffer.BlockCopy(BitConverter.GetBytes(textureIndex), 0, data, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)nameBuf.Length), 0, data, 4, 2);
        Buffer.BlockCopy(nameBuf, 0, data, 6, nameBuf.Length);

        return data;
    }
}
