
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour {

    public InputField addressField;

    public void JoinGame () {

        if (addressField.text == "") return;

        GameLoader.hosting = false;
        GameLoader.address = addressField.text;
    }

    public void HostGame () {

        GameLoader.hosting = true;
        GameLoader.address = "localhost";
    }
}
