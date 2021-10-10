
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDetector : MonoBehaviour {

    public GameObject upgradeOverlay;

    public Text pointsReminder;

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.25f;

            upgradeOverlay.SetActive(PlanetIndex.CheckIfInRange(transform.position, 0.24f) && MoveCheck());

        } else timer -= Time.deltaTime;


        pointsReminder.text =
            UpgradeMenu.instance.points > 0 ? "You've " +
            UpgradeMenu.instance.points.ToString() +
            " unspent point" +
            (UpgradeMenu.instance.points == 1 ? "" : "s") +
            ". Idle near a planet to spend " +
            (UpgradeMenu.instance.points == 1 ? "it" : "them") +
            "..." : "";
    }

    private bool MoveCheck ()
        => !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D);
}
