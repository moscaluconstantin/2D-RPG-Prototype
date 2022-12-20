using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private SpriteRenderer _renderer;

        private IInventoryService _inventory;
        private bool _picked = false;
        private bool _playerInRange = false;

        private void Awake() =>
            _inventory = ServiceProvider.GetService<IInventoryService>();

        private void Update()
        {
            if (_picked)
                return;

            if (_playerInRange && Input.GetButtonUp(InputConstants.FIRE_1))
            {
                _picked = true;
                _inventory.Add(_item);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out PlayerMovement _))
                _playerInRange = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out PlayerMovement _))
                _playerInRange = false;
        }

        [ContextMenu("Initialize")]
        private void Initialize()
        {
            _item = Resources.Load<InventoryItem>($"{ResourcePaths.INVENTORY_ITEMS}/{gameObject.name}");
            RefreshSprite();
        }

        [ContextMenu("Refresh Sprite")]
        private void RefreshSprite()
        {
            _renderer.sprite = _item.Image;
        }
    }
}
