
using UnityEngine;
using UnityEngine.UI;

public class RectAnchor : MonoBehaviour {

    public Vector2 viewportPos;

    public bool snapOnUpdate, snapOnStart, snapOnWindowAdjust;

    private RectTransform rTransform;

    private void Start () {

        rTransform = GetComponent<RectTransform>();

        if (snapOnStart) CalculatePos();
    }

    private int screenCache;

    private void Update () {

        if (snapOnUpdate) CalculatePos();

        if (snapOnWindowAdjust) {

            if (screenCache != Screen.width + Screen.height) {

                screenCache = Screen.width + Screen.height;

                CalculatePos();
            }
        }
    }

    private void CalculatePos () {

        rTransform.position = new Vector2(
            Screen.width / (viewportPos.x * 4),
            Screen.height / (viewportPos.y * 4)
        );
    }
}
