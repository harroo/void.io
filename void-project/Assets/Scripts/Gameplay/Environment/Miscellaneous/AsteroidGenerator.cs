
using System;
using System.Collections.Generic;

using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

    public static AsteroidGenerator instance;
    private void Awake () { instance = this; }

    public int max;

    private float timer;

    private List<Vector2> poses = new List<Vector2>();

    private void Start () {

        for (int i = 0; i < 24; ++i) {

            poses.Add(
                new Vector2(
                    UnityEngine.Random.Range(-64, 64),
                    UnityEngine.Random.Range(-64, 64)
                )
            );
        }
    }

    private void Update () {

        if (timer < 0) { timer = 1.0f;

            foreach (var pos in poses) {

                for (int i = 0; i < 2; ++i) {

                    if (asos.Count >= max) return;

                    CreateAsteroid(
                        pos.x + UnityEngine.Random.Range(-6.0f, 9.0f),
                        pos.y + UnityEngine.Random.Range(-9.0f, 6.0f)
                    );
                }
            }

        } else timer -= Time.deltaTime;
    }

    private void CreateAsteroid (float x, float y) {

        byte[] asteroidData = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(x), 0, asteroidData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(y), 0, asteroidData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(UnityEngine.Random.Range(0f, 180f)), 0, asteroidData, 8, 4);

        TcpStream.Send_CreateObject(UnityEngine.Random.Range(-999999999, 999999999), UnityEngine.Random.Range(5, 8), asteroidData);
    }

    private List<AsteroidObject> asos = new List<AsteroidObject>();

    public void Add (AsteroidObject aso) { asos.Add(aso); }
    public void Remove (AsteroidObject aso) { asos.Remove(aso); }
}
