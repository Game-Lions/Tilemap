using UnityEngine;
//using UnityEngine.SceneManagement;

public class PickaxeCollision : MonoBehaviour
{
    public KeyboardMoverByTile playerMover;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickaxe"))
        {
            playerMover.setPickaxe(true);
            Debug.Log("player has pickaxe");
            Destroy(other.gameObject);
        }
    }
    
}
