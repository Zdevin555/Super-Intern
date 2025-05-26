
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    public TextMeshProUGUI BadgeText;
    public TextMeshProUGUI BadgeUseText;
    public TextMeshProUGUI LiftUseText;
    public TextMeshProUGUI GetItemText;
    public TextMeshProUGUI UnLockText;
    public GameObject AimIcon;

    public TextMeshProUGUI BoxCount;

    public GameObject Elastic;
    public GameObject SuperRun;
    public GameObject Throughwall;
    public GameObject ControlTime;

    public Image ElasticCool;
    public Image SuperRunCool;
    public Image ThroughwallCool;
    public Image ControlTimeCool;

    private void Start()
    {
        if (BadgeText != null)
        {
            BadgeText.gameObject.SetActive(false);
        }
        if (BadgeUseText != null)
        {
            BadgeUseText.gameObject.SetActive(false);
        }
        if (LiftUseText != null)
        {
            LiftUseText.gameObject.SetActive(false);
        }
        if (AimIcon != null)
        {
            AimIcon.SetActive(false);
        }
    }

    public void ShowGetSkill(BadgeType type)
    {
        switch (type)
        {
            case BadgeType.Elastic:
                Elastic.SetActive(true);
                break;
            case BadgeType.SuperRun:
                SuperRun.SetActive(true);
                break;
            case BadgeType.Throughwall:
                Throughwall.SetActive(true);
                break;
            case BadgeType.ControlTime:
                ControlTime.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetSkillCoolDown(BadgeType type, float ratio)
    {
        switch (type)
        {
            case BadgeType.Elastic:
                ElasticCool.fillAmount = ratio;
                break;
            case BadgeType.SuperRun:
                SuperRunCool.fillAmount = ratio;
                break;
            case BadgeType.Throughwall:
                ThroughwallCool.fillAmount = ratio;
                break;
            case BadgeType.ControlTime:
                ControlTimeCool.fillAmount = ratio;
                break;
            default:
                break;
        }
    }

    public void SetAimIconOn()
    {
        AimIcon.SetActive(true);
    }


    public void SetAimIconOff()
    {
        AimIcon.SetActive(false);
    }


    public void SetBadgeTextActive(bool active)
    {
        if (BadgeText != null)
        {
            BadgeText.gameObject.SetActive(active);
        }
    }


    public void SetUseBadgePanel(bool active)
    {
        if (BadgeUseText != null)
        {
            BadgeUseText.gameObject.SetActive(active);
        }
    }

    public void SetUseLiftPanel(bool active)
    {
        if (LiftUseText != null)
        {
            LiftUseText.gameObject.SetActive(active);
        }
    }

    public void SetGetItemPanel(bool active)
    {
        if (GetItemText != null)
        {
            GetItemText.gameObject.SetActive(active);
        }
    }

    public void SetUnLockText(bool active, int lockCount = 0)
    {
        if (UnLockText != null)
        {
            UnLockText.gameObject.SetActive(active);
            UnLockText.text = "You need to unlock it " + lockCount + " times";
        }
    }

    public void SetMissionText(bool active, int missionCount = 0)
    {
        if (UnLockText != null)
        {
            UnLockText.gameObject.SetActive(active);
            UnLockText.text = "You still need to collect " + missionCount + " energy reactors";
        }
    }

    public void SetBoxCount(int count)
    {
        if (BoxCount != null)
        {
            if (count > 0)
            {
                BoxCount.gameObject.SetActive(true);
                BoxCount.text = "BOX: " + count;
            }
            else
            {
                BoxCount.gameObject.SetActive(false);
            }
        }

    }
}

