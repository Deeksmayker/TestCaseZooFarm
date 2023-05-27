using UnityEngine;
using UnityEngine.AI;

public class RabbitAiController : MonoBehaviour
{
    [SerializeField] private float timeToChangeLocation;

    private float _timer;

    private NavMeshAgent _agent;

    private Aviary _connectedAviary;

    private void Awake()
    {
        _connectedAviary = GetComponentInParent<Aviary>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            _agent.SetDestination(_connectedAviary.GetRandomPointInside());
            _timer = Random.Range(timeToChangeLocation - 1, timeToChangeLocation + 1);
            return;
        }

        _timer -= Time.deltaTime;
    }

    public float GetCurrentSpeed()
    {
        return _agent.velocity.magnitude;
    }
}