using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemPickUps_SO itemDefinition;

    private GameObject _player;
    private CharacterStats charStats;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        charStats = _player.GetComponent<CharacterStats>();
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
        switch (itemDefinition.itemType)
        {
            case ItemTypeDefinitions.HEALTH:
                charStats.ApplyHealth(itemDefinition.itemAmount);
                break;
            case ItemTypeDefinitions.WEAPON:
                charStats.ChangeWeapon(this);
                break;
        }
    }
}
