using System.Collections;
using System.Collections.Generic;
using CB.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CB.SceneControl
{
    public class SceneLoader : MonoBehaviour
    {
        GameStatus gameStatus;

        private void Start()
        {
            gameStatus = FindObjectOfType<GameStatus>();
        }

        public void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//faz a variavel guardar o indice da cena atual de jogo
            SceneManager.LoadScene(currentSceneIndex + 1);//chama a função de carregar uma cena e passa como parametro o indice atual + 1 para ir para próxima cena
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadScene(int sceneIdx)
        {
            SceneManager.LoadScene(sceneIdx);
        }

        public void FirstScene()
        {
            gameStatus.Reset();

            Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
            Cursor.visible = true;

            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}


