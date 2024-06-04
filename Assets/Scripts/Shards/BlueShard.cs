using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Shards
{
    public class AzureShard : MonoBehaviour
    {
        public Text nameOfBuff;

        public AzureShard(string name) => nameOfBuff.text = name;

        public void Start() => this.GameObject().SetActive(false);
    }
}
