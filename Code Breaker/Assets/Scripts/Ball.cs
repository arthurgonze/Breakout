using UnityEngine;

public class Ball : MonoBehaviour
{
    //config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.42f;

    //state
    Vector2 paddleToBallVector;
    bool gameStarted = false;

    // Cached Component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    //configuration
    Vector2 initialPosition = new Vector2(8f, 0.85f);

    // Use this for initialization
    void Start ()
    {
        transform.position = initialPosition;
        paddleToBallVector = transform.position - paddle1.transform.position; //first param -> transform position of the ball
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!gameStarted)
        {
            LockToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush,yPush);
            gameStarted = true;
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(1f, randomFactor), Random.Range(1f, randomFactor));
        if(gameStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
