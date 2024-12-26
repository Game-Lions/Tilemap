using UnityEngine;

public class GoatCollision: MonoBehaviour {
    public KeyboardMoverByTile playerMover;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goat"))
        {
            playerMover.setGoat(true);
            Debug.Log("player has Goat");
            Destroy(other.gameObject);
        }
    }

}
