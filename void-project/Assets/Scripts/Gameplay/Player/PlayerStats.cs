
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;
    private void Awake () { instance = this; }

    private int __textureIndex;
    public static int textureIndex => instance.__textureIndex;
    public static void SetTextureIndex (int index) { instance.__textureIndex = index; }


    private int __playerLevel = 1;
    public static int playerLevel => instance.__playerLevel;
    public static void SetPlayerLevel (int level) { instance.__playerLevel = level; }


    private int __shipID = 1;
    public static int shipID => instance.__shipID;
    public static void SetShipID (int id) { instance.__shipID = id; }

//MOVEMENT


    public float rocketUpgrades;

    public float __forwardForce = 2.0f;
    public static float forwardForce => instance.__forwardForce + (instance.dragUpgrades/1.4f) + instance.rocketUpgrades;


    public float __turnForce = 0.02f;
    public static float turnForce => instance.__turnForce + (instance.dragUpgrades*0.004f) + (instance.rocketUpgrades*0.01f);


    public float brakeUpgrades;

    public float __brakePower = 2.0f;
    public static float brakePower => instance.__brakePower + instance.brakeUpgrades;


    public float dragUpgrades;

    public float __defaultDrag = 1.0f;
    public static float defaultDrag => instance.__defaultDrag + instance.dragUpgrades;


    public float __defaultAngluarDrag = 2.0f;
    public static float defaultAngluarDrag => instance.__defaultAngluarDrag + (instance.dragUpgrades*1.68f);

//HEALTH

    public float regenUpgrades;

    public float __regenSpeed = 1.0f;
    public static float regenSpeed => instance.__regenSpeed + instance.regenUpgrades;


    public int healthUpgrades;

    public int __playerHealth = 4;
    public static int playerHealth => instance.__playerHealth + instance.healthUpgrades;
    public static void SetPlayerHealth (int level) { instance.__playerHealth = level; }

//COMBAT

    public int bulletDamageUpgrades;

    public int __bulletDamage = 1;
    public static int bulletDamage => instance.__bulletDamage + instance.bulletDamageUpgrades;
    public static void SetBulletDamage (int level) { instance.__bulletDamage = level; }


    public int bulletForceUpgrades;

    public static int bulletForce => instance.bulletForceUpgrades;


    public float reloadUpgrades;

    public float __reloadUpgrades = 1.0f;
    public static float reloadSpeed => instance.__reloadUpgrades + instance.reloadUpgrades;
}
