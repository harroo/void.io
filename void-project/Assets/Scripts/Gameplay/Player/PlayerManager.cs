
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    private void Awake () { instance = this; }

    public GameObject playerObject;

    public void SetPlayerStart (int playerID) {

        playerObject.GetComponent<Object>().ID = playerID;

        playerObject.SetActive(true);
    }

    public byte[] GetPlayerData () {

        return playerObject.GetComponent<Object>().GetData();
    }
}
