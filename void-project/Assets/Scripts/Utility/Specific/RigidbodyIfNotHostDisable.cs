
using UnityEngine;

public class RigidbodyIfNotHostDisable : MonoBehaviour {

    private void Start () {

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null) return;

        if (GlobalValues.Hosting) return;

        Destroy(rb);
    }
}
