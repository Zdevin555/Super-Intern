using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public bool Elastic;
    public bool SuperRun;
    public bool Throughwall;
    public bool ControlTime;

    public float MaxHP = 100;
    public float HP = 100;

    public int MaxLife = 3;
    public int Life = 3;
    public int MissionItem = 3;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


}
