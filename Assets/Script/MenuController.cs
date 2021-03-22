using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameStop()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

    }
    public void GameStart()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void ActiveUI()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }
}
