
using System;

using UnityEngine;

public class AsteroidObject : Object {

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
    }

    public override void Spawn () {

        AsteroidGenerator.instance.Add(this);
    }

    public override void Despawn () {

        AsteroidGenerator.instance.Remove(this);
    }
}
