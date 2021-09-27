
using System;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour {

    [Space()]
    public int ID;

    public virtual void Config (byte[] buf) { }
    public virtual byte[] GetData () { return new byte[0]; }

    public virtual void UpdatePos (float x, float y, float rot) {

        transform.position = new Vector3(x, y, 0);
        transform.eulerAngles = new Vector3(0, rot, 0);
    }
    public virtual byte[] GetPos () {

        byte[] posData = new byte[12];
        Buffer.BlockCopy(BitConverter.GetBytes(transform.position.x), 0, posData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(transform.position.y), 0, posData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(transform.eulerAngles.y), 0, posData, 8, 4);

        return posData;
    }
    public virtual byte[] GetPosPacket () {

        byte[] posData = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(ID), 0, posData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(transform.position.x), 0, posData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(transform.position.y), 0, posData, 8, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(transform.eulerAngles.y), 0, posData, 12, 4);

        return posData;
    }

    private Vector3 posCache;
    private Quaternion rotCache;
    private float timer;
    private void Update () {

        if (!GlobalValues.Hosting) return;

        if (timer < 0) { timer = 0.25f;

            if (posCache != transform.position || rotCache != transform.rotation) {

                posCache = transform.position; rotCache = transform.rotation;

                UdpStream.SendObjectPosUpdate(GetPosPacket());
            }

        } else timer -= Time.deltaTime;

        Tick();
    }

    public virtual void Tick () {}

    public void DeleteThisObject () {

        MainClient.SendObjectDelete(ID);
    }
}
