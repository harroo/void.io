
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour {

    public static UpgradeMenu instance;
    private void Awake () { instance = this; }

    public int points;

    private List<Slider> sliderCache = new List<Slider>();

    public Text pointsDisplay;

    private void Update () {

        pointsDisplay.text = points > 0 ? "x" + points.ToString() : "";
    }

    public void SlotClicked (Slider slider, string upgradeName, int max) {

        Console.Log("UPGRADE: " + upgradeName);

        if (slider.value == max) return;

        if (!sliderCache.Contains(slider)) sliderCache.Add(slider);

        if (points <= 0) return;

        points--;
        slider.value++;

        switch (upgradeName) {

            case "Anti-Drift.": PlayerStats.instance.dragUpgrades += 0.5f; break;

            case "Brake Power.": PlayerStats.instance.brakeUpgrades += 0.24f; break;

            case "Automatic Repairs.": PlayerStats.instance.regenUpgrades += 0.24f; break;

            case "Hull Durability.": PlayerStats.instance.healthUpgrades += 2; break;

            case "Projectile Force.": PlayerStats.instance.bulletForceUpgrades += 2; break;

            case "Projectile Damage.": PlayerStats.instance.bulletDamageUpgrades += 1; break;

            case "Reload Speed.": PlayerStats.instance.reloadUpgrades += 0.1f; break;

            case "Thruster Power.": PlayerStats.instance.rocketUpgrades += 0.2f; break;
        }
    }

    public void Reset () {

        PlayerStats.instance.bulletDamageUpgrades = 0;
        PlayerStats.instance.bulletForceUpgrades = 0;
        PlayerStats.instance.dragUpgrades = 0;
        PlayerStats.instance.reloadUpgrades = 0;
        PlayerStats.instance.brakeUpgrades = 0;
        PlayerStats.instance.regenUpgrades = 0;
        PlayerStats.instance.healthUpgrades = 0;
        PlayerStats.instance.rocketUpgrades = 0;
        foreach (var s in sliderCache)
            s.value = 0;
    }
}
