
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
