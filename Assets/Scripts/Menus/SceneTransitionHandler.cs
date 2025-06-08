using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace NF.Main.UI
{
    public class SceneTransitionHandler : MonoBehaviour
    {
        [Header("Loading Screen")]
        [SerializeField] private GameObject loadingScreen;

        [Header("Scene Names")]
        [SerializeField] private string mainMenuSceneName;

        /// <summary>
        /// Called by Main Menu button
        /// </summary>
        public void LoadMainMenu()
        {
            StartCoroutine(LoadSceneRoutine(mainMenuSceneName));
        }

        /// <summary>
        /// Called by Restart button
        /// </summary>
        public void RestartLevel()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            StartCoroutine(LoadSceneRoutine(currentScene));
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            if (loadingScreen != null)
                loadingScreen.SetActive(true);

            Time.timeScale = 1f; // Reset time in case game is paused

            // Optional delay for visual effect
            yield return new WaitForSecondsRealtime(1f);

            SceneManager.LoadScene(sceneName);
        }
    }
}
