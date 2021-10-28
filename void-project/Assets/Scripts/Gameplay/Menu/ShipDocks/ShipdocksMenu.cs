
using UnityEngine;
using UnityEngine.UI;

public class ShipdocksMenu : MonoBehaviour {

    public Text upgradeInfo;

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.25f;

            switch (PlayerStats.shipID) {

                case 1: {

                    if (PlayerStats.playerLevel >= 10)
                        ShowAvailable();
                    else
                        ShowRequired(10);

                break; }
            }

        } else timer -= Time.deltaTime;
    }

    public GameObject slotPrefab;
    public Transform parent;
    public UpgradeOption[] options;

    private void ShowAvailable () {

        Clear();

        slotPrefab.SetActive(true);

        foreach (var option in options) {

            if (option.parentId == PlayerStats.shipID) {

                GameObject slot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                slot.transform.SetParent(parent);
                slot.transform.localScale = new Vector3(1,1,1);

                slot.GetComponent<ShipdocksSlot>().SetData(option);
            }
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

        int points = UpgradeMenu.instance.points;

        UpgradeMenu.instance.Reset();

        UpgradeMenu.instance.points = Mathf.RoundToInt(points / 1.25f);
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

[System.Serializable]
public class UpgradeOption {

    public string name;
    public Sprite icon;

    public int parentId;
    public int id;

    [Space()]
    public float forwardForce = 2.0f;
    public float turnForce = 0.02f;

    [Space()]
    public float brakePower = 2.0f, defaultDrag = 1.0f, defaultAngluarDrag = 2.0f;

    [Space()]
    public float regenSpeed = 1.0f;
    public int playerHealth = 4, bulletDamage = 1;
    public float reloadUpgrades = 1.0f;

    [Space()]
    public int colliderId = 1;
}
