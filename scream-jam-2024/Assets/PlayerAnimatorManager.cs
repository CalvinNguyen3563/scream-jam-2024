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
    public Dictionary<string, TwoBoneIKConstraint> leftArmIK;
    public RigBuilder rigBuilder;
    public Rig torchRig;
    public Rig axeRig;
    public TwoBoneIKConstraint axeLeftArm;
    public GameObject leftIKTarget;

    [Header("Unarmed State")]
    public Item unarmed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = animator.runtimeAnimatorController;

        rigs = new Dictionary<string, Tuple<Rig, float>>() {
            { "Torch", new Tuple<Rig, float>(torchRig, 0.6f) },
            { "Axe", new Tuple<Rig, float>(axeRig, 0.75f) }
        };

        leftArmIK = new Dictionary<string, TwoBoneIKConstraint> {
            {"Axe", axeLeftArm}
        };
    }

    private void Start()
    {
        player = WorldGameObjectStorage.Instance.player;
    }
    private void Update()
    {
        HandleMovementAnimations();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("UseItem");
        }
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

        foreach (var rig in leftArmIK.Keys)
        {
            leftArmIK[rig].data.target = null;
        }

        animator.runtimeAnimatorController = unarmed.overrideController;
    }

    public IEnumerator LinkItemAnimationProfile(Item item)
    {
        yield return null;
        if (item != null)
        {

            animator.runtimeAnimatorController = item.overrideController;
            animator.CrossFade("Equip", 0f);
            if (rigs.ContainsKey(item.name))
            {
                Tuple<Rig, float> rig = rigs[item.name];
                StartCoroutine(SmoothRig(rig.Item1, rig.Item1.weight, rig.Item2));
            }


            // Control left arm IK
            if (leftArmIK.ContainsKey(item.name))
            {
                if (leftIKTarget != null)
                {
                    leftArmIK[item.name].data.target = leftIKTarget.transform;
                }

                foreach (var rig in leftArmIK.Keys)
                {
                    if (rig != item.name)
                    {
                        leftArmIK[rig].data.target = null;
                    }
                }
            }
            else
            {
                foreach (var rig in leftArmIK.Keys)
                {
                    leftArmIK[rig].data.target = null;
                }
            }

            rigBuilder.enabled = false;
            rigBuilder.enabled = true;
            
        }
        else
        {
            SwitchToUnarmedState();
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

        rig.weight = end;
    }
}
