
using UnityEngine;

public class MinimapZoomer : MonoBehaviour {

    public GameObject bigmap, littlemap;

    private void Update () {

        if (Input.GetKeyDown(KeyCode.M)) {

            bigmap.SetActive(true);
            littlemap.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.M)) {

            bigmap.SetActive(false);
            littlemap.SetActive(true);
        }
    }
}
