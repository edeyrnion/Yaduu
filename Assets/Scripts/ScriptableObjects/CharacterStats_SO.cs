using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
    [SerializeField] public bool SetManually = false;

    [SerializeField] public bool IsPlayer = false;

    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] public float CurrentHealth = 100f;

    [SerializeField] public float BaseDamage = 1f;
    [SerializeField] public float CurrentDamage = 1f;

    [SerializeField] public float BaseResistance = 0f;
    [SerializeField] public float CurrentResistance = 0f;

    [SerializeField] public float BaseRange = 2f;
    [SerializeField] public float CurrentRange = 2f;

    [SerializeField] public float BaseAttackSpeed = 1f;
    [SerializeField] public float CurrentAttackSpeed = 1f;

    public ItemPickUp Weapon { get; private set; }

    public void ApplyHealth(float healthAmount)
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

    public void TakeDamage(float amount, GameObject gameObject)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            Death(gameObject);
        }
    }

    public void EquipWeapon(ItemPickUp weaponPickUp, GameObject weaponSlot)
    {
        Rigidbody newWeapon;

        Weapon = weaponPickUp;
        newWeapon = Instantiate(weaponPickUp.ItemDefinition.weaponSlotObject, weaponSlot.transform);
        CurrentDamage = BaseDamage + Weapon.ItemDefinition.itemAmount;
    }
    
    public bool UnEquipWeapon(ItemPickUp weaponPickUp, GameObject weaponSlot)
    {
        bool previousWeaponSame = false;
        if (Weapon != null)
        {
            if (Weapon == weaponPickUp)
            {
                previousWeaponSame = true;
                return previousWeaponSame;
            }
            Destroy(weaponSlot.transform.GetChild(0).gameObject);
            Weapon = null;
            CurrentDamage = BaseDamage;
        }

        return previousWeaponSame;
    }

    private void Death(GameObject gameObject)
    {
        if (IsPlayer)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
