using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilitySystemController;
using static InteractionController;

public class AbilitySystemController : MonoBehaviour
{
    public enum DebuffType { SLOW, STUN, AIRBORNE }

    AttributeSet playerAttributeSet;

    void Awake()
    {
        playerAttributeSet = GameObject.Find("Player").GetComponent<AttributeSet>();
    }

    public void TriggerDebuff(DebuffType debuffType, float duration)
    {
        switch (debuffType)
        {
            case DebuffType.SLOW:
                playerAttributeSet.SetSpeed(1);
                break;

            case DebuffType.STUN:
                break;

            case DebuffType.AIRBORNE:
                break;

            default:
                Debug.LogError("Unknown debuff type: " + debuffType);
                break;
        }
    }

    public void RemoveDebuff(DebuffType debuffType, float duration)
    {
        switch (debuffType)
        {
            case DebuffType.SLOW:
                playerAttributeSet.SetSpeed(3);
                break;

            case DebuffType.STUN:
                break;

            case DebuffType.AIRBORNE:
                break;

            default:
                Debug.LogError("Unknown debuff type: " + debuffType);
                break;
        }
    }
}
