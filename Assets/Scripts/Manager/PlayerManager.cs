using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator m_Animator;

    #region Animator 
    private readonly int Run = Animator.StringToHash("Run");
    private readonly int MoveX = Animator.StringToHash("MoveX");
    private readonly int MoveY = Animator.StringToHash("MoveY");
    private readonly int Jump = Animator.StringToHash("Jump");
    private readonly int Fall = Animator.StringToHash("Fall");
    private readonly int Skill = Animator.StringToHash("Skill");
    private readonly int Hurt = Animator.StringToHash("Hurt");
    private readonly int SkillShot = Animator.StringToHash("SkillShot");
    private readonly int SuperRunAnim = Animator.StringToHash("SuperRun");
    #endregion

    private readonly string TransparentWallTag = "TransparentWall";

    private readonly string EnemyTag = "Enemy";

    private float m_InputX = 0f;
    private float m_InputY = 0f;

    private bool m_IsGround = true;

    private Rigidbody m_Rigidbody;

    [SerializeField]
    private CinemachineVirtualCamera m_MainCameraAim;

    [SerializeField]
    private Transform m_AimTarget;

    private Vector3 m_DefaultAimTarget;

    private int m_DefaultPriority = 9;

    [SerializeField]
    private bool m_Elastic = false;

    [SerializeField]
    private bool m_SuperRun = false;

    [SerializeField]
    private bool m_Throughwall = false;

    [SerializeField]
    private bool m_ControlTime = false;

    [SerializeField]
    private float m_JumpForwardForce = 5f;

    [SerializeField]
    private float m_JumpRightForce = 3f;

    [SerializeField]
    private float m_ElasticJumpForce = 50f;

    [SerializeField]
    private float m_DefaultJumpForce = 20f;

    private float m_DefaultSuperRunSpeed = 1f;

    [SerializeField]
    private float m_SuperRunSpeed = 3f;

    private float m_SuperRunSpeedTime = 5f;


    private float m_CurrentSuperRunTime = 0;

    private Camera m_Camera;
    [SerializeField]
    private bool m_IsAim = false;

    private PlayerData m_PlayerData;
    private ResManager m_ResManager;
    private LevelManager m_LevelManager;
    private HPPanel m_HPPanel;

    private AudioSource m_AudioSource;

    private bool m_IsElastic = false;

    private bool m_IsSkill = false;

    private bool m_IsInHurt = false;

    private bool m_IsDead = false;

    private bool m_IsShowDeadPanel = false;

    private float m_HP = 100;
    private int m_Life = 3;

    private float m_HurtTime = 2f;


    private float m_CurrentHurtTime = 0;

    private bool m_CanHurt = true;

    private bool m_IsHurt = false;

    private bool m_IsSuperRun = false;

    private bool m_IsUseElastic = false;
    private float m_DefaultElasticCoolingTime = 5f;

    [SerializeField]
    private float m_CurrentElasticCoolingTime = 5f;

    private bool m_IsUseSuperRun = false;
    private float m_DefaultSuperRunCoolingTime = 5f;
    [SerializeField]
    private float m_CurrentSuperRunCoolingTime = 5f;

    private bool m_IsUseThroughwall = false;
    private float m_DefaultThroughwallCoolingTime = 5f;
    [SerializeField]
    private float m_CurrentThroughwallCoolingTime = 5f;

    private bool m_IsUseControlTime = false;
    private float m_DefaultControlTimeCoolingTime = 5f;
    [SerializeField]
    private float m_CurrentControlCoolingTimeTime = 5f;

    private bool m_IsShowMissionPanel = false;

    private int m_MissionItemCount = 0;



    public bool m_IsJump;

    private bool m_IsInAim = false;

    private LayerMask m_ThroughwallLayerMask;

    private bool m_IsReStart;

    private Vector3 m_Level1ReStartPosition = new Vector3(-22f, -22, -215f);

    private Vector3 m_Level2ReStartPosition = new Vector3(-44f, 32.5f, 0);

    private bool m_IsPause = false;
    public UIPanel m_UIPanel;

    private int m_BoxItem = 0;

    #region 

    public int BoxItem
    {
        get
        {
            return m_BoxItem;
        }
        set
        {
            m_BoxItem = value;
        }
    }

    public int MissionItemCount
    {
        get
        {
            return m_MissionItemCount;
        }
        set
        {
            m_MissionItemCount = value;
        }
    }

    public bool IsShowMissionPanel
    {
        get
        {
            return m_IsShowMissionPanel;
        }
        set
        {
            m_IsShowMissionPanel = value;
        }
    }
    public bool IsPause
    {
        get
        {
            return m_IsPause;
        }
        set
        {
            m_IsPause = value;
        }
    }

    public bool CanHurt
    {
        get
        {

            return m_CanHurt;
        }

        set
        {
            m_CanHurt = value;
        }
    }
    public bool IsDead
    {
        get
        {

            return m_IsDead;
        }

        set
        {
            m_IsDead = value;
        }
    }

    public bool IsSkill
    {
        get
        {
            return m_IsSkill;
        }
        set
        {
            m_IsSkill = value;
        }
    }

    public bool IsInHurt
    {
        get
        {
            return m_IsInHurt;
        }
        set
        {
            m_IsInHurt = value;
        }
    }

    public bool Elastic
    {
        get
        {
            return m_Elastic;
        }
        set
        {
            m_Elastic = value;
        }
    }

    public bool SuperRun
    {
        get
        {
            return m_SuperRun;
        }
        set
        {
            m_SuperRun = value;
        }
    }
    public bool Throughwall
    {
        get
        {
            return m_Throughwall;
        }
        set
        {
            m_Throughwall = value;
        }
    }
    public bool ControlTime
    {
        get
        {
            return m_ControlTime;
        }
        set
        {
            m_ControlTime = value;
        }
    }

    public bool IsGround
    {
        get
        {
            return m_IsGround;
        }
        set
        {
            m_IsGround = value;
        }
    }
    #endregion

    void Start()
    {


        m_Animator = GetComponent<Animator>();
        if (m_Animator == null)
        {
            Debug.LogError("Animator is invalid");
        }
        m_Rigidbody = GetComponent<Rigidbody>();
        if (m_Rigidbody == null)
        {
            Debug.LogError("Rigidbody is invalid");
        }
        m_Camera = Camera.main;
        m_PlayerData = FindObjectOfType<PlayerData>();

        if (m_PlayerData != null)
        {
            m_Elastic = m_PlayerData.Elastic;
            m_SuperRun = m_PlayerData.SuperRun;
            m_Throughwall = m_PlayerData.Throughwall;
            m_ControlTime = m_PlayerData.ControlTime;
            if (m_Elastic)
            {
                m_UIPanel.ShowGetSkill(BadgeType.Elastic);
            }
            if (m_SuperRun)
            {
                m_UIPanel.ShowGetSkill(BadgeType.SuperRun);
            }
            if (m_Throughwall)
            {
                m_UIPanel.ShowGetSkill(BadgeType.Throughwall);
            }
            if (m_ControlTime)
            {
                m_UIPanel.ShowGetSkill(BadgeType.ControlTime);
            }
            m_Life = m_PlayerData.Life;
            m_HP = m_PlayerData.MaxHP;
        }
        m_ResManager = ResManager.Instance;
        if (m_ResManager != null)
        {
            m_HPPanel = Instantiate(m_ResManager.HPPanel).GetComponent<HPPanel>();
        }
        m_HPPanel.HPSlider.value = m_HP;
        m_HPPanel.LifeText.text = m_Life.ToString();
        m_AudioSource = GetComponent<AudioSource>();
        m_LevelManager = LevelManager.Instance;
        if (m_LevelManager != null)
        {
            m_IsReStart = m_LevelManager.IsReStart;
        }

        if (m_IsReStart)
        {
            Level currentLevel = m_LevelManager.GetCurrentLevel();
            if (currentLevel == Level.Level1)
            {
                transform.position = m_Level1ReStartPosition;
            }
            else if (currentLevel == Level.Level2)
            {
                transform.position = m_Level2ReStartPosition;
            }

        }
        else
        {
            Instantiate(m_ResManager.MissionPanel);
        }
        m_DefaultAimTarget = m_AimTarget.localPosition;


    }

    private void PlayerPlaySound(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }

    void Update()
    {

        if (IsShowMissionPanel)
        {
            return;
        }

        m_UIPanel.SetBoxCount(m_BoxItem);


        if (m_IsAim)
        {
            if (!m_IsInAim)
            {
                m_MainCameraAim.Priority += 10;
                m_IsInAim = true;
                m_UIPanel.SetAimIconOn();
            }

        }
        else
        {
            if (m_IsInAim)
            {
                m_MainCameraAim.Priority = m_DefaultPriority;
                m_IsInAim = false;
                m_UIPanel.SetAimIconOff();
            }
        }

        if (m_IsHurt)
        {
            m_CurrentHurtTime += Time.deltaTime;
            if (m_CurrentHurtTime >= m_HurtTime)
            {
                m_CurrentHurtTime = 0;
                CanHurt = true;
                m_IsHurt = false;
            }
        }

        if (m_IsSuperRun)
        {
            m_CurrentSuperRunTime += Time.deltaTime;
            if (m_CurrentSuperRunTime >= m_SuperRunSpeedTime)
            {
                m_CurrentSuperRunTime = 0;
                m_IsSuperRun = false;
                m_Animator.SetFloat(SuperRunAnim, m_DefaultSuperRunSpeed);
            }
        }

        if (m_IsDead)
        {
            if (!m_IsShowDeadPanel)
            {
                Instantiate(m_ResManager.DeadPanel);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                m_IsShowDeadPanel = true;
            }

            return;
        }

        InputInteractive();


        CalculateCoolingTime();
    }

    private void CalculateCoolingTime()
    {
        if (m_IsUseElastic)
        {
            m_CurrentElasticCoolingTime += Time.deltaTime;
            if (m_CurrentElasticCoolingTime >= m_DefaultElasticCoolingTime)
            {
                m_CurrentElasticCoolingTime = m_DefaultElasticCoolingTime;
                m_IsUseElastic = false;
            }
            float ratio = 1f - m_CurrentElasticCoolingTime / m_DefaultElasticCoolingTime;
            m_UIPanel.SetSkillCoolDown(BadgeType.Elastic, ratio);
        }
        if (m_IsUseSuperRun)
        {
            m_CurrentSuperRunCoolingTime += Time.deltaTime;
            if (m_CurrentSuperRunCoolingTime >= m_DefaultElasticCoolingTime)
            {
                m_CurrentSuperRunCoolingTime = m_DefaultElasticCoolingTime;
                m_IsUseSuperRun = false;
            }
            float ratio = 1f - m_CurrentSuperRunCoolingTime / m_DefaultElasticCoolingTime;
            m_UIPanel.SetSkillCoolDown(BadgeType.SuperRun, ratio);
        }
        if (m_IsUseThroughwall)
        {
            m_CurrentThroughwallCoolingTime += Time.deltaTime;

            if (m_CurrentThroughwallCoolingTime >= m_DefaultThroughwallCoolingTime)
            {
                m_CurrentThroughwallCoolingTime = m_DefaultThroughwallCoolingTime;
                m_IsAim = false;
                m_IsUseThroughwall = false;
            }
            float ratio = 1f - m_CurrentThroughwallCoolingTime / m_DefaultElasticCoolingTime;
            m_UIPanel.SetSkillCoolDown(BadgeType.Throughwall, ratio);

        }
        if (m_IsUseControlTime)
        {
            m_CurrentControlCoolingTimeTime += Time.deltaTime;

            if (m_CurrentControlCoolingTimeTime >= m_DefaultControlTimeCoolingTime)
            {
                m_CurrentControlCoolingTimeTime = m_DefaultControlTimeCoolingTime;
                m_IsAim = false;
                m_IsUseControlTime = false;
            }
            float ratio = 1f - m_CurrentControlCoolingTimeTime / m_DefaultElasticCoolingTime;
            m_UIPanel.SetSkillCoolDown(BadgeType.ControlTime, ratio);
        }
    }

    private void InputInteractive()
    {
        m_InputX = Input.GetAxis("Horizontal");
        m_InputY = Input.GetAxis("Vertical");


        if (!m_IsAim)
        {
            if (!m_IsInAim)
            {
                m_AimTarget.localPosition = m_DefaultAimTarget;
                Vector3 forward = m_Camera.transform.TransformDirection(Vector3.forward);
                Quaternion targetRotation = Quaternion.LookRotation(forward);
                Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f);
                Quaternion myRotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
                transform.rotation = myRotation;
            }

        }
        else
        {
            float inputX = Input.GetAxis("Mouse X");
            float inputY = Input.GetAxis("Mouse Y");
            m_AimTarget.localPosition += new Vector3(inputX, inputY, 0);
        }


        SetRayHit();

        if (!m_IsPause)
        {
            if (InputManager.Pause())
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Instantiate(m_ResManager.PausePanel);
            }
        }
        if (m_IsInHurt)
        {
            return;
        }

        if (IsGround)
        {
            if (InputManager.PutBox())
            {
                if (m_BoxItem > 0)
                {
                    Instantiate(m_ResManager.BoxItem, transform.position + transform.forward, Quaternion.identity);
                    m_BoxItem--;
                }
            }

            if (m_Elastic)
            {
                if (InputManager.Elastic())
                {
                    if (m_CurrentElasticCoolingTime >= m_DefaultElasticCoolingTime)
                    {
                        m_IsElastic = true;
                        m_Animator.SetTrigger(Skill);
                        m_IsSkill = true;
                        m_IsUseElastic = true;
                        m_CurrentElasticCoolingTime = 0;
                    }
                }
            }

            if (m_SuperRun)
            {
                if (InputManager.SuperRun())
                {
                    if (m_CurrentSuperRunCoolingTime >= m_DefaultSuperRunCoolingTime)
                    {
                        m_Animator.SetTrigger(Skill);
                        m_Animator.SetFloat(SuperRunAnim, m_SuperRunSpeed);
                        m_IsSkill = true;
                        m_IsSuperRun = true;
                        m_IsUseSuperRun = true;
                        m_CurrentSuperRunCoolingTime = 0;
                    }


                }
            }

            if (m_Throughwall)
            {
                if (InputManager.Throughwall())
                {
                    if (m_CurrentThroughwallCoolingTime >= m_DefaultThroughwallCoolingTime)
                    {
                        m_IsUseThroughwall = true;
                        m_CurrentThroughwallCoolingTime = 0;
                        m_IsAim = true;
                    }
                }
            }


            if (ControlTime)
            {
                if (InputManager.ControlTime())
                {
                    if (m_CurrentControlCoolingTimeTime >= m_DefaultControlTimeCoolingTime)
                    {
                        m_IsAim = true;
                        m_IsUseControlTime = true;
                        m_CurrentControlCoolingTimeTime = 0;
                    }
                }
            }
            if (InputManager.Jump())
            {
                m_IsJump = true;
                if (!IsSkill)
                {
                    if (m_IsElastic)
                    {
                        m_Rigidbody.AddForce(new Vector3(0, m_ElasticJumpForce, 0), ForceMode.Impulse);
                        m_IsElastic = false;
                    }
                    else
                    {
                        m_Rigidbody.AddForce(new Vector3(0, m_DefaultJumpForce, 0), ForceMode.Impulse);
                    }
                    m_Animator.SetTrigger(Jump);
                }

            }
            if (InputManager.Run())
            {
                m_Animator.SetBool(Run, true);
            }
            else
            {
                m_Animator.SetBool(Run, false);
            }
            m_Animator.SetBool(Fall, false);
            m_Animator.SetFloat(MoveX, m_InputX);
            m_Animator.SetFloat(MoveY, m_InputY);
        }
        else
        {
            m_Animator.SetBool(Fall, true);
            m_Rigidbody.MovePosition(transform.position + m_JumpForwardForce * transform.forward * m_InputY * Time.deltaTime + m_JumpRightForce * transform.right * m_InputX * Time.deltaTime);
        }
    }

    private void SetRayHit()
    {
        if (m_IsUseThroughwall || m_IsUseControlTime)
        {
            if (InputManager.TakeShot())
            {
                Vector3 dir = Camera.main.transform.forward;
                Ray ray = new Ray(Camera.main.transform.position, dir);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 20f, ~(1 << LayerMask.NameToLayer("Player"))))
                {
                    if (m_IsUseThroughwall)
                    {
                        if (hitInfo.collider.tag.Equals(TransparentWallTag))
                        {
                            m_IsAim = false;
                            TransparentWall transparentWall = hitInfo.collider.gameObject.GetComponent<TransparentWall>();
                            transparentWall.SetTransparentWallOn();
                            m_Animator.SetTrigger(SkillShot);
                            m_IsSkill = true;
                        }
                    }
                    if (m_IsUseControlTime)
                    {
                        if (hitInfo.collider.tag.Equals(EnemyTag))
                        {
                            m_IsAim = false;
                            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
                            enemy.SetControlTimeOn();
                            m_Animator.SetTrigger(SkillShot);
                            m_IsSkill = true;
                            hitInfo.collider.gameObject.GetComponent<Animator>().speed = 0.1f;
                        }
                    }

                }
            }
        }

    }
    public void ApplyDamage(int damage)
    {
        m_IsHurt = true;
        m_CanHurt = false;
        m_IsInHurt = true;
        m_Animator.SetTrigger(Hurt);
        m_HP -= damage;
        if (m_HP <= 0)
        {
            m_HP = 0;
            m_IsDead = true;
            m_HPPanel.SetLife(--m_PlayerData.Life);
        }
        float hpRatio = m_HP / m_PlayerData.MaxHP;
        m_HPPanel.SetHP(hpRatio);
    }

    public void FallDamage()
    {
        m_HP = 0;
        m_IsDead = true;
        m_HPPanel.SetLife(--m_PlayerData.Life);
        float hpRatio = m_HP / m_PlayerData.MaxHP;
        m_HPPanel.SetHP(hpRatio);
    }
}
