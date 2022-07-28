using UnityEngine;

public class Coin : Interacted
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerPoint player))
        {
            player.gameObject.GetComponent<CoinCollector>().OnCoinCollected();
            Destroy(gameObject);
        }
    }
}
