using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        
    }
    public void LoadNextScene()
    {
        Cursor.visible = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//faz a variavel guardar o indice da cena atual de jogo
        SceneManager.LoadScene(currentSceneIndex + 1);//chama a função de carregar uma cena e passa como parametro o indice atual + 1 para ir para próxima cena
    }

    public void FirstScreen()
    {
        gameStatus.Reset();
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

