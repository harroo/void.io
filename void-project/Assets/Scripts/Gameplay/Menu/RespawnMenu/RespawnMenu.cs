
using UnityEngine;
using UnityEngine.UI;

public class RespawnMenu : MonoBehaviour {

    public static RespawnMenu instance;
    private void Awake () { instance = this; }

    public void Activate () {

        PlayerManager.instance.HideShip();

        RenderDisplay();
    }

    public GameObject slotPrefab;
    public Transform parent;
    public StarterOption[] options;

    private void RenderDisplay () {

        Clear();

        slotPrefab.SetActive(true);

        foreach (ShipData shipData in ShipIndex.shipData.Values) {

            if (shipData.parentId != -1) continue;

            GameObject slot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(parent);
            slot.transform.localScale = new Vector3(1,1,1);

            slot.GetComponent<RespawnMenuSlot>().SetData(shipData);
        }

        slotPrefab.SetActive(false);
    }
    public void Clear () {

        foreach (Transform child in parent)
            if (child != slotPrefab.transform)
                Destroy(child.gameObject);
    }
}

[System.Serializable]
public class StarterOption {

    public string name;
    public Sprite icon;

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
