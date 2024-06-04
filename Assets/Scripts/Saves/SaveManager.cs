using Misc;
using SceneScripts;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        player.transform.position = Saves.PlayerPosition;
        foreach (var item in Saves.Fights.Keys)
            Saves.Fights[item].SetObject();
    }

    public void Start()
    {
        var foundShards = GameObject.FindGameObjectsWithTag("Shard");
        foreach (var shardGameObject in foundShards)
        {
            var comp = shardGameObject.GetComponent<StationaryShard>();
            if (Saves.ShardsIsBroken[comp.key]) 
                shardGameObject.SetActive(false);
        }

        var foundTriggers = GameObject.FindGameObjectsWithTag("Trigger");
        foreach (var trigger in foundTriggers)
        {
            var comp = trigger.GetComponent<SquadTrigger>();
            if (Saves.Triggers[comp.key]) 
                trigger.SetActive(false);
        }
    }
}