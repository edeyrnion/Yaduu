using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO CharacterDefinition;
    public GameObject CharacterWeaponSlot;

    void Start()
    {
        if (!CharacterDefinition.SetManually)
        {
            CharacterDefinition.MaxHealth = 100;
            CharacterDefinition.CurrentHealth = 100;

            CharacterDefinition.BaseDamage = 1;
            CharacterDefinition.CurrentDamage = CharacterDefinition.BaseDamage;
        }
    }

    public void ApplyHealth(int healthAmount)
    {
        CharacterDefinition.ApplyHealth(healthAmount);
    }

    public void TakeDamage(int amount)
    {
        CharacterDefinition.TakeDamage(amount);
    }

    public void ChangeWeapon(ItemPickUp weaponPickUp)
    {
        if (!CharacterDefinition.UnEquipWeapon(weaponPickUp, CharacterWeaponSlot))
        {
            CharacterDefinition.EquipWeapon(weaponPickUp, CharacterWeaponSlot);
        }
    }
}
