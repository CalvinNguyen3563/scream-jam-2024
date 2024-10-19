using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeUse : MonoBehaviour, UseItem
{

    public float attackStaminaValue = 20f;
    public float cooldown = 1f;
    public bool canAttack = true;
    public void UseItem()
    {
        if (!WorldGameObjectStorage.Instance.player.playerAnimatorManager.animator.GetBool("UseItem") &&
           canAttack &&
           PlayerStatsManager.Instance.Stamina > 0 &&
           WorldGameObjectStorage.Instance.player.playerInteractableManager.currentEquippedItem != null &&
           WorldGameObjectStorage.Instance.player.playerInteractableManager.currentEquippedItem.use != null)
        {
            canAttack = false;
            WorldGameObjectStorage.Instance.player.playerAnimatorManager.animator.SetTrigger("UseItem");
            PlayerStatsManager.Instance.DecreaseStamina(attackStaminaValue); 
            StartCoroutine(ResetCooldown(cooldown));
        }
    }

    private IEnumerator ResetCooldown(float time)
    {
        yield return new WaitForSeconds(time); 
        canAttack = true;
    }
}
