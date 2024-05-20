using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap _floorMap, _wallMap;
    [SerializeField] private int _levelIndex;

    public void SaveMap()
    {
        var newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

        newLevel.LevelIndex = _levelIndex;
        newLevel.name = $"Level {_levelIndex}";

        newLevel.FloorTiles = GetTilesFromMap(_floorMap).ToList();
        newLevel.WallTiles = GetTilesFromMap(_wallMap).ToList();

        //ScriptableObjectUtility.SaveLevelFile(newLevel);
    }

    public void ClearMap()
    {
        var maps = FindObjectsOfType<Tilemap>();
        foreach (var tilemap in maps)
        {
            tilemap.ClearAllTiles();
        }
    }

    public void LoadMap()
    {
        var level = Resources.Load<ScriptableLevel>($"Assets/Resources/Levels/Level {_levelIndex}");
        if (level is null)
        {
            Debug.Break();
            Debug.LogError($"Level {_levelIndex} doesn't exist");
            return;
        }
        
        ClearMap();

        foreach (var savedTile in level.FloorTiles)
        {
            switch (savedTile.Tile.tileType)
            {
                case TileType.Floor:
                    _floorMap.SetTile(savedTile.Position, savedTile.Tile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        foreach (var savedTile in level.WallTiles)
        {
            switch (savedTile.Tile.tileType)
            {
                case TileType.Wall:
                    _floorMap.SetTile(savedTile.Position, savedTile.Tile);
                    _floorMap.SetColliderType(savedTile.Position, UnityEngine.Tilemaps.Tile.ColliderType.Grid);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public IEnumerable<SavedTile> GetTilesFromMap(Tilemap map)
    {
        foreach (var position in map.cellBounds.allPositionsWithin)
        {
            if (!map.HasTile(position)) continue;
            var levelTile = map.GetTile<LevelTile>(position);
            yield return new SavedTile
            {
                Position = position, Tile = levelTile
            };
        }        
    }
}

#if UNITY_EDITOR

public static class ScriptableObjectUtility
{
    public static void SaveLevelFile(ScriptableLevel level)
    {
        AssetDatabase.CreateAsset(level, $"Levels/{level.name}");
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

#endif
