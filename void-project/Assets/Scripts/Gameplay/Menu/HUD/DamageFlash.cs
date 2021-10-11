
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour {

    public float fadeTime;
    public Image image;

    private void Update () {

        if (image.enabled) {

            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - fadeTime*Time.deltaTime);
            if (image.color.a <= 0) image.enabled = false;
        }
    }

    public void Flash () {

        image.enabled = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.42f);
    }
}
