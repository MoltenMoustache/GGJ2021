using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Intro")]
    [SerializeField] AudioSource theme;
    [SerializeField] float themeFadeOutTime;
    [Space(5)]
    [SerializeField] float startDelay = 1f;
    [SerializeField] CanvasGroup fadeImage;
    [SerializeField] float imageFadeTime;
    [Space(5)]
    [SerializeField] CanvasGroup title;
    [SerializeField] float titleFadeTime;
    [Space(5)]
    [SerializeField] CanvasGroup buttons;
    [SerializeField] float buttonFadeTime;
    [Header("Pausing")]
    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))   //well thats one way to do it lmao
            StartCoroutine(QueueIntro());

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            pauseMenu.SetActive(false); //just double check

    }

    #region Coroutines
    IEnumerator QueueIntro()
    {
        yield return new WaitForSeconds(startDelay);
        FadeImage(0, imageFadeTime);
        yield return new WaitForSeconds(imageFadeTime + .5f);
        FadeTitle();
    }

    IEnumerator QueueStartGame()
    {
        FadeImage(1, imageFadeTime);
        LeanTween.alphaCanvas(title, 0, 1);
        LeanTween.alphaCanvas(buttons, 0, 1);
        LeanTween.value(theme.volume, 0, themeFadeOutTime); //doesn't work
        yield return new WaitForSeconds(imageFadeTime);
        SceneManager.LoadScene(1);
    }

    IEnumerator QueueQuitGame()
    {
        LeanTween.alphaCanvas(fadeImage, 1, 1);
        LeanTween.alphaCanvas(title, 0, 1);
        LeanTween.alphaCanvas(buttons, 0, 1);
        yield return new WaitForSeconds(1.5f);
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region intro
    private void FadeImage(float alpha, float fadeTime)
    {
        LeanTween.alphaCanvas(fadeImage, alpha, fadeTime);
    }

    private void FadeTitle()
    {
        LeanTween.alphaCanvas(title, 1, titleFadeTime).setOnComplete(() => FadeButtons());
    }

    private void FadeButtons()
    {
        LeanTween.alphaCanvas(buttons, 1, buttonFadeTime);
    }
    #endregion

    #region button functions
    public void StartGame()
    {
        StartCoroutine(QueueStartGame());
    }

    public void QuitGameFromMainMenu()
    {
        StartCoroutine(QueueQuitGame());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

}
