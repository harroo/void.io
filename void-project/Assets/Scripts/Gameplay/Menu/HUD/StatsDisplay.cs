
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour {

    public Slider lvlSlider, scoreSlider;
    public Text lvlText, scoreText;

    private void Update () {

        lvlText.text = PlayerStats.playerLevel.ToString();
        lvlSlider.value = (float)PlayerXPManager.instance.lvl / PlayerXPManager.instance.lvlNext;

        scoreText.text = PlayerXPManager.instance.scoreU == "" ?
            PlayerXPManager.instance.score.ToString("0") :
            PlayerXPManager.instance.score.ToString("0.0") + PlayerXPManager.instance.scoreU;
        // scoreSlider.value = PlayerXPManager.instance.score / PlayerXPManager.instance.lvlNext;

        //TEMP:
        // if(Input.GetKey(KeyCode.K))PlayerXPManager.AddXP(Random.Range(0, 234));
    }
}
