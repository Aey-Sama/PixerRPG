using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Overworld");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
