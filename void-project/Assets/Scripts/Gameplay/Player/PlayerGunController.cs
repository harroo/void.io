
using UnityEngine;

public class PlayerGunController : MonoBehaviour {

    public static PlayerGunController instance;
    private void Awake () { instance = this; }

    private void Update () {

        switch (ShipIndex.Get(PlayerStats.shipID).weaponType) {

            case WeaponType.Humon_Standard: default: Humon_Standard(); break;

            case WeaponType.Ruzbad_Standard: Ruzbad_Standard(); break;

            case WeaponType.Pirate_Standard: Pirate_Standard(); break;
        }
    }

    public float reloadTimer = 0.0f;

    private void StartReload (float val) {

        reloadTimer = val;
        ReloadBar.total = val;
    }

    private void Humon_Standard () {

        if (reloadTimer > 0) { reloadTimer -= Time.deltaTime * PlayerStats.reloadSpeed; return; }

        if (Input.GetMouseButton(0)) {

            StartReload(1.5f);

            var cache = transform.up;
            transform.up = getFaceMouseRotation();
            transform.Translate(Vector3.up * 0.15f);

            BulletCreator.CreateBullet(1, PlayerStats.bulletForce, PlayerStats.bulletDamage, transform.position, transform.eulerAngles);

            transform.Translate(Vector3.up * -0.15f);
            transform.up = cache;
        }
    }

    private void Ruzbad_Standard () {

        if (reloadTimer > 0) { reloadTimer -= Time.deltaTime * PlayerStats.reloadSpeed; return; }

        if (Input.GetMouseButton(0)) {

            StartReload(1.5f);

            var cache = transform.up;
            transform.up = getFaceMouseRotation();
            transform.Translate(Vector3.up * 0.15f);

            BulletCreator.CreateBullet(13, PlayerStats.bulletForce, PlayerStats.bulletDamage, transform.position, transform.eulerAngles);

            transform.Translate(Vector3.up * -0.15f);
            transform.up = cache;
        }
    }

    private void Pirate_Standard () {

        if (reloadTimer > 0) { reloadTimer -= Time.deltaTime * PlayerStats.reloadSpeed; return; }

        if (Input.GetMouseButton(0)) {

            StartReload(1.5f);

            var cache = transform.up;
            transform.up = getFaceMouseRotation();
            transform.Translate(Vector3.up * 0.17f);

            BulletCreator.CreateBullet(14, PlayerStats.bulletForce, PlayerStats.bulletDamage, transform.position, transform.eulerAngles);

            transform.Translate(Vector3.up * -0.17f);
            transform.up = cache;
        }
    }


    private Vector3 getFaceMouseRotation () {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        return new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
    }
}

public enum WeaponType {

    Humon_Standard,
    Ruzbad_Standard,
    Pirate_Standard
}
