
using UnityEngine;

public class NetworkInterface : MonoBehaviour {

    public static NetworkInterface instance;
    private void Awake () { instance = this; }

    public void Start () {

        if (GlobalValues.Hosting) {

            ServerLayer.Init();

        } else {

            ClientLayer.Init();
        }

        GlobalValues.LocalPlayerID = UnityEngine.Random.Range(111111111, 999999999);
        PlayerManager.instance.SetPlayerStart(GlobalValues.LocalPlayerID);
        TcpStream.Send_CreateObject(GlobalValues.LocalPlayerID, 0, PlayerManager.instance.GetPlayerData());
    }

    public void Update () {

        if (GlobalValues.Hosting) {

            ServerLayer.Tick();

        } else {

            ClientLayer.Tick();
        }
    }

    public void OnDestroy () {

        if (GlobalValues.Hosting) {

            ServerLayer.Kill();

        } else {

            ClientLayer.Kill();
        }
    }
}
