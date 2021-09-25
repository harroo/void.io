
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public static bool hosting;
    public static string address;

    public GameObject serverObject;

    private void Start () {

        if (hosting) {

            serverObject.SetActive(true);

        } else {

            Destroy(serverObject);
        }
    }
}
