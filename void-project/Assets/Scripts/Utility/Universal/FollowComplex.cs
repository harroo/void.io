
using UnityEngine;

public class FollowComplex : MonoBehaviour {

    private Transform parent;

    private void Start () {

        parent = transform.parent;
        transform.parent = null;
    }

    public bool killOnParentNull;
    public bool overridePos;
    public Vector3 posOverride;
    public bool overrideRot;
    public Vector3 rotOverride;

    private void Update () {

        if (parent == null) {

            if (killOnParentNull) Destroy(this.gameObject);
            return;
        }

        if (!overridePos) transform.position = parent.position;
        else transform.position = posOverride;

        if (!overrideRot) transform.rotation = parent.rotation;
        else transform.eulerAngles = rotOverride;
    }
}
