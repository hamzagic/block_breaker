using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Paddle _paddle1;

    [SerializeField]
    private AudioSource _source;

    private Rigidbody2D _myRigidBody2D;

    Vector3 paddleToBallVector;

    private bool _hasStarted = false;

    [SerializeField]
    private float xPush = 2f;

    [SerializeField]
    private float yPush = 12f;

    [SerializeField]
    private float randomness = 0.3f;

    [SerializeField]
    AudioClip[] ballSounds;
    void Start()
    {
        paddleToBallVector = transform.position - _paddle1.transform.position;
        _hasStarted = false;
        _source = GetComponent<AudioSource>();
        _myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!_hasStarted)
        {
            LaunchBallOnMouseClick();
            BallPaddleStartPosition();
        } else
        {
            
        }
    }

    private void BallPaddleStartPosition()
    {
        Vector3 paddlePosition = new Vector3(_paddle1.transform.position.x, _paddle1.transform.position.y + 0.5f, transform.position.z);
        transform.position = paddlePosition - paddleToBallVector + paddleToBallVector;
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _myRigidBody2D.velocity = new Vector2(xPush, yPush);
            _hasStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float randomDirection = Random.Range(0f, randomness);
        Vector2 velocityTweak = new Vector2(randomDirection, randomDirection);
        if (_hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            _source.PlayOneShot(clip);
            _myRigidBody2D.velocity += velocityTweak;
        }
    }
}
