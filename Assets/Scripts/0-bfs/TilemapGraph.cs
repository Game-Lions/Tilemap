using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph: IGraph<Vector3Int> {
    private Tilemap tilemap;
    private TileBase[] allowedTiles;

    public TilemapGraph(Tilemap tilemap, TileBase[] allowedTiles) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    static Vector3Int[] directions = {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
    };
    //static Vector3Int[] evenRowDirections = {
    //    new Vector3Int(-1, 0, 0),
    //    new Vector3Int(-1, 1, 0),
    //    new Vector3Int(0, -1, 0),
    //    new Vector3Int(0, 1, 0),
    //    new Vector3Int(1, 0, 0),
    //    new Vector3Int(1, 1, 0)
    //};

    //static Vector3Int[] oddRowDirections = {
    //    new Vector3Int(-1, -1, 0),
    //    new Vector3Int(-1, 0, 0),
    //    new Vector3Int(0, -1, 0),
    //    new Vector3Int(0, 1, 0),
    //    new Vector3Int(1, -1, 0),
    //    new Vector3Int(1, 0, 0)
    //};


    //private Vector3Int[] GetMovementDirections(Vector3Int currentPos)
    //{
    //    // התאמת הכיוונים לשורות זוגיות ואי-זוגיות
    //    return (currentPos.y % 2 == 0) ? evenRowDirections : oddRowDirections;
    //}


    public IEnumerable<Vector3Int> Neighbors(Vector3Int node) {
        //Vector3Int[] directions = GetMovementDirections(node);
        foreach (var direction in directions) {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            Debug.Log($"Checking neighbor at {neighborPos}, Tile: {neighborTile?.name}");
            if (allowedTiles.Contains(neighborTile))
                yield return neighborPos;
        }
    }
}
