using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _navMeshAgent;

    private void Start()
    {
        MouseManager.Instance.OnClickEnvironment.AddListener(HandleClickEnvironment);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
    }

    void HandleClickEnvironment(Vector3 target, Layer layer)
    {
        if (layer == Layer.Walkable)
        {
            _navMeshAgent.destination = target;
        }
    }
}
