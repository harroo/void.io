
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour {

    public InputField addressField;

    public void JoinGame () {

        if (addressField.text == "") return;

        GameLoader.hosting = false;
        GameLoader.address = addressField.text;

        SceneManager.LoadScene("Game");
    }

    public void HostGame () {

        GameLoader.hosting = true;
        GameLoader.address = "localhost";

        SceneManager.LoadScene("Game");
    }
}
