
using System;

using UnityEngine;

public class BulletObject : Object {

    public float startForce;
    public float aliveTime;

    public int creatorID;

    public override string Type => "BULLET";

    public override int GetAttackingID () {

        return creatorID;
    }

    public override void Config (byte[] buf) {

        transform.position = new Vector3(
            BitConverter.ToSingle(buf, 0),
            BitConverter.ToSingle(buf, 4),
            0
        );

        transform.eulerAngles = new Vector3(
            0,
            0,
            BitConverter.ToSingle(buf, 8)
        );

        startForce += BitConverter.ToInt32(buf, 12);
        GetComponent<Damager>().damage = BitConverter.ToInt32(buf, 16);

        creatorID += BitConverter.ToInt32(buf, 20);
    }

    public override void Spawn () {

        GetComponent<Rigidbody2D>().AddForce(transform.up * startForce);
    }

    public override void Tick () {

        aliveTime -= Time.deltaTime;

        if (aliveTime < 0) {

            DeleteThisObject();
        }
    }
}
