using System.Collections;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayerManager m_PlayerManager;

    [SerializeField]
    private float m_DistanceBetweenSteps = 1f;

    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    float minVolume = 0.3f;
    [SerializeField]
    float maxVolume = 0.5f;

    private float m_StepCycleProgress;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ground"))
        {
            if (m_PlayerManager.m_IsJump)
            {
                m_AudioSource.clip = ResManager.Instance.SoundScriptableObject.JumpLand;
                m_AudioSource.Play();
                m_PlayerManager.m_IsJump = false;
            }

            m_PlayerManager.IsGround = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Ground"))
        {
            m_PlayerManager.IsGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Ground"))
        {
            m_PlayerManager.IsGround = false;
        }
    }
    private void Update()
    {
        if (m_PlayerManager.IsGround)
        {
            float speed = m_PlayerManager.GetComponent<Rigidbody>().velocity.magnitude;
            AdvanceStepCycle(speed * Time.deltaTime);
        }
    }

    private void AdvanceStepCycle(float increment)
    {
        m_StepCycleProgress += increment;
        if (m_StepCycleProgress > m_DistanceBetweenSteps)
        {
            m_StepCycleProgress = 0f;
            PlayFootstep();
        }
    }

    private void PlayFootstep()
    {
        float randomVolume = Random.Range(minVolume, maxVolume);
        m_AudioSource.clip = ResManager.Instance.SoundScriptableObject.MoveStep;
        m_AudioSource.volume = randomVolume;
        m_AudioSource.Play();
    }
}
