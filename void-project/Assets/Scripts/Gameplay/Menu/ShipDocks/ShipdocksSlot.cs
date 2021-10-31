
using UnityEngine;
using UnityEngine.UI;

public class ShipdocksSlot : MonoBehaviour {

    public Image image;
    public Text text;

    private ShipData option;

    public void SetData (ShipData option) {

        this.option = option;

        image.sprite = ShipRenderingAssets.Get(option.setId).idle;
        text.text = option.shipName;
    }

    public void Click () {

        Debug.Log("Clicked Upgrade: " + option.shipName);

        PlayerStats.SetShipID(option.shipId);

        PlayerStats.instance.__forwardForce = option.forwardForce;
        PlayerStats.instance.__turnForce = option.turnForce;

        PlayerStats.instance.__brakePower = option.brakePower;
        PlayerStats.instance.__defaultDrag = option.defaultDrag;
        PlayerStats.instance.__defaultAngluarDrag = option.defaultAngluarDrag;

        PlayerStats.instance.__regenSpeed = option.regenSpeed;
        PlayerStats.instance.__playerHealth = option.healthPoints;
        PlayerStats.instance.__bulletDamage = option.bulletDamage;
        PlayerStats.instance.__reloadUpgrades = option.reloadSpeed;

        TcpStream.Send_ColliderUpdate(GlobalValues.LocalPlayerID, option.colliderId);

        PlayerHealth.instance.ResetHealth();

        ShipdocksMenu.ResetUpgrades();

        ShipdocksMenu.Recalc();
    }
}
