
using UnityEngine;

public class PlayerXPManager : MonoBehaviour {

    public static PlayerXPManager instance;
    private void Awake () { instance = this; }

    public int xp, lvl;
    public float score;
    public string scoreU;

    public int lvlNext = 128;

    public static void AddXP (int amnt) {

        instance.xp += amnt;
        instance.score = instance.xp;
        instance.lvl += amnt;

        if (instance.lvl >= instance.lvlNext) {

            instance.lvl = 0;
            instance.lvlNext += 64;
            PlayerStats.SetPlayerLevel(PlayerStats.playerLevel + 1);
            UpgradeMenu.instance.points++;
        }

        if (instance.score > 999) {

            instance.score /= 1000; instance.scoreU = "K";

            if (instance.score > 999) {

                instance.score /= 1000; instance.scoreU = "M";
            }
        }
    }

    public static void Reset () {

        instance.xp = 0;
        instance.score = 0;
        instance.lvl = 0;
        instance.scoreU = "";
        instance.lvlNext = 100;
        PlayerStats.SetPlayerLevel(1);
        PlayerStats.SetShipID(1);
        UpgradeMenu.instance.points = 0;
        UpgradeMenu.instance.spentPoints = 0;
    }
}
