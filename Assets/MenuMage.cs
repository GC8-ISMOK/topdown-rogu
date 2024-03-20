using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMage : MonoBehaviour
{
    public void ButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void ButtonExit()
    {
        Application.Quit();
    }
}
