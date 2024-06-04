using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace SceneScripts
{
    public class NPCScript : MonoBehaviour
    {
        [FormerlySerializedAs("dialoge")] public GameObject dialog;
        public Sprite dialogue1;
        public Sprite dialogue2;
        private bool _isStart;
        public GameObject blackOut;

        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = dialog.GetComponent<SpriteRenderer>();
            dialog.SetActive(false);
            blackOut.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player") || _isStart) 
                return;
            _isStart = true;
            StartCoroutine(GetDialog());
        }

        private IEnumerator GetDialog()
        {
            _renderer.sprite = dialogue1;
            dialog.SetActive(true);
            blackOut.SetActive(true);
            yield return new WaitForSeconds(12);
            _renderer.sprite = dialogue2;
            yield return new WaitForSeconds(12);
            dialog.SetActive(false);
            blackOut.SetActive(false);
        }
    }
}
