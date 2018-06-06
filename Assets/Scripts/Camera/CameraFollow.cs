using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] GameObject player;
    Vector3 offset;

    bool b;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void LateUpdate()
    {

        if (!b)
        {
            offset = transform.position - player.transform.position;
            b = true;
        }
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * 5);
        return;
    }
}
