using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    [SerializeField] private float _waitWaypointTime = 5f;
    [SerializeField] private float _aggroRange = 5f;
    [SerializeField] private Transform _waypointCollection;

    private Transform[] _waypoints;
    private float _tickTime = 0.5f;
    private float _currentTickTime;
    private int _index;
    private float _speed, _agentSpeed;

    private Transform _player;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agentSpeed = _agent.speed;
        }
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        _waypoints = _waypointCollection.GetComponentsInChildren<Transform>();       
        _index = Random.Range(0, _waypoints.Length);

        InvokeRepeating("Tick", Random.Range(0, 1), _tickTime);
    }

    void Tick()
    {

        if (_player != null && Vector3.Distance(transform.position, _player.position) < _aggroRange)
        {
            _agent.destination = _player.position;
            _agent.speed = _agentSpeed;
        }
        else if (_agent.velocity == Vector3.zero)
        {
            _currentTickTime += _tickTime;
            if (_currentTickTime >= _waitWaypointTime)
            {
                _currentTickTime = 0f;

                _index = _index == _waypoints.Length - 1 ? 0 : _index + 1;
                _agent.destination = _waypoints[_index].position;
                _agent.speed = _agentSpeed / 2;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _aggroRange);
    }

}
