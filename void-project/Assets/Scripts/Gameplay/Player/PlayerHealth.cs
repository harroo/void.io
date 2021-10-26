
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth instance;
    private void Awake () { instance = this; }

    public int health;
    public int maxHealth {
        get => PlayerStats.playerHealth;
        set => PlayerStats.SetPlayerHealth(value);
    }

    public Slider healthBar;

    private void Start () {

        ResetHealth();
    }

    public void ResetHealth () {

        healthBar.maxValue = maxHealth;
        health = maxHealth;
        healthBar.value = health;
    }

    private float regenTimer;
    private void Update () {

        healthBar.value = health;

        if (regenTimer < 0) { regenTimer = 8.0f;

            Heal(1);

        } else regenTimer -= Time.deltaTime * PlayerStats.regenSpeed;
    }

    public static void Damage (int amount) {

        instance.health -= amount;
        instance.health = Mathf.Clamp(instance.health, 0, instance.maxHealth);

        instance.DeathCheck();

        FindObjectOfType<DamageFlash>().Flash();
    }

    public static void Heal (int amount) {

        instance.health += amount;
        instance.health = Mathf.Clamp(instance.health, 0, instance.maxHealth);

        instance.DeathCheck();
    }

    private void DeathCheck () {

        if (health > 0) return;

        EffectCreator.CreateEffect(2, transform.position, transform.eulerAngles);

        transform.position = Vector3.zero;

        ResetHealth();
        UpgradeMenu.instance.Reset();

        if (GlobalValues.LatestAttackingID != 0) {

            TcpStream.Send_AwardXP(GlobalValues.LatestAttackingID, 100 + (PlayerXPManager.instance.xp / 2));
            GlobalValues.LatestAttackingID = 0;
        }

        int xpCache = PlayerXPManager.instance.xp / 3;
        PlayerXPManager.Reset();
        PlayerXPManager.AddXP(xpCache);

        ShipdocksMenu.SetDefault();
    }
}
