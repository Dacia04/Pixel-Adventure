using System.Collections;
using UnityEngine;

public class TurtleManager : EnemyBase
{
    public float TimeState;

    private bool IdleState;
    private bool SpikeState;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetIdleState();
        StartCoroutine(ChangeState());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        _animator.SetBool("Idle",IdleState);
        _animator.SetBool("Spike",SpikeState);
    }

    private void SetIdleState()
    {
        IdleState = true;
        SpikeState = false;
        gameObject.tag = "Enemy";
    }

    private void SetSpikeState()
    {
        IdleState = false;
        SpikeState = true;
        gameObject.tag = "Trap";
    }

    private IEnumerator ChangeState()
    {
        while(true)
        {
            if(IdleState)
            {
                yield return new WaitForSeconds(TimeState);
                SetSpikeState();
            }
            else
            {
                yield return new WaitForSeconds(TimeState);
                SetIdleState();
            }
            yield return null;
        }
    }
}
