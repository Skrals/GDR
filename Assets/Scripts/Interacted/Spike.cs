using UnityEngine;

public class Spike : Interacted
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerPoint player))
        {
            player.PlayerSprite.sprite = null;
            player.DeadEffect.gameObject.SetActive(true);
            player.Control.SetIsGameOver(true, "You LOSE");
        }
    }
}
