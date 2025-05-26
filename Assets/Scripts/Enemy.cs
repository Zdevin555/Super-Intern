using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates
{
    Idle,
    Run,
    Patrol,

}

public class Enemy : MonoBehaviour
{
    private GameObject m_Target;
    private Animator m_Animator;

    private readonly int MoveY = Animator.StringToHash("MoveY");
    private readonly string PlayerTag = "Player";

    public int Attack = 20;

    private bool m_IsControlTime = false;

    private float m_DurationTime = 6f;
    private float m_CurrentTime = 0f;

    private int m_DefaultLayer;

    private EnemyStates m_EnemyStates;

    private NavMeshAgent m_Agent;

    [SerializeField]
    private Vector3 m_TargetPosition;

    [SerializeField]
    private Vector3 m_DefaultPosition;

    [SerializeField]
    private List<Transform> m_PatrolPoint;

    private bool m_IsGotoPatrolPoint = false;

    private int m_CurrentPatrolIndex = 0;

    private float m_IdleTime = 5f;
    private float m_CurrentIdleTime = 5f;

    private Quaternion m_TargetRotation;
    private Quaternion m_NewRotation;
    private Quaternion m_MyRotation;

    [SerializeField]
    private GameObject m_WarningIcon = null;

    [SerializeField]
    private AudioSource m_AudioSource = null;

    private bool m_IsPlaySound = false;

    private float m_CurrentWarningHideTime = 0;
    private float m_DefaultWarningHideTime = 2f;

    public void SetControlTimeOn()
    {
        m_IsControlTime = true;
        m_Animator.speed = 0.1f;
    }
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_DefaultLayer = gameObject.layer;
        m_EnemyStates = EnemyStates.Idle;
        m_Agent.speed = 0.1f;
        m_DefaultPosition = transform.position;
        m_TargetRotation = Quaternion.identity;
        m_NewRotation = Quaternion.identity;
        m_MyRotation = Quaternion.identity;
    }

    private void EnemyStatesLogic()
    {
        switch (m_EnemyStates)
        {
            case EnemyStates.Idle:
                m_CurrentIdleTime += Time.deltaTime;
                if (m_CurrentIdleTime >= m_IdleTime)
                {
                    m_CurrentIdleTime = 0;
                    m_EnemyStates = EnemyStates.Patrol;
                }
                break;
            case EnemyStates.Run:
                Vector3 dir;

                if (m_Target != null)
                {
                    m_TargetPosition = m_Target.transform.position;
                    dir = m_TargetPosition - transform.position;
                    m_Animator.SetFloat(MoveY, 1);
                    m_Agent.isStopped = false;
                    m_Agent.destination = m_TargetPosition;

                    if (!NavMesh.SamplePosition(m_Target.transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
                    {
                        m_Target = null;
                    }
                }
                else
                {
                    m_EnemyStates = EnemyStates.Patrol;
                    break;

                }

                if (m_Target != null)
                {
                    dir = m_Target.transform.position - transform.position;
                    m_TargetRotation = Quaternion.LookRotation(dir);
                    m_NewRotation = Quaternion.Slerp(transform.rotation, m_TargetRotation, 2f);
                    m_MyRotation = Quaternion.Euler(0, m_NewRotation.eulerAngles.y, 0);
                    transform.rotation = m_MyRotation;
                }
                break;


            case EnemyStates.Patrol:
                if (!m_IsGotoPatrolPoint)
                {
                    m_CurrentPatrolIndex = Random.Range(0, m_PatrolPoint.Count);
                    m_IsGotoPatrolPoint = true;
                }
                m_Agent.destination = m_PatrolPoint[m_CurrentPatrolIndex].position;
                m_Animator.SetFloat(MoveY, 0.5f);

                dir = m_PatrolPoint[m_CurrentPatrolIndex].position - transform.position;
                m_TargetRotation = Quaternion.LookRotation(dir);
                m_NewRotation = Quaternion.Slerp(transform.rotation, m_TargetRotation, 2f);
                m_MyRotation = Quaternion.Euler(0, m_NewRotation.eulerAngles.y, 0);
                transform.rotation = m_MyRotation;

                if (Vector3.Distance(m_Agent.transform.position, m_PatrolPoint[m_CurrentPatrolIndex].position) < 3f)
                {
                    m_EnemyStates = EnemyStates.Idle;
                    m_IsGotoPatrolPoint = false;
                    m_Animator.SetFloat(MoveY, 0f);
                    return;
                }
                break;
            default:
                break;
        }
    }

    void Update()
    {
        EnemyStatesLogic();
        if (m_Target != null)
        {
            float dis = Vector3.Distance(transform.position, m_Target.transform.position);
            if (dis >= 15)
            {
                m_Target = null;
            }
        }
        else
        {
            if (m_IsPlaySound)
            {
                PauseWarningSound();
            }
        }

        if (m_WarningIcon.activeSelf)
        {
            m_WarningIcon.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            m_CurrentWarningHideTime += Time.deltaTime;
            if (m_CurrentWarningHideTime >= m_DefaultWarningHideTime)
            {
                m_WarningIcon.SetActive(false);
            }
        }

        if (m_IsControlTime)
        {
            m_CurrentTime += Time.deltaTime;
            if (m_CurrentTime >= m_DurationTime)
            {
                m_CurrentTime = 0;
                m_IsControlTime = false;
                m_Animator.speed = 1f;
            }
        }
    }

    private void PlayWarningSound()
    {
        m_AudioSource.clip = ResManager.Instance.SoundScriptableObject.Waring;
        m_AudioSource.loop = true;
        m_AudioSource.Play();
        m_IsPlaySound = true;
    }

    private void PauseWarningSound()
    {
        m_AudioSource.Pause();
        m_IsPlaySound = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(PlayerTag))
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            if (player.CanHurt)
            {
                player.ApplyDamage(Attack);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag(PlayerTag))
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            if (player.CanHurt)
            {
                player.ApplyDamage(Attack);
                m_EnemyStates = EnemyStates.Run;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            m_Target = other.gameObject;
            if (!m_WarningIcon.activeSelf)
            {
                m_WarningIcon.SetActive(true);
            }
            if (!m_IsPlaySound)
            {
                PlayWarningSound();
            }
            m_EnemyStates = EnemyStates.Run;
        }
    }

}