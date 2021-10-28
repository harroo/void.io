
using UnityEngine;

public class NetworkInterface : MonoBehaviour {

    public static NetworkInterface instance;
    private void Awake () { instance = this; }

    public void Start () {

        Console.Clear();

        if (GlobalValues.Hosting) {

            ServerLayer.Init();

        } else {

            ClientLayer.Init();
        }

        GlobalValues.LocalPlayerID = UnityEngine.Random.Range(111111111, 999999999);
        PlayerManager.instance.SetPlayerStart(GlobalValues.LocalPlayerID);
        TcpStream.Send_CreateObject(GlobalValues.LocalPlayerID, 0, PlayerManager.instance.GetPlayerData());

        //might be removed and used just as the default thing when we have multiple tech trees
        TcpStream.Send_ColliderUpdate(GlobalValues.LocalPlayerID, 1);
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
