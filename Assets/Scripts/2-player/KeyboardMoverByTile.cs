using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] public TileBase replacementTile = null;
    [SerializeField] public TileBase mountainsTile = null;
    [SerializeField] public TileBase sea1Tile = null;
    [SerializeField] public TileBase sea2Tile = null;
    [SerializeField] public TileBase sea3Tile = null;

    private bool Pickaxe = false;
    private bool Boat = false;
    private bool Goat = false;

    public void setPickaxe(bool Pickaxe)
    {
        this.Pickaxe = Pickaxe;
    }
    public void setBoat(bool Boat)
    {
        this.Boat = Boat;
    }
    public void setGoat(bool Goat)
    {
        this.Goat = Goat;
    }


    [SerializeField] Tilemap tilemap = null;
//    [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    
    
    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
    // 
    private void SetTileOnPosition(Vector3 worldPosition, TileBase newTile)
    {
        // ממיר מיקום בעולם (World Position) למיקום תא (Cell Position) בטיילמאפ
        // המרה זו נדרשת מכיוון שהטיילמאפ מחלק את המרחב לרשת של תאים (Cells)
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        // מחליף את האריח (Tile) בתא שצויין
        // התא שבמיקום cellPosition יוחלף באריח החדש newTile
        tilemap.SetTile(cellPosition, newTile);
    }
    //
    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if(tileOnNewPosition== mountainsTile && Pickaxe)
        {
            SetTileOnPosition(newPosition, replacementTile);

        }
        if(Boat)
        {
            if(tileOnNewPosition==sea1Tile|| tileOnNewPosition== sea2Tile || tileOnNewPosition== sea3Tile)
            {
                transform.position = newPosition;
            }
        }
        if (Goat)
        {
            if (tileOnNewPosition == mountainsTile)
            {
                transform.position = newPosition;
            }
        }
        if (allowedTiles.Contains(tileOnNewPosition)) {
            transform.position = newPosition;
        } else {
            Debug.LogError("You cannot walk on " + tileOnNewPosition + "!");
        }

    }
}
