
using UnityEngine;

public class SpriteLookAt : MonoBehaviour {

    public bool lookAtMouse;

    public Transform target;

    private void Update () {

        if (lookAtMouse) {

            /* Danndx 2017 (youtube.com/danndx)
            From video: youtu.be/_XdqA3xbP2A
            thanks - delete me! :) */

            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

            transform.up = direction;

        } else if (target != null) {

            Vector2 direction = new Vector2(
                target.position.x - transform.position.x,
                target.position.y - transform.position.y
            );

            transform.up = direction;
        }
    }
}
