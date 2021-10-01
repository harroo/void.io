
using UnityEngine;
using UnityEngine.UI;

public class InputFieldMemory : MonoBehaviour {

    private InputField field;
    private void Start () {

        field = GetComponent<InputField>();

        field.text = PlayerPrefs.GetString(gameObject.name, "");

        field.onEndEdit.AddListener(delegate{PlayerPrefs.SetString(gameObject.name, field.text);});
    }
}
