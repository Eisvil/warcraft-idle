using UnityEngine;

public class AttackState : State
{
    private float _timer;

    public override void Enter()
    {
        base.Enter();

        _timer = 0;
    }

    public override void UpdateHandle()
    {
        _timer += Time.deltaTime;

        if (!(_timer >= 1 / Unit.Stats.AttackSpeed)) return;
        
        Unit.Attack();
        _timer = 0f;
    }
}
