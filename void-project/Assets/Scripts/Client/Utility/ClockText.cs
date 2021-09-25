
using UnityEngine;
using UnityEngine.UI;

public class ClockText : MonoBehaviour {

    private Text text;

    private void Start () {

        text = GetComponent<Text>();
    }

    private void Update () {

        text.text = System.DateTime.Now.ToString();
    }
}
