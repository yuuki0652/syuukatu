using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPositionSMB : StateMachineBehaviour
{
    [SerializeField] bool m_useX;
    [SerializeField] bool m_useY;
    [SerializeField] bool m_useZ;
    [SerializeField] bool m_useRX;
    [SerializeField] bool m_useRY;
    [SerializeField] bool m_useRZ;

    public Transform target;

    Vector3 m_lastPosition = Vector3.zero;
    Vector3 m_lastRotation = Vector3.zero;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_lastPosition = animator.transform.position;
        m_lastRotation = animator.transform.eulerAngles;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 deltaPosition = Vector3.zero;
        Vector3 deltaRotation = Vector3.zero;

        if (m_useX || m_useY || m_useZ)
        {
            deltaPosition = target.position - m_lastPosition;
        }
        if (m_useRX || m_useRY || m_useRZ)
        {
            deltaRotation = target.eulerAngles - m_lastRotation;
        }

        Vector3 newPosition = animator.transform.position + deltaPosition;
        Vector3 newRotation = animator.transform.eulerAngles + deltaRotation;

        if (!m_useX) { newPosition.x = animator.transform.position.x; }
        if (!m_useY) { newPosition.y = animator.transform.position.y; }
        if (!m_useZ) { newPosition.z = animator.transform.position.z; }

        if (!m_useRX) { newRotation.x = animator.transform.eulerAngles.x; }
        if (!m_useRY) { newRotation.y = animator.transform.eulerAngles.y; }
        if (!m_useRZ) { newRotation.z = animator.transform.eulerAngles.z; }

        animator.transform.position = newPosition;
        animator.transform.eulerAngles = newRotation;

        m_lastPosition = animator.transform.position;
        m_lastRotation = animator.transform.eulerAngles;
    }
}

