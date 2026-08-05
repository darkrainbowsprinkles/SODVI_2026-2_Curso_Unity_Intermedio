using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPS.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] float timeScale = 0.2f;
        [SerializeField] Button restartButton;
        [SerializeField] Button quitButton;

        void OnEnable()
        {
            Time.timeScale = timeScale;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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