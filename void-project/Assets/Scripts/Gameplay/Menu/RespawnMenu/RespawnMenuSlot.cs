
using UnityEngine;
using UnityEngine.UI;

public class RespawnMenuSlot : MonoBehaviour {

    public Image image;
    public Text text;

    private StarterOption option;

    public void SetData (StarterOption option) {

        this.option = option;

        image.sprite = option.icon;
        text.text = option.name;
    }

    public void Click () {

        PlayerStats.SetShipID(option.id);

        PlayerStats.instance.__forwardForce = option.forwardForce;
        PlayerStats.instance.__turnForce = option.turnForce;

        PlayerStats.instance.__brakePower = option.brakePower;
        PlayerStats.instance.__defaultDrag = option.defaultDrag;
        PlayerStats.instance.__defaultAngluarDrag = option.defaultAngluarDrag;

        PlayerStats.instance.__regenSpeed = option.regenSpeed;
        PlayerStats.instance.__playerHealth = option.playerHealth;
        PlayerStats.instance.__bulletDamage = option.bulletDamage;
        PlayerStats.instance.__reloadUpgrades = option.reloadUpgrades;

        TcpStream.Send_ColliderUpdate(GlobalValues.LocalPlayerID, option.colliderId);

        PlayerManager.instance.ShowShip();

        PlayerHealth.instance.ResetHealth();

        RespawnMenu.instance.Clear();
    }
}
