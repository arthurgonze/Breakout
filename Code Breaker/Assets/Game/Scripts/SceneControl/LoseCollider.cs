using System.Collections;
using System.Collections.Generic;
using CB.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CB.SceneControl
{
    public class LoseCollider : MonoBehaviour
    {
        SceneLoader sceneLoader;

        void Start()
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(FindObjectOfType<GameStatus>().GetPlayerLives() > 1)
                FindObjectOfType<GameStatus>().LoseLife();
            else 
                LoadGameOverScene();
        }

        private void LoadGameOverScene()
        {
            string cena = "Game Over";
            Cursor.visible = true;
            sceneLoader.LoadScene(cena);
        }
    }
}

