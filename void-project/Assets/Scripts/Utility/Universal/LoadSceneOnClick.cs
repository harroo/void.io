
using UnityEngine;

using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public string sceneToLoad;

    public void OnClick () {

        SceneManager.LoadScene(sceneToLoad);
    }
}
