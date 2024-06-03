using SceneScripts;
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
        var findedShards = GameObject.FindGameObjectsWithTag("Shard");
        foreach (var shardGameObject in findedShards)
        {
            var comp = shardGameObject.GetComponent<StationaryShard>();
            if (Saves.ShardsIsBroken[comp.key]) 
                shardGameObject.SetActive(false);
        }

        var findedTriggers = GameObject.FindGameObjectsWithTag("Trigger");
        foreach (var trigger in findedTriggers)
        {
            var comp = trigger.GetComponent<SquadTrigger>();
            if (Saves.Triggers[comp.key]) 
                trigger.SetActive(false);
        }
    }
}