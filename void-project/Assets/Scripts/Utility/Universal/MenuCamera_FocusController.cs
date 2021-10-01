
using UnityEngine;

public class MenuCamera_FocusController : MonoBehaviour {

    public SmoothFollow sfollow;

    public void SetFocus (Transform target) {

        sfollow.target = target;
    }
}
