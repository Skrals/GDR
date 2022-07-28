using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public event UnityAction<Vector3> PointAdded;
    public event UnityAction PointRemoved;

    [SerializeField] private float _speed;
    [SerializeField] private UIControl _uIControl;

    private List<Vector3> _pointsList;
    private Vector3 _targetPosition;
    private bool _isGameOver = false;

    public void SetIsGameOver(bool flag, string gameOverText)
    {
        _isGameOver = flag;
        _uIControl.GameOver(gameOverText);
    }

    private void Awake()
    {
        _pointsList = new List<Vector3>();
    }

    private void Update()
    {
        if (_isGameOver)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _targetPosition.z = 0;

            _pointsList.Add(_targetPosition);
            PointAdded?.Invoke(_targetPosition);
        }

        if (_pointsList.Count > 0)
        {
            Move(_pointsList[0]);
        }
    }

    private void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            _pointsList.RemoveAt(0);
            PointRemoved?.Invoke();
        }
    }
}
