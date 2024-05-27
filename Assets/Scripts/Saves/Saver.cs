using System.Collections.Generic;
using Unity;
using UnityEngine;

public static class Saves
{
    public static string savedValue;

    public static Dictionary<int, ExploreObject> Fights = new()
    {
        //[101] = new ExploreObject(Resources.Load("Fight1") as GameObject),
        //[102] = new ExploreObject(Resources.Load("Fight2") as GameObject)
    };
    public static Dictionary<int, ExploreObject> Shards = new()
    {
        [101] = new ExploreObject(Resources.Load("Geode1") as GameObject)
    };
}

public class ExploreObject
{
    public ExploreObject(GameObject trigger)
    {
        TriggerObject = trigger;
        Position = trigger.transform.position;
    }
    public GameObject TriggerObject;
    public Vector3 Position;
    public void SetObject()
    {
        GameObject.Instantiate(TriggerObject, Position, new Quaternion());
    }
}