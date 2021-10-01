
using UnityEngine;
using UnityEngine.UI;

public class VersionTextSetter : MonoBehaviour {

    private void Start () {

        GetComponent<Text>().text = Application.version;
    }
}
