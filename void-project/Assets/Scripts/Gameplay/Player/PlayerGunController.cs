
using UnityEngine;

public class PlayerGunController : MonoBehaviour {

    private void Update () {

        switch (PlayerStats.shipID) {

            case 1: default: s1_Tick(); break;
        }
    }

    private float reloadTimer;

    private void s1_Tick () {

        if (reloadTimer > 0) { reloadTimer -= Time.deltaTime * PlayerStats.reloadSpeed; return; }

        if (Input.GetMouseButton(0)) {

            reloadTimer = 1.5f;

            var cache = transform.up;
            transform.up = getFaceMouseRotation();
            transform.Translate(Vector3.up * 0.15f);

            BulletCreator.CreateBullet(1, PlayerStats.bulletForce, PlayerStats.bulletDamage, transform.position, transform.eulerAngles);

            transform.Translate(Vector3.up * -0.15f);
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
