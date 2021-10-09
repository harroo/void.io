
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    private void OnCollisionEnter2D (Collision2D collision) {

        if (collision.collider.tag == "DAMAGING") {

            Object obj = collision.collider.GetComponent<Object>();
            GlobalValues.LatestAttackingID = obj == null ? 0 : obj.GetAttackingID();

            PlayerHealth.Damage(collision.collider.GetComponent<Damager>().damage);

        } else if (collision.collider.tag != "INSTA_KILL") {

            Object obj = collision.collider.GetComponent<Object>();
            GlobalValues.LatestAttackingID = obj == null ? 0 : obj.GetAttackingID();

            PlayerHealth.Damage(69420); //insane amnt damage, instant kill
        }
    }
}
