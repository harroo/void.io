
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPrefSet : MonoBehaviour {

    public string valueTitle, defautlValue;

    private InputField field;
    private void Start () {

        field = GetComponent<InputField>();

        field.text = PlayerPrefs.GetString(valueTitle, defautlValue);

        field.onEndEdit.AddListener(delegate{PlayerPrefs.SetString(valueTitle, field.text);});
    }
}
