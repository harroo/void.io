
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour {

    public static float total = 1.0f;

    public Slider slider;
    public Text text;
    public Image fill;

    private float timer;
    private void Update () {

        if (timer < 0) { timer = 0.1f;

            slider.value = (total / total) - (rtime / total);

            fill.color = Color.Lerp(Color.red, Color.green, slider.value);
            fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, 69/255f);

            text.text = rtime == 0.0f ? "<color=green>Ready!</color>" : "<color=#"+colorhex+"ff>Reloading...</color>";

        } else timer -= Time.deltaTime;
    }

    private float rtime
        => PlayerGunController.instance.reloadTimer < 0.0f ? 0.0f : PlayerGunController.instance.reloadTimer;

    private string colorhex
        => ColorUtility.ToHtmlStringRGB(Color.Lerp(Color.red, Color.green, (total / total) - (rtime / total)));
}
