using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NF.Main.UI 
{
    public class ReturnToMenu : MonoBehaviour
    {
        [SerializeField] private string mainMenuSceneName = "MainMenu";
        [SerializeField] private GameObject loadingScreen;

        public void LoadMainMenu()
        {
            StartCoroutine(LoadMainMenuRoutine());
        }

        private IEnumerator LoadMainMenuRoutine()
        {
            if (loadingScreen != null)
                loadingScreen.SetActive(true);

            Time.timeScale = 1f; // Reset in case it's paused (like in Game Over)

            // Optional short delay for visual effect
            yield return new WaitForSecondsRealtime(1f);

            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

}
