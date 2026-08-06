using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPS.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] float timeScale = 0.2f;
        [SerializeField] float interactionDelay = 1f;
        [SerializeField] Button restartButton;
        [SerializeField] Button quitButton;

        void OnEnable()
        {
            Time.timeScale = timeScale;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(SetInteractionRoutine());
            restartButton.onClick.AddListener(ReloadScene);
            quitButton.onClick.AddListener(QuitGame);
        }

        void OnDisable()
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            restartButton.onClick.RemoveListener(ReloadScene);
            quitButton.onClick.RemoveListener(QuitGame);
        }

        IEnumerator SetInteractionRoutine()
        {
            restartButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(interactionDelay);
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }

        void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        void QuitGame()
        {
            # if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            # else
                Application.Quit();
            # endif
        }
    }
}