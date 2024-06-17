using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UI_MainMenu : MonoBehaviour
    {
        [SerializeField] private string sceneName = "Level1";
        [SerializeField] private UI_FadeScreen fadeScreen;
        [SerializeField] private GameObject continuteButton;

        private void Start()
        {

        }

        public void ContinueGame() => StartCoroutine(LoadSceneWithFadeEffect(1.5f));

        public void NewGame() => StartCoroutine(LoadSceneWithFadeEffect(1.5f));

        public void ExitGame() => Application.Quit();

        IEnumerator LoadSceneWithFadeEffect(float delay)
        {
            fadeScreen.FadeOut();

            yield return new WaitForSeconds(delay);

            SceneManager.LoadScene(sceneName);
        }
    }
}