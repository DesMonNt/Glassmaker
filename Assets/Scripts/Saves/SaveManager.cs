using Unity;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void Awake()
    {
        foreach (var item in Saves.Fights.Keys)
            Saves.Fights[item].SetObject();
        foreach (var item in Saves.Shards.Keys)
            Saves.Shards[item].SetObject();
    }

    public void RemoveShardByKey(int key) => Saves.Shards.Remove(key);
    public void RemoveFightByKey(int key) => Saves.Fights.Remove(key);
}