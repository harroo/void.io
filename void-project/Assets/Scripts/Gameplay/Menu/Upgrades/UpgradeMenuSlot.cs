
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuSlot : MonoBehaviour {

    public Text text;
    public Slider slider;
    [Space()]
    public string upgradeName;
    public int max;

    private void Start () {

        text.text = upgradeName;
        slider.maxValue = max;
        slider.value = 0;
    }

    public void OnClick () {

        UpgradeMenu.instance.SlotClicked(slider, upgradeName, max);
    }
}
