using System.Collections;
using System.Collections.Generic;
using CB.Core;
using UnityEngine;

namespace CB.SceneControl
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject leaderboardCanvas;
        [SerializeField] private GameObject optionsMenuCanvas;
        [SerializeField] private GameObject pauseMenuCanvas;

        void Update()
        {
            if(pauseMenuCanvas != null && !pauseMenuCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
                ShowPauseMenu();
        }

        public void ShowLeaderboard()
        {
            leaderboardCanvas?.SetActive(true);
        }

        public void HideLeaderboard()
        {
            leaderboardCanvas?.SetActive(false);
        }

        public void ShowOptionsMenu()
        {
            optionsMenuCanvas?.SetActive(true);
        }

        public void HideOptionsMenu()
        {
            optionsMenuCanvas?.SetActive(false);
        }

        private void ShowPauseMenu()
        {
            pauseMenuCanvas?.SetActive(true);
            FindObjectOfType<GameStatus>()?.PauseGame();
        }

        public void HidePauseMenu()
        {
            pauseMenuCanvas?.SetActive(false);
            FindObjectOfType<GameStatus>()?.ContinueGame();
        }

        public void ReturnToMainMenu()
        {
            FindObjectOfType<GameStatus>()?.ContinueGame();
            FindObjectOfType<GameStatus>()?.Reset();
            string cena = "Main Menu";
            Cursor.visible = true;
            FindObjectOfType<SceneLoader>()?.LoadScene(cena);
        }
    }
}
