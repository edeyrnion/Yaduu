using UnityEngine;

public interface ISpawns
{
    Rigidbody ItemSpawned { get; set; }
    Renderer ItemMaterial { get; set; }
    ItemPickUp ItemType { get; set; }

    void CreatSpawn(int value);
}
