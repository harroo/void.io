
using UnityEngine;
using UnityEngine.UI;

public class ClientInterface : MonoBehaviour {

    public static ClientInterface instance;
    private void Awake () { instance = this; }

    public void Connect (string address) {

        if (address == "") return;

        MainClient.ConnectToServer(address, 2486);

        UdpCore.Initialize();

        GlobalValues.RunningOnline = true;

        int localPlayerID = UnityEngine.Random.Range(111111111, 999999999);
        PlayerManager.instance.SetPlayerStart(localPlayerID);
        MainClient.SendObjectCreate(localPlayerID, 0, PlayerManager.instance.GetPlayerData());

        if (MainClient.connected)
            Console.Log(LogType.OK, "<color=green>Connected!</color>");
    }

    public void Disconnect () {

        MainClient.DisconnectFromServer();

        GlobalValues.RunningOnline = false;

        if (!MainClient.connected)
            Console.Log(LogType.OK, "<color=white>Disconnected.</color>");

        Cleanup();
    }

    public void CreateNewObject (int type) {

        MainClient.SendObjectCreate(Random.Range(111111111, 999999999), type);
    }

    public void CreateNewObject (int type, byte[] data) {

        MainClient.SendObjectCreate(Random.Range(111111111, 999999999), type, data);
    }

    public void Cleanup () {

        ObjectManager.instance.Clear();
    }

    private void Start () {

        Invoke("LateStart", 1.0f);
    }
    private void LateStart () {

        Connect(GameLoader.address);
    }
    private void Update () {

        MainClient.Tick();
    }
    private void OnDestroy () {

        Disconnect();
    }
}
