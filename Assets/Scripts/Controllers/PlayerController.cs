using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _navMeshAgent;

    private bool _isMouseMovement = true;

    private void Start()
    {
        MouseManager.Instance.OnLeftClick.AddListener(HandleClickEnvironment);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        
        if (!_isMouseMovement)
        {
            ProcessDirectMovement();
        }
    }

    void HandleClickEnvironment(Vector3 target, Layer layer)
    {
        if (layer == Layer.Walkable)
        {
            _navMeshAgent.destination = target;
        }
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward + h * Camera.main.transform.right;

        _navMeshAgent.velocity = move * _navMeshAgent.speed;
    }

}


