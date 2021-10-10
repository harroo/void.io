
using UnityEngine;

public class ThermonuclearDetector : MonoBehaviour {

    public GameObject overlay;

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.5f;

            overlay.SetActive(StarIndex.CheckIfInRange(transform.position, 0.68f));

            if (overlay.activeSelf)
                if (StarIndex.CheckIfInRange(transform.position, 0.32f))
                    if (Random.Range(0, 4) == 0) PlayerHealth.Damage(1);

        } else timer -= Time.deltaTime;
    }
}
