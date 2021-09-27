
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public static bool hosting = true;
    public static string address = "localhost";

    public GameObject serverObject, clientObject;

    private void Start () {

        if (hosting) {

            serverObject.SetActive(true);
            Destroy(clientObject);

        } else {

            clientObject.SetActive(true);
            Destroy(serverObject);
        }
    }
}
