
using UnityEngine;

public static class LilBootHandler {

    [RuntimeInitializeOnLoadMethod]
    public static void Boot () {

        Application.targetFrameRate = 240;
    }
}
