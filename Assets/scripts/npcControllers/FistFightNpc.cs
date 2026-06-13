using System.Collections;
using UnityEngine;

public class NPCFistFighter : MonoBehaviour
{
    [Header("Left Arm")]
    [SerializeField] private Transform leftUpperArm;
    [SerializeField] private Transform leftForearm;

    [Header("Right Arm")]
    [SerializeField] private Transform rightUpperArm;
    [SerializeField] private Transform rightForearm;

    [Header("Colliders")]
    [SerializeField] private FistCollider leftFistCollider;
    [SerializeField] private FistCollider rightFistCollider;

    [Header("Attack Settings")]
    [SerializeField] private float timeBetweenAttacks = 1.5f;
    [SerializeField] private float attackDuration = 0.5f;

    [Tooltip("Kontroliše brzinu udarca")]
    [SerializeField]
    private AnimationCurve attackCurve =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Quaternion leftUpperStart;
    private Quaternion leftForearmStart;

    private Quaternion rightUpperStart;
    private Quaternion rightForearmStart;

    private bool isAttacking;

    enum AttackType
    {
        LeftJab,
        RightCross,
        RightUppercut
    }

    void Start()
    {
        SaveInitialPose();
        StartCoroutine(AttackLoop());
    }

    void SaveInitialPose()
    {
        leftUpperStart = leftUpperArm.localRotation;
        leftForearmStart = leftForearm.localRotation;

        rightUpperStart = rightUpperArm.localRotation;
        rightForearmStart = rightForearm.localRotation;
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);

            if (!isAttacking)
            {
                AttackType attack =
                    (AttackType)Random.Range(0, 3);

                yield return StartCoroutine(PerformAttack(attack));
            }
        }
    }

    IEnumerator PerformAttack(AttackType attack)
    {
        isAttacking = true;

        float timer = 0f;

        while (timer < attackDuration)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.Clamp01(timer / attackDuration);

            float anim =
                attackCurve.Evaluate(t);

            AnimateAttack(attack, anim);

            UpdateDamageWindow(attack, t);

            yield return null;
        }

        DisableAllDamage();
        ResetPose();

        isAttacking = false;
    }

    void AnimateAttack(AttackType attack, float t)
    {
        ResetPose();

        switch (attack)
        {
            case AttackType.LeftJab:

                leftUpperArm.localRotation =
                    leftUpperStart *
                    Quaternion.Euler(-40f * t, 0, 0);

                leftForearm.localRotation =
                    leftForearmStart *
                    Quaternion.Euler(-60f * t, 0, 0);

                break;

            case AttackType.RightCross:

                rightUpperArm.localRotation =
                    rightUpperStart *
                    Quaternion.Euler(-60f * t, 0, 0);

                rightForearm.localRotation =
                    rightForearmStart *
                    Quaternion.Euler(-50f * t, 0, 0);

                break;

            case AttackType.RightUppercut:

                rightUpperArm.localRotation =
                    rightUpperStart *
                    Quaternion.Euler(-80f * t, 0, 25f * t);

                rightForearm.localRotation =
                    rightForearmStart *
                    Quaternion.Euler(-20f * t, 0, 0);

                break;
        }
    }

    void UpdateDamageWindow(AttackType attack, float t)
    {
        bool damageActive =
            t > 0.45f && t < 0.75f;

        DisableAllDamage();

        if (!damageActive)
            return;

        switch (attack)
        {
            case AttackType.LeftJab:
                leftFistCollider.EnableDamage();
                break;

            default:
                rightFistCollider.EnableDamage();
                break;
        }
    }

    void DisableAllDamage()
    {
        leftFistCollider.DisableDamage();
        rightFistCollider.DisableDamage();
    }

    void ResetPose()
    {
        leftUpperArm.localRotation = leftUpperStart;
        leftForearm.localRotation = leftForearmStart;

        rightUpperArm.localRotation = rightUpperStart;
        rightForearm.localRotation = rightForearmStart;
    }
}