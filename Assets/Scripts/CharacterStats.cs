using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO CharacterDefinition;
    public GameObject CharacterWeaponSlot;

    void Start()
    {
        if (!CharacterDefinition.SetManually)
        {
            CharacterDefinition.IsPlayer = false;

            CharacterDefinition.MaxHealth = 100;
            CharacterDefinition.CurrentHealth = 100;

            CharacterDefinition.BaseDamage = 1;
            CharacterDefinition.CurrentDamage = CharacterDefinition.BaseDamage;

            CharacterDefinition.BaseResistance = 0f;
            CharacterDefinition.CurrentResistance = CharacterDefinition.BaseResistance;

            CharacterDefinition.BaseRange = 2f;
            CharacterDefinition.CurrentRange = CharacterDefinition.BaseRange;

            CharacterDefinition.BaseAttackSpeed = 1f;
            CharacterDefinition.CurrentAttackSpeed = CharacterDefinition.BaseAttackSpeed;
}
    }

    public void ApplyHealth(float healthAmount)
    {
        CharacterDefinition.ApplyHealth(healthAmount);
    }

    public void TakeDamage(float amount)
    {
        CharacterDefinition.TakeDamage(amount, gameObject);
        Debug.Log(CharacterDefinition.name + " Health: " + GetHealth());
    }

    public void ChangeWeapon(ItemPickUp weaponPickUp)
    {
        if (!CharacterDefinition.UnEquipWeapon(weaponPickUp, CharacterWeaponSlot))
        {
            CharacterDefinition.EquipWeapon(weaponPickUp, CharacterWeaponSlot);
            weaponPickUp.gameObject.SetActive(false); //Want to destroy it, but somehow it bugs destroy of the weapon in character hand
        }
    }

    public float GetHealth()
    {
        return CharacterDefinition.CurrentHealth;
    }

    public ItemPickUp GetCurrentWeapon()
    {
        return CharacterDefinition.Weapon;
    }

    public float GetDamage()
    {
        return CharacterDefinition.CurrentDamage;
    }

    public float GetResistance()
    {
        return 1 - CharacterDefinition.CurrentResistance;
    }

    public float GetRange()
    {
        return CharacterDefinition.CurrentRange;
    }

    public float GetAttackSpeed()
    {
        return CharacterDefinition.CurrentAttackSpeed;
    }
}
