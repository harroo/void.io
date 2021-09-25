
using UnityEngine;
using UnityEngine.UI;

public class TittleSetter : MonoBehaviour {

    private void Start () {

        GetComponent<Text>().text =
            Application.productName + " " + Application.version;

            //put this here cos im lazy lmao
            Application.targetFrameRate = 60;
    }
}
