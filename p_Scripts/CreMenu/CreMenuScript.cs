using UnityEngine;
using UnityEngine.SceneManagement;

public class CreMenuScript : MonoBehaviour
{
    public GameObject ExitImage;
    public GameObject UIAll;
    public void ResetScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenExitImage()
    {
        UIAll.SetActive(false);
        ExitImage.SetActive(true);
    }
    public void CloseExitImage()
    {
        UIAll.SetActive(true);
        ExitImage.SetActive(false);
    }
    public void CLoseGame()
    {
        Application.Quit();
    }
}
