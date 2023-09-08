using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [Networked] private TickTimer delay { get; set; }

    private NetworkCharacterControllerPrototype _cc;
    [SerializeField] private NetworkObject _prefabBall;

    private Vector3 _forward;
    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        { 
            data.direction.Normalize();
            _cc.Move(5 * data.direction * Runner.DeltaTime);
            if(data.direction.sqrMagnitude > 0 )
            {
                _forward = data.direction; 
            }
            if (delay.ExpiredOrNotRunning(Runner))
            {
                if ((data.buttons & NetworkInputData.MOUSEBUTTON01) != 0)
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_prefabBall,
                        transform.position + _forward, Quaternion.LookRotation(_forward),
                        Object.InputAuthority,(runner, o) =>
                        {
                            o.GetComponent<Ball>().Init();
                        });
                }
            }
        }
    }
}
