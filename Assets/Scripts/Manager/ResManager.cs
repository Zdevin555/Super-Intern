using System.Collections;
using UnityEngine;

public class ResManager : MonoBehaviour
{
    public static ResManager Instance
    {
        private set; get;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public PhysicMaterial NoFrictionMaterial = null;
    public GameObject PopPanel = null;
    public GameObject DeadPanel = null;

    public GameObject HPPanel = null;
    public GameObject PausePanel = null;
    public GameObject MissionPanel = null;
    public GameObject BoxItem = null;
    public SoundScriptableObject SoundScriptableObject = null;
}
