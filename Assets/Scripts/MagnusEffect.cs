using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusEffect : MonoBehaviour
{
    [Header("Shot")]
    public float m_Force;
    public float m_Angle;
    public float m_BackSpin;
    public float m_SideSpin;

    [Header("Properties")]
    public bool m_UseEffect;
    public float m_Multiply = 1.0f;
    public float m_StopSpeed = 0.001f;

    private Rigidbody m_Body;
    private bool m_Stopped;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    public void Shot()
    {
        if (!m_Stopped) return;

        m_Body.AddRelativeForce(Vector3.forward * m_Force, ForceMode.Impulse);
        m_Body.angularVelocity = new Vector3(m_BackSpin, m_SideSpin, 0.0f);
    }

    private void FixedUpdate()
    {
        m_Stopped = Mathf.Abs(m_Body.velocity.magnitude) < m_StopSpeed;

        if (m_Stopped || !m_UseEffect) return;
        Vector3 magnusForce = Vector3.Cross(m_Body.angularVelocity, m_Body.velocity);
        m_Body.AddForce(magnusForce.normalized * m_Multiply);
    }

    public void SetAngle(float angle)
    {
        m_Angle = angle;
        Vector3 rotation = transform.eulerAngles;
        rotation.x = -m_Angle;
        transform.eulerAngles = rotation;
    }

    private void Update()
    {
        if (m_Stopped)
        {
            SetAngle(m_Angle);
        }

        if (Input.GetButtonDown("Jump") && m_Stopped)
        {
            Shot();
        }
    }
}
