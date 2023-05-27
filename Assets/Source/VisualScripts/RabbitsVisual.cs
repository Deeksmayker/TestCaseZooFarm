using UnityEngine;

public class RabbitsVisual : MonoBehaviour
{
    private RabbitAiController _aiController;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aiController = GetComponent<RabbitAiController>();
    }

    private void Update()
    {
        if (_aiController.GetCurrentSpeed() > 0.1f)
        {
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetBool("Moving", false);
        }
    }
}