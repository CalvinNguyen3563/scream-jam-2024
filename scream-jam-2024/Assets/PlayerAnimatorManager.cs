using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerAnimatorManager : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public PlayerManager player;
    public RuntimeAnimatorController controller;

    [Header("Rigs")]
    public Dictionary<string, Tuple<Rig, float>> rigs;
    public Rig torchRig;

    [Header("Unarmed State")]
    public Item unarmed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = animator.runtimeAnimatorController;

        rigs = new Dictionary<string, Tuple<Rig, float>>() {
            { "Torch", new Tuple<Rig, float>(torchRig, 0.6f) } 
        };
    }

    private void Start()
    {
        player = WorldGameObjectStorage.Instance.player;
    }
    private void Update()
    {
        HandleMovementAnimations();
    }

    private void LateUpdate()
    {

    }

    private void HandleMovementAnimations()
    {
        if (player.playerLocomotionManager.isSprinting)
        {
            animator.SetBool("IsSprinting", true);
            animator.SetBool("IsWalking", false);
        }
        else if (player.rb.velocity.magnitude <= 0.2f)
        {
            animator.SetBool("IsSprinting", false);
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsSprinting", false);
            animator.SetBool("IsWalking", true);
        }
    }
    public void SwitchToUnarmedState()
    {
        foreach (var rig in rigs.Keys)
        {
            StartCoroutine(SmoothRig(rigs[rig].Item1, rigs[rig].Item1.weight, 0));
        }

        animator.runtimeAnimatorController = unarmed.overrideController;
    }

    public void LinkItemAnimationProfile(Item item)
    {
        animator.runtimeAnimatorController = item.overrideController;

        if (rigs[item.name] != null)
        {
            Tuple<Rig, float> rig = rigs[item.name];
            StartCoroutine(SmoothRig(rig.Item1, rig.Item1.weight, rig.Item2));
        }
    }


    IEnumerator SmoothRig(Rig rig, float start, float end)
    {
        float elapsedTime = 0;
        float waitTime = 0.1f;

        while (elapsedTime < waitTime)
        {
            rig.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
