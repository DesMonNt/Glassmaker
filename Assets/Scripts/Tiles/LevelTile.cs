using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Tile", menuName = "2D/Tiles/LevelTile")]
public class LevelTile : UnityEngine.Tilemaps.Tile
{
    public TileType tileType;
}

[Serializable]
public enum TileType
{
    Floor = 0,
    Wall = 1,
}
