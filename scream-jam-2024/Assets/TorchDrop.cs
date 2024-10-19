using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchDrop : MonoBehaviour
{
    public float burnOpening = 2f;
    public bool canBurn = true;
    public LayerMask whatIsEnemy;

    private void Awake()
    {
       
    }

    private void Start()
    {
        StartCoroutine(cooldownExpire(burnOpening));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((whatIsEnemy & (1 << collision.gameObject.layer)) != 0 && !collision.collider.isTrigger && canBurn)
        {
            Debug.Log("Burn");
            canBurn = false;
            if (collision.gameObject.transform.parent.TryGetComponent<MonsterStateManager>(out MonsterStateManager stateManager))
            {
                stateManager.SwitchState(stateManager.stunState);
            }
            
        }
    }

    private IEnumerator cooldownExpire(float time)
    {
        yield return new WaitForSeconds(time);
        canBurn = false;
        
    }
}
