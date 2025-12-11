using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int nextScene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            LoadScenes();
    }

    private void LoadScenes()
    {
        SceneManager.LoadScene(nextScene);
    }
}
