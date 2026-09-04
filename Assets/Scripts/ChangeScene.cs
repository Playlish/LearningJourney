using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneByName(string sceneName)
    {
        //connect loading scenes in Unity Engine using their name
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSaveYes()
    {
        SaveAndLoad.loadActive = true;
    }
    public void QuitToDesktop()
    {
//end play mode when using Unity Engine form
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#else
//otherwise quit application when using application form
Application.Quit();
#endif
    }
}
