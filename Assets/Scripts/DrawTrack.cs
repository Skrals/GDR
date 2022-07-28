using System.Collections.Generic;
using UnityEngine;

public class DrawTrack : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private List<Vector3> _pointsList;

    private void OnEnable()
    {
        _pointsList = new List<Vector3>();
        _playerControl.PointAdded += OnPointAdd;
        _playerControl.PointRemoved += OnPointRemove;
    }

    private void OnDisable()
    {
        _playerControl.PointAdded -= OnPointAdd;
        _playerControl.PointRemoved -= OnPointRemove;
    }

    private void OnDrawGizmos()
    {
        Vector3 preveousPount = transform.position;
        
        for (int i = 0; i < _pointsList.Count; i++)
        {
            Gizmos.DrawLine(preveousPount,_pointsList[i]);
            preveousPount = _pointsList[i];
        }
    }

    private void OnPointAdd(Vector3 point)
    {
        _pointsList.Add(point);
    }

    private void OnPointRemove()
    {
        _pointsList.RemoveAt(0);
    }
}
