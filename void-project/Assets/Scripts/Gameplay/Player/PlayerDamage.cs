
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    public int hp;

    private int _hp;

    private void Start () {

        _hp = hp;
    }

    private void OnCollisionEnter2D (Collision2D collision) {

        if (collision.collider.tag == "DAMAGING") {

            _hp -= collision.collider.GetComponent<Damager>().damage;

            if (_hp <= 0) {

                EffectCreator.CreateEffect(2, transform.position, transform.eulerAngles);

                transform.position = Vector3.zero;
                _hp = hp;
            }

        } else if (collision.collider.tag != "INSTA_KILL") {

            EffectCreator.CreateEffect(2, transform.position, transform.eulerAngles);

            transform.position = Vector3.zero;
        }
    }
}
