using System;
using Unity;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        player.transform.position = Saves.playerPosition;
        foreach (var item in Saves.Fights.Keys)
            Saves.Fights[item].SetObject();
    }

    public void Start()
    {
        var a = GameObject.FindGameObjectsWithTag("Shard");
        foreach (var shardGameObject in a)
        {
            var comp = shardGameObject.GetComponent<StationaryShard>();
            if (Saves.ShardsIsBroken[comp.key])
                shardGameObject.SetActive(false);
        }
    }
}