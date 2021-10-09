
using UnityEngine;

public class UpgradeDetector : MonoBehaviour {

    public GameObject upgradeOverlay;

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.5f;

            upgradeOverlay.SetActive(PlanetIndex.CheckIfInRange(transform.position, 0.24f) && !Input.anyKey);

        } else timer -= Time.deltaTime;
    }
}
