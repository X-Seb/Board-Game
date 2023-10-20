using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class GGFaceRBVelocity : MonoBehaviour
{
    [Header("We'll face the direction of this rigidbody's velocity: ")]
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private Projectile m_projectile;
    [SerializeField] private Vector3 m_upDirection;
    [Header("For reference only:")]
    [SerializeField] private Vector3 m_velocity;

    private void LateUpdate()
    {
        m_velocity = m_projectile.Direction;

        if (m_velocity.magnitude >= 0.1)
        {
            transform.rotation = Quaternion.LookRotation(m_velocity, m_upDirection);
        }
    }
}
