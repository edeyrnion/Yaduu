using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform _player;

    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = _player.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _player.transform.position, Time.deltaTime * 5);
        return;
    }
}
