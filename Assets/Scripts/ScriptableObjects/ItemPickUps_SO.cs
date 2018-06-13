using UnityEngine;

public enum ItemTypeDefinitions { HEALTH, WEAPON };

[CreateAssetMenu(fileName = "NewItem", menuName = "Spawnable Item/New Pick-up", order = 1)]
public class ItemPickUps_SO : ScriptableObject
{
    public string itemName = "New Item";
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.HEALTH;
    public int itemAmount = 0;

    public Material itemMaterial = null;
    public Rigidbody itemSpawnObject = null;
    public Rigidbody weaponSlotObject = null;

}
