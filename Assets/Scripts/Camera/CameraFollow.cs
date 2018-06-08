using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] GameObject player;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 5);
        return;
    }
}
