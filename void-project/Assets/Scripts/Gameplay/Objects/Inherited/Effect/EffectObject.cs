
using System;

using UnityEngine;

public class EffectObject : Object {

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
}
