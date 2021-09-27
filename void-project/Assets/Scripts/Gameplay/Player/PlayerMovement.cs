
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigidBody;

    private void Start () {

        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update () {

        brake = Input.GetKey(KeyCode.Space);

        forward = Input.GetKey(KeyCode.W) && !brake;
        left = Input.GetKey(KeyCode.A) && !brake;
        right = Input.GetKey(KeyCode.D) && !brake;
    }

    private bool forward, left, right, brake;

    private void FixedUpdate () {

        if (forward)
            rigidBody.AddForce(transform.up * PlayerStats.forwardForce);

        if (left)
            rigidBody.AddTorque(PlayerStats.turnForce);

        if (right)
            rigidBody.AddTorque(-PlayerStats.turnForce);

        rigidBody.drag =
            PlayerStats.defaultDrag + (brake ? PlayerStats.brakePower : 0.0f);

        rigidBody.angularDrag =
            PlayerStats.defaultAngluarDrag + (brake ? PlayerStats.brakePower : 0.0f);
    }
}
