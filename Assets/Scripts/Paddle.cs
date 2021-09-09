using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float screenWidthInUnits = 16;

    private GameStatus _gameStatus;

    private Ball _ball;

    private float _minX = 1.0f;

    private float _maxX = 15.0f;

    private void Start()
    {
        _gameStatus = FindObjectOfType<GameStatus>();

        _ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        MovePaddleWithMouse();
    }

    private void MovePaddleWithMouse()
    {
        Vector3 paddlePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        paddlePosition.x = Mathf.Clamp(GetXPosition(), _minX, _maxX);
        transform.position = paddlePosition;
    }

    private float GetXPosition()
    {
        if (_gameStatus.IsAutoPlayEnabled())
        {
            return _ball.transform.position.x;
        }
        else
        {
            float mousePositionInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
            return mousePositionInUnits;
        }
    }
}
