using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
    [SerializeField] public bool SetManually = false;

    [SerializeField] public int MaxHealth = 0;
    [SerializeField] public int CurrentHealth = 0;

    [SerializeField] public int BaseDamage = 0;
    [SerializeField] public int CurrentDamage = 0;

    public ItemPickUp Weapon { get; private set; }

    public void ApplyHealth(int healthAmount)
    {
        if ((CurrentHealth + healthAmount) > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += healthAmount;
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void EquipWeapon(ItemPickUp weaponPickUp, GameObject weaponSlot)
    {
        Rigidbody newWeapon;

        Weapon = weaponPickUp;
        newWeapon = Instantiate(weaponPickUp.itemDefinition.weaponSlotObject, weaponSlot.transform);
        CurrentDamage = BaseDamage + Weapon.itemDefinition.itemAmount;
    }
    
    public bool UnEquipWeapon(ItemPickUp weaponPickUp, GameObject weaponSlot)
    {
        bool previousWeaponSame = false;

        if (Weapon != null)
        {
            if (Weapon == weaponPickUp)
            {
                previousWeaponSame = true;
            }
            Destroy(weaponSlot.transform.GetChild(0).gameObject);
            Weapon = null;
            CurrentDamage = BaseDamage;
        }

        return previousWeaponSame;
    }

    private void Death()
    {
        Debug.Log("YOU DIED");
        //Call to Game Manager for Death State to trigger respawn
        //Dispaly the Death visualization
    }
}
