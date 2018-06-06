using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;

    private void Start()
    {
        MouseManager.Instance.OnClickEnvironment.AddListener(HandleClickEnvironment);
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void HandleClickEnvironment(Vector3 target, Layer layer)
    {
        if (layer == Layer.Walkable)
        {
            _navMeshAgent.destination = target;
        }
    }
}
