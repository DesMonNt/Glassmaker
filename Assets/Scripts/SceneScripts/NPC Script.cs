using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCScript : MonoBehaviour
{
    [FormerlySerializedAs("dialoge")] public GameObject dialog;
    [SerializeField] public Sprite dialoge1;
    [SerializeField] public Sprite dialoge2;
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
        _renderer.sprite = dialoge1;
        dialog.SetActive(true);
        blackOut.SetActive(true);
        yield return new WaitForSeconds(8);
        _renderer.sprite = dialoge2;
        yield return new WaitForSeconds(8);
        dialog.SetActive(false);
        blackOut.SetActive(false);
    }
}
