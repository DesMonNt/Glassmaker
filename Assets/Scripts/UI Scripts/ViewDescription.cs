using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class ViewDescription : MonoBehaviour
    {
        [FormerlySerializedAs("IsAttack")] public bool isAttack;
        [FormerlySerializedAs("IsSkill")] public bool isSkill;
        [FormerlySerializedAs("IsUltimate")] public bool isUltimate;
        [FormerlySerializedAs("_spritePassive")] [SerializeField] private Sprite spritePassive;
        [FormerlySerializedAs("_spriteActive")] [SerializeField] private Sprite spriteActive;
        private SpriteRenderer _renderer;
    
        [FormerlySerializedAs("Description")] public Image description;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            isAttack = false;
            isSkill = false;
            isUltimate = false;
            description.GameObject().SetActive(false);
        }

        private void OnMouseOver()
        {
            _renderer.sprite = spriteActive;
            description.GameObject().SetActive(true);
        }

        private void OnMouseExit()
        {
            _renderer.sprite = spritePassive;
            description.GameObject().SetActive(false);
        }

        private void OnMouseDown()
        {
            if (this.GameObject().name is "Attack")
                isAttack = true;
            else if (this.GameObject().name is "Skill")
                isSkill = true;
            else if (this.GameObject().name is "Ultimate")
                isUltimate = true;
        }
    }
}
