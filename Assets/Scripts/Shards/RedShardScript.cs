using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Shards
{
    public class CrimsonShardScript : MonoBehaviour
    {
        public Text nameOfBuff;

        public CrimsonShardScript(string name) => nameOfBuff.text = name;

        public void Start() => this.GameObject().SetActive(false);
    }
}
