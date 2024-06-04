using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Shards
{
    public class AmberShard : MonoBehaviour
    {
        public Text nameOfBuff;

        public AmberShard(string name) => nameOfBuff.text = name;

        public void Start() => this.GameObject().SetActive(false);
    }
}
