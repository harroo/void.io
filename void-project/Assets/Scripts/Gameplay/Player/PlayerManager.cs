
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

    private GameObject goCache;

    public void HideShip () {

        playerObject.SetActive(false);

        goCache = new GameObject("PLACE HOLDER");
        goCache.transform.position = playerObject.transform.position;
        playerObject.transform.position = new Vector3(-690, Random.Range(-690, 690), -690);
        Camera.main.gameObject.GetComponent<SmoothFollow>().target = goCache.transform;
    }

    public void ShowShip () {

        playerObject.SetActive(true);

        playerObject.transform.position = goCache.transform.position;
        Destroy(goCache);
        Camera.main.gameObject.GetComponent<SmoothFollow>().target = playerObject.transform;
    }
}

public static class PlayerRef {

    public static Transform transform => PlayerManager.instance.playerObject.transform;
    public static GameObject player => PlayerManager.instance.playerObject;
}
