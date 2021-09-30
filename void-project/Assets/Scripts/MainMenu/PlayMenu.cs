
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour {

    public InputField addressField;

    public void JoinGame () {

        if (addressField.text == "") return;

        GlobalValues.Hosting = false;
        GlobalValues.Address = addressField.text;

        SceneManager.LoadScene("Game");
    }

    public void HostGame () {

        GlobalValues.Hosting = true;
        GlobalValues.Address = "localhost";

        SceneManager.LoadScene("Game");
    }
}
