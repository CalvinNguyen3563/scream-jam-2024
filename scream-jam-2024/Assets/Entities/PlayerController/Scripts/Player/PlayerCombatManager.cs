using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    public PlayerManager player;
    public GameObject cam;
    public float attackRange = 5f;
    public float damage = 15f;
    public GameObject bloodVFX;

    public AudioClip sfx;

    private void Awake()
    {
        
    }

    private void Start()
    {
        player = WorldGameObjectStorage.Instance.player;
        cam = WorldGameObjectStorage.Instance.mainCam;
    }
    public void Attack()
    {
        if (Physics.Raycast(player.transform.position, cam.transform.forward, attackRange, player.whatIsEnemy, QueryTriggerInteraction.Ignore))
        {
            MonsterStatsManager.Instance.DecreaseHealth(damage);
            StartCoroutine(PlayBlood());
        }
    }

    public void PlayAxeSFX()
    {
        SoundManager.instance.PlaySoundFXClip(sfx, WorldGameObjectStorage.Instance.player.transform.position, 1f);
    }

    public IEnumerator PlayBlood()
    {
        bloodVFX.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        bloodVFX.SetActive(false);
    }

}
