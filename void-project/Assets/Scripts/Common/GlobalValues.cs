
public static class GlobalValues {

    public static bool RunningOnline;
    public static bool Hosting;

    public static string GetUsername () => UnityEngine.PlayerPrefs.GetString("USERNAME", "Player");
}
