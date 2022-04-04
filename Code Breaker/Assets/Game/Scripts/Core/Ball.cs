using UnityEngine;

namespace CB.Core
{
    public class Ball : MonoBehaviour
    {
        //config parameters
        [SerializeField] Paddle paddle1;
        [SerializeField] Vector2 push = new Vector2(2f, 15f);
        [SerializeField] Vector2 initialPosition = new Vector2(8f, 0.85f);
        [SerializeField] AudioClip[] ballSounds;
        [SerializeField] float randomFactor = 1.42f;

        //state
        Vector2 paddleToBallVector;
        bool gameStarted = false;
        bool lockBallLaunch = false;
        // Cached Component references
        AudioSource myAudioSource;
        Rigidbody2D myRigidBody2D;
        GameStatus theGameStatus;

        void Awake()
        {
            theGameStatus = FindObjectOfType<GameStatus>();
            myAudioSource = GetComponent<AudioSource>();
            myRigidBody2D = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {
            transform.position = initialPosition;
            paddleToBallVector = transform.position - paddle1.transform.position; //first param -> transform position of the ball
        }

        // Update is called once per frame
        void Update()
        {
            if (lockBallLaunch || gameStarted) return;

            LockToPaddle();
            LaunchOnMouseClick();
        }

        public void ResetBall()
        {
            transform.position = initialPosition;
            paddleToBallVector = transform.position - paddle1.transform.position; //first param -> transform position of the ball
            gameStarted = false;
        }

        private void LaunchOnMouseClick()
        {
            if (theGameStatus.IsMenu() || Input.GetMouseButtonDown(0))
                LaunchBall();
        }

        public void LaunchBall()
        {
            Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
            myRigidBody2D.velocity = new Vector2(push.x, push.y);
            gameStarted = true;
        }

        private void LockToPaddle()
        {
            Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
            transform.position = paddlePos + paddleToBallVector;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!gameStarted) return;

            PlayBallSound();
            UpdateBallVelocity();
        }

        private void PlayBallSound()
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }

        private void UpdateBallVelocity()
        {
            Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor/2), Random.Range(0f, randomFactor));
            myRigidBody2D.velocity += velocityTweak;
        }

        public void ToggleLockBallLaunch(bool toggle)
        {
            lockBallLaunch = toggle;
        }
    }
}

