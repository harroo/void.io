
using UnityEngine;
using UnityEngine.UI;

public class ClientUIMethods : MonoBehaviour {

    public static ClientUIMethods instance;
    private void Awake () { instance = this; }

    public Text connectionStatus;

    public void Connect (InputField field) {

        if (field.text == "") return;

        MainClient.ConnectToServer(field.text, 2486);

        if (MainClient.connected) connectionStatus.text = "<color=green>Connected!</color>";
    }

    public void Disconnect () {

        MainClient.DisconnectFromServer();

        if (!MainClient.connected) connectionStatus.text = "<color=white>Not connected.</color>";

        Cleanup();
    }

    public void CreateNewObject () {

        MainClient.SendObjectCreate(Random.Range(111111111, 999999999));
    }

    public void Cleanup () {

        ObjectManager.instance.Clear();
    }

    private void Update () {

        MainClient.Tick();
    }
    private void OnDestroy () {

        Disconnect();
    }
}
