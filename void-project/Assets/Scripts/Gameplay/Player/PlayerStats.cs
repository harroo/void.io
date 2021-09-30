
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;
    private void Awake () { instance = this; }

    private int __textureIndex;
    public static int textureIndex => instance.__textureIndex;
    public static void SetTextureIndex (int index) { instance.__textureIndex = index; }

    private int __playerLevel;
    public static int playerLevel => instance.__playerLevel;
    public static void SetPlayerLevel (int level) { instance.__playerLevel = level; }

    private float __forwardForce = 2.0f;
    private float __turnForce = 0.02f;
    private float __brakePower = 2.0f;
    private float __defaultDrag = 1.0f;
    private float __defaultAngluarDrag = 2.0f;
    public static float forwardForce => instance.__forwardForce;
    public static float turnForce => instance.__turnForce;
    public static float brakePower => instance.__brakePower;
    public static float defaultDrag => instance.__defaultDrag;
    public static float defaultAngluarDrag => instance.__defaultAngluarDrag;
}
