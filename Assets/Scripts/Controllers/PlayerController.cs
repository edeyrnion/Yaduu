using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _navMeshAgent;
    CharacterStats _stats;

    private GameObject attackTarget;

    private bool _isMouseMovement = true;

    private void Start()
    {
        MouseManager.Instance.OnLeftClick.AddListener(HandleClickEnvironment);
        MouseManager.Instance.OnClickUseable.AddListener(InteractWithTarget);

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _stats = GetComponent<CharacterStats>();
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
            StopAllCoroutines();
            _navMeshAgent.isStopped = false;
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

    public void InteractWithTarget(GameObject target, Layer layer)
    {
        if (layer == Layer.Enemy)
        {
            StopAllCoroutines();

            _navMeshAgent.isStopped = false;
            attackTarget = target;
            StartCoroutine(PursueAttackTarget());
        }
    }

    private IEnumerator PursueAttackTarget()
    {
        _navMeshAgent.isStopped = false;
        var weapon = _stats.GetCurrentWeapon();

        while (Vector3.Distance(transform.position, attackTarget.transform.position) > _stats.GetRange())
        {
            _navMeshAgent.destination = attackTarget.transform.position;
            yield return null;
        }

        _navMeshAgent.isStopped = true;

        transform.LookAt(attackTarget.transform);
        _animator.SetTrigger("Attack");
    }

    public void Hit()
    {
        if (attackTarget != null)
        {
            ExecuteAttack(gameObject, attackTarget);
        }
    }

    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (defender == null)
        {
            return;
        }

        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > _stats.GetRange())
        {
            return;
        }

        if (!attacker.transform.IsFacingTarget(defender.transform))
        {
            return;
        }

        var defenderStats = defender.GetComponent<CharacterStats>();
        if (defenderStats != null)
        {
            var attack = _stats.GetDamage();
            attack *= defenderStats.GetResistance();
            defenderStats.TakeDamage(attack);
        }
    }
}


