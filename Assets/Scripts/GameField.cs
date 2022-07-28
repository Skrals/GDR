using UnityEngine;
using UnityEngine.Events;

public class GameField : MonoBehaviour
{
    public event UnityAction<int> FieldIsReady;

    [SerializeField] Interacted[] _interactedTemplates; //[0] - empty 
    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;
    [SerializeField] private int _coinCount;
    [SerializeField] private int _spikeCount;
    [SerializeField] private float _scaleFactorOffset;

    private Interacted[,] _interactedArray;
    private int _coinCounter;
    private int _spikeCounter;
    private Vector2 _positionOffset;


    private void Awake()
    {
        _interactedArray = new Interacted[_xSize, _ySize];
        _positionOffset = new Vector2(_interactedTemplates[0].SpriteRenderer.bounds.size.x * _xSize / _scaleFactorOffset, _interactedTemplates[0].SpriteRenderer.bounds.size.y * _ySize / _scaleFactorOffset);
        GenerateField();
    }

    private void GenerateField()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        System.Random pickElement = new System.Random();

        for (int x = 0; x < _interactedArray.GetLength(0); x++)
        {
            for (int y = 0; y < _interactedArray.GetLength(1); y++)
            {
                int current = pickElement.Next(0, _interactedTemplates.Length);
                Vector2 interactedSize = _interactedTemplates[current].SpriteRenderer.bounds.size;
                Vector3 position = new Vector3(xPos + (interactedSize.x * x) - _positionOffset.x, yPos + (interactedSize.y * y) - _positionOffset.y);
                Interacted interacted = Instantiate(_interactedTemplates[current], transform.position, Quaternion.identity);
                interacted.transform.position = position;
                interacted.name = $"Interacted - {x} {y}";

                TypeCount(interacted, x, y);

                if (IsEqual(interacted))
                {
                    if (_coinCounter > _coinCount)
                    {
                        RemoveCurrent(interacted, ref y, ref _coinCounter);
                    }
                    else if (_spikeCounter > _spikeCount)
                    {
                        RemoveCurrent(interacted, ref y, ref _spikeCounter);
                    }
                }
                interacted.transform.parent = transform;
            }
        }

        FieldIsReady?.Invoke(_coinCount);
    }

    private bool IsEqual(Interacted current)
    {
        if (current.GetType() == typeof(NoneInteraction))
        {
            return false;
        }

        foreach (var interacted in _interactedTemplates)
        {
            if (current.GetType() == interacted.GetType())
            {
                return true;
            }
        }

        return false;
    }

    private void TypeCount(Interacted current, int x, int y)
    {
        if (current.GetType() == typeof(NoneInteraction))
        {
            current.name = $"Empty - {x} {y}";
            return;
        }

        if (current.GetType() == typeof(Coin))
        {
            _coinCounter++;
        }
        else if (current.GetType() == typeof(Spike))
        {
            _spikeCounter++;
        }
    }

    private void RemoveCurrent(Interacted current, ref int y, ref int counter)
    {
        Destroy(current.gameObject);
        y--;
        counter--;
    }
}
