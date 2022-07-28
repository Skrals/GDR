using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer PlayerSprite { get; private set; }
    [field: SerializeField] public ParticleSystem DeadEffect { get; private set; }
    [field: SerializeField] public PlayerControl Control { get; private set; }
}
