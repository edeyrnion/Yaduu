using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemPickUps_SO ItemDefinition;

    private GameObject _player;
    private CharacterStats _charStats;
    private Rigidbody _item;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _charStats = _player.GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        switch (ItemDefinition.itemType)
        {
            case ItemTypeDefinitions.HEALTH:
                _charStats.ApplyHealth(ItemDefinition.itemAmount);
                break;
            case ItemTypeDefinitions.WEAPON:
                _charStats.ChangeWeapon(this);
                break;
        }
    }
}
