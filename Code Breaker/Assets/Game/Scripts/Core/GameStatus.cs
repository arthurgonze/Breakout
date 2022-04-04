using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CB.Core
{
    public class GameStatus : MonoBehaviour
    {
        //config params
        [Header("-------------------------------- Score Params --------------------------------")]
        [SerializeField] int pointsPerBlockDestroyed = 42;
        [SerializeField] TextMeshProUGUI scoreText;
        
        [Space][Header("-------------------------------- Configs Params --------------------------------")]
        [Range(0f, 1f)][SerializeField] float gameSpeed = 1f;
        [SerializeField] bool isMenu = false;
        [SerializeField] bool preserveSingleton = false;

        [Space][Header("-------------------------------- Player Params --------------------------------")]
        [SerializeField] bool isAutoPlayEnabled = false;
        [SerializeField] int playerLives = 3;
        [SerializeField] List<Image> playerLivesUI = new List<Image>();

        // state variables
        [SerializeField] int score = 0;

        private void Awake()
        {
            if (isMenu)
                isAutoPlayEnabled = true;
            
            // if(!preserveSingleton) return;

            // Singleton pattern
            int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
            if (gameStatusCount > 1)
            {
                Debug.Log("Already exist a game status, detroy me");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("There is no other game status, dont detroy me");
                gameObject.transform.DetachChildren();
                DontDestroyOnLoad(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
            scoreText?.SetText(score.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            if (isMenu) return;
            Time.timeScale = gameSpeed;

            CheckScoreTextReference();
            CheckPlayerLivesImagesReference();
            if(int.Parse(scoreText?.text) > score)
                scoreText.text = score.ToString();
        }

        private void CheckScoreTextReference()
        {
            if(scoreText == null)
            {
                scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
                scoreText.text = score.ToString();
            }
        }

        private void CheckPlayerLivesImagesReference()
        {
            if(playerLivesUI[0] == null || playerLivesUI[1] == null || playerLivesUI[2] == null)
            {
                playerLivesUI[0] = GameObject.FindGameObjectWithTag("Life_0").GetComponent<Image>();
                playerLivesUI[1] = GameObject.FindGameObjectWithTag("Life_1").GetComponent<Image>();
                playerLivesUI[2] = GameObject.FindGameObjectWithTag("Life_2").GetComponent<Image>();
                UpdateLifeImages();
            }
        }

        public void CheckIsMenu()
        {
            if(FindObjectOfType<Level>())
                isMenu = false;
            else
                isMenu = true;
        }

        public void AddToScore()
        {
            if(isMenu) return;
            score += pointsPerBlockDestroyed;
            scoreText?.SetText(score.ToString());
        }

        public void LoseLife()
        {
            playerLives--;
            playerLivesUI[playerLives].enabled = false;
            FindObjectOfType<Paddle>().ResetPaddlePos();
            foreach (Ball ball in FindObjectsOfType<Ball>())
                ball.ResetBall(); 
        }

        public void AddLife()
        {
            if(playerLives < 3 && playerLives > 0)
                playerLives++;
            playerLivesUI[playerLives-1].enabled = true;
        }

        private void UpdateLifeImages()
        {
            for(int i = 0; i < 3; i++)
                if(i > playerLives-1)
                    playerLivesUI[i].enabled = false;
        }

        public int GetPlayerLives()
        {
            return playerLives;
        }

        public void Reset()
        {
            Destroy(gameObject);
        }

        public bool IsAutoPlayEnabled()
        {
            return isAutoPlayEnabled;
        }

        public bool IsMenu()
        {
            return isMenu;
        }

        public void PauseGame()
        {
            gameSpeed = 0;
            foreach (Ball ball in FindObjectsOfType<Ball>())
                ball.ToggleLockBallLaunch(true);
            FindObjectOfType<Paddle>().ToggleLockPaddle(true);
        }

        public void ContinueGame()
        {
            foreach (Ball ball in FindObjectsOfType<Ball>())
                ball.ToggleLockBallLaunch(false);
            FindObjectOfType<Paddle>().ToggleLockPaddle(false);

            gameSpeed = 1;
        }

        public int GetScore()
        {
            return score;
        }
    }
}
