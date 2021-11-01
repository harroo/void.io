
using UnityEngine;
using UnityEngine.UI;

public class ShipdocksMenu : MonoBehaviour {

    public Text upgradeInfo;

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.25f;

            switch (ShipIndex.Get(PlayerStats.shipID).level) {

                case 1: Check(1); break;
                case 2: Check(22); break;
                case 3: Check(34); break;
                case 4: Check(46); break;
                case 5: Check(58); break;
                case 6: Check(69); break;
                case 7: Check(82); break;
            }

        } else timer -= Time.deltaTime;
    }

    public void Check (int amount) {

        if (PlayerStats.playerLevel >= amount)
            ShowAvailable();
        else
            ShowRequired(amount);
    }

    public GameObject slotPrefab;
    public Transform parent;

    private void ShowAvailable () {

        Clear();

        slotPrefab.SetActive(true);

        foreach (ShipData shipData in ShipIndex.shipData.Values) {

            if (shipData.parentId != PlayerStats.shipID) continue;

            GameObject slot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(parent);
            slot.transform.localScale = new Vector3(1,1,1);

            slot.GetComponent<ShipdocksSlot>().SetData(shipData);
        }

        slotPrefab.SetActive(false);
    }
    private void ShowRequired (int req) {

        Clear();

        upgradeInfo.text =
            "The next upgrades are available at level '" +
            req.ToString() +
            "'.";
    }
    private void Clear () {

        upgradeInfo.text = "";

        foreach (Transform child in parent)
            if (child != slotPrefab.transform)
                Destroy(child.gameObject);
    }

    public static void Recalc () {

        FindObjectOfType<ShipdocksMenu>().Clear();
    }
    public static void ResetUpgrades () {

        int spoints = UpgradeMenu.instance.spentPoints;

        UpgradeMenu.instance.Reset();

        UpgradeMenu.instance.points += Mathf.RoundToInt(spoints / 1.25f);
    }

    public static void SetDefault () {

        PlayerStats.SetShipID(1);

        PlayerStats.instance.__forwardForce = 2.0f;
        PlayerStats.instance.__turnForce = 0.02f;

        PlayerStats.instance.__brakePower = 2.0f;
        PlayerStats.instance.__defaultDrag = 1.0f;
        PlayerStats.instance.__defaultAngluarDrag = 2.0f;

        PlayerStats.instance.__regenSpeed = 1.0f;
        PlayerStats.instance.__playerHealth = 4;
        PlayerStats.instance.__bulletDamage = 1;
        PlayerStats.instance.__reloadUpgrades = 1.0f;

        TcpStream.Send_ColliderUpdate(GlobalValues.LocalPlayerID, 1);

        PlayerHealth.instance.ResetHealth();

        ResetUpgrades();
    }
}
