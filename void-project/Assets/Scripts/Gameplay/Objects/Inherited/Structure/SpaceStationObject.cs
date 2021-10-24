
using System;

using UnityEngine;

public class SpaceStationObject : Object {

    public override string Type => "STATION";

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

    public override void Asign () {

        ShipdocksDetector.instance.stations.Add(transform);
    }
    public override void Expel () {

        ShipdocksDetector.instance.stations.Remove(transform);
    }
}
