using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioClip sceneClip;
    [SerializeField]
    private AudioClip quitClip;

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
        AudioSource.PlayClipAtPoint(sceneClip, this.transform.position, 5f);
    }

    public void QuitButton()
    {
        AudioSource.PlayClipAtPoint(quitClip, this.transform.position, 5f);
        Application.Quit();
    }
}
