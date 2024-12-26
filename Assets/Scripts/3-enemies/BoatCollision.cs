using UnityEngine;

public class Boatcollision: MonoBehaviour 
{
    public KeyboardMoverByTile playerMover;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boat"))
        {
            playerMover.setBoat(true);
            Debug.Log("player has Boat");
            Destroy(other.gameObject);
        }
    }

}
