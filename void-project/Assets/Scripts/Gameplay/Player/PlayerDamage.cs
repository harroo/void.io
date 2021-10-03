
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    public int hp;

    private int _hp;

    private void Start () {

        _hp = hp;
    }

    private void OnCollisionEnter2D (Collision2D collision) {

        if (collision.collider.tag == "BULLET") {

            _hp--;

            if (_hp <= 0) {

                EffectCreator.CreateEffect(2, transform.position, transform.eulerAngles);

                transform.position = Vector3.zero;
                _hp = hp;
            }

        } else if (collision.collider.tag != "PLAYER") {

            EffectCreator.CreateEffect(2, transform.position, transform.eulerAngles);

            transform.position = Vector3.zero;
        }
    }
}
