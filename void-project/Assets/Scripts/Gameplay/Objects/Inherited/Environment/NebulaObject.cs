
using System;

using UnityEngine;

using SimplexNoise;

public class NebulaObject : Object {

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

        int seed = BitConverter.ToInt32(buf, 12);
        pandset = seed;

        GetComponent<NebulaRenderer>().Render(new NebulaRenderData{

            size = PerlinRand(3) + 1,

            color = new Color(
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f
            )
        });
    }

    private int pandset;
    private int PerlinRand (int max) {

        pandset += max;

        int x = Mathf.Abs(Mathf.RoundToInt(
            Noise.Generate(
                (pandset) * 0.32f
            ) * max
        ));

        return x;
    }
}
