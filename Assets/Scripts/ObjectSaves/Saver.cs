using System.Collections.Generic;
using UnityEngine;

namespace ObjectSaves
{
    public static class Saves
    {
        public static Vector2 PlayerPosition = new(-198, -13);

        public static readonly Dictionary<int, ExploreObject> Fights = new()
        {
            [102] = new ExploreObject(Resources.Load("Fight2") as GameObject),
            [103] = new ExploreObject(Resources.Load("BadYellowBall") as GameObject),
            [104] = new ExploreObject(Resources.Load("TormentorA") as GameObject),
            [105] = new ExploreObject(Resources.Load("YellowBlue") as GameObject),
            [106] = new ExploreObject(Resources.Load("YellowTormentor") as GameObject),
            [107] = new ExploreObject(Resources.Load("Shell1") as GameObject),
            [108] = new ExploreObject(Resources.Load("ThreeBalls") as GameObject),
            [109] = new ExploreObject(Resources.Load("TwoBalls") as GameObject),
            [110] = new ExploreObject(Resources.Load("Shell2") as GameObject),
            [111] = new ExploreObject(Resources.Load("Hasssle") as GameObject),
            [112] = new ExploreObject(Resources.Load("TrueGlassmaker") as GameObject)
        };

        public static readonly Dictionary<int, bool> Triggers = new()
        {
            [1] = false,
            [2] = false,
            [3] = false,
            [4] = false
        };
        
        public static readonly Dictionary<int, bool> BrokenShards = new()
        {
            [50] = false,
            [51] = false,
            [52] = false,
            [53] = false,
            [54] = false,
            [55] = false,
            [56] = false,
            [57] = false,
            [58] = false,
            [59] = false,
            [60] = false,
            [61] = false,
            [62] = false,
            [63] = false,
            [64] = false,
            [65] = false,
            [66] = false,
            [67] = false,
            [68] = false,
            [69] = false,
            [70] = false,
            [71] = false,
            [72] = false,
            [73] = false,
            [228] = false,
            [1337] = false,
            [1488] = false,
            [0] = false
        };
    }

    public class ExploreObject
    {
        public ExploreObject(GameObject trigger)
        {
            _triggerObject = trigger;
            _position = trigger.transform.position;
        }

        private readonly GameObject _triggerObject;
        private readonly Vector3 _position;
        public void SetObject() => Object.Instantiate(_triggerObject, _position, new Quaternion());
    }
}