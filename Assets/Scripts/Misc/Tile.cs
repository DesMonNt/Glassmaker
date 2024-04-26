using System.Collections;
using System.Collections.Generic;
using Misc;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileType type;
    private int _x, _y;
    private bool _isSolid;
    public GameObject obj;
    

    public Tile(TileType type, int x, int y)
    {
        this.type = type;
        _x = x;
        _y = y;
        _isSolid = type == TileType.Wall;
    }
    
    
}
