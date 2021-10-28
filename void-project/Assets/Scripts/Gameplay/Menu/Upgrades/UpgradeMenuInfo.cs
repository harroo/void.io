
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuInfo : MonoBehaviour {

    public Text antidrift, brakepower,
        autorepairs, hulldurab, projforce,
        projdamage, reloadspeed, thrusterpower;

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.25f;

            antidrift.text = (PlayerStats.defaultDrag + PlayerStats.defaultAngluarDrag).ToString();

            brakepower.text = PlayerStats.brakePower.ToString();

            autorepairs.text = PlayerStats.regenSpeed.ToString();

            hulldurab.text = PlayerStats.playerHealth.ToString();

            projforce.text = PlayerStats.bulletForce.ToString();

            projdamage.text = PlayerStats.bulletDamage.ToString();

            reloadspeed.text = PlayerStats.reloadSpeed.ToString();

            thrusterpower.text = PlayerStats.forwardForce.ToString();

        } else timer -= Time.deltaTime;
    }
}
