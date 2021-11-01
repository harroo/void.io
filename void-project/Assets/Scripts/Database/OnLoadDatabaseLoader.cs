
using UnityEngine;

public class OnLoadDatabaseLoader : MonoBehaviour {

    private void Start () {

        ShipIndex.InitializeShips();
        ColliderIndex.InitializeColliders();
    }
}
