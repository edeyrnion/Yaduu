using UnityEngine;

public class SpawnItem : MonoBehaviour, ISpawns
{
    public ItemPickUps_SO[] itemDefinitions;

    public Rigidbody ItemSpawned { get; set; }
    public Renderer ItemMaterial { get; set; }
    public ItemPickUp ItemType { get; set; }

    private void Start()
    {
        CreatSpawn(Random.Range(0, itemDefinitions.Length));
    }

    public void CreatSpawn(int randomItem)
    {
        ItemPickUps_SO ip = itemDefinitions[randomItem];

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        spawnPosition += new Vector3(0f, 2f, 0f);
        Quaternion spawnRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        spawnRotation *= Quaternion.Euler(0f, 90f, 0f);

        ItemSpawned = Instantiate(ip.itemSpawnObject, spawnPosition, spawnRotation, transform);
        ItemSpawned.gameObject.layer = (int)Layer.Item;

        ItemMaterial = ItemSpawned.GetComponent<Renderer>();
        ItemMaterial.material = ip.itemMaterial;

        ItemType = ItemSpawned.GetComponent<ItemPickUp>();
        ItemType.ItemDefinition = ip;
    }
}
