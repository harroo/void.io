
using System;

using UnityEngine;

using SimplexNoise;

public class SolarSystemObject : Object {

    public GameObject star, planet;

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

        GameObject myStar = Instantiate(star, transform.position, transform.rotation);
        myStar.transform.SetParent(transform);
        myStar.GetComponent<StarRenderer>().Render(GenStar());

        for (int i = 0; i < PerlinRand(16); ++i) {

            GameObject myPlanet = Instantiate(planet, transform.position, transform.rotation);
            myPlanet.transform.SetParent(transform);
            myPlanet.GetComponent<PlanetRenderer>().Render(GenPlanet());

            myPlanet.transform.Rotate(0, 0, PerlinRand(360));
            myPlanet.transform.Translate(Vector3.up * PerlinRand(40));

            for (int ii = 0; ii < PerlinRand(16); ++ii) {

                GameObject myMoon = Instantiate(planet, myPlanet.transform.position, myPlanet.transform.rotation);
                myMoon.transform.SetParent(myPlanet.transform);
                myMoon.GetComponent<PlanetRenderer>().Render(GenMoon());

                myMoon.transform.Rotate(0, 0, PerlinRand(360));
                myMoon.transform.Translate(Vector3.up * PerlinRand(20));
            }
        }
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

    private PlanetRenderData GenMoon () {

        return new PlanetRenderData {

            size = PerlinRand(4) + 1,

            baseID = PerlinRand(PlanetRendererAssets.BaseRange()-1),
            overlayID = PerlinRand(PlanetRendererAssets.OverlayRange()-1),

            baseColor = new Color(
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f
            ),
            overlayColor = new Color(
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f
            )
        };
    }

    private PlanetRenderData GenPlanet () {

        return new PlanetRenderData {

            size = PerlinRand(8) + 1,

            baseID = PerlinRand(PlanetRendererAssets.BaseRange()-1),
            overlayID = PerlinRand(PlanetRendererAssets.OverlayRange()-1),

            baseColor = new Color(
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f
            ),
            overlayColor = new Color(
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f
            )
        };
    }

    private StarRenderData GenStar () {

        return new StarRenderData {

            size = PerlinRand(8) + 6,

            burstID = PerlinRand(StarRendererAssets.Range()-1),

            color = new Color(
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f,
                PerlinRand(255) / 255f
            )
        };
    }
}
