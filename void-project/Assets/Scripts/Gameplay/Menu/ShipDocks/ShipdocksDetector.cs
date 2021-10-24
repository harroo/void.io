
using UnityEngine;

using System.Collections.Generic;

public class ShipdocksDetector : MonoBehaviour {

    public static ShipdocksDetector instance;
    private void Awake () { instance = this; }

    public GameObject overlay;
    public List<Transform> stations = new List<Transform>();

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.25f;

            overlay.SetActive(false);

            foreach (var station in stations) {

                if ((PlayerRef.transform.position - station.position).magnitude < 1.24f) {

                    overlay.SetActive(true);
                    return;
                }
            }

        } else timer -= Time.deltaTime;
    }
}
