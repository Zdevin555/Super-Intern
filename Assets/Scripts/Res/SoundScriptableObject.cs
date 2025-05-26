using System.Collections;
using UnityEngine;


[CreateAssetMenuAttribute(fileName = "SoundClip", menuName = "CreateSoundScriptableObject")]
public class SoundScriptableObject : ScriptableObject
{
    public AudioClip L_DoorOpen;
    public AudioClip L_DoorClose;

    public AudioClip S_DoorOpen;
    public AudioClip S_DoorClose;

    public AudioClip Jump;
    public AudioClip JumpLand;

    public AudioClip ComputerSound;
    public AudioClip LiftSound;
    public AudioClip BadgeSound;

    public AudioClip MoveStep;

    public AudioClip BGM;

    public AudioClip Waring;

    public AudioClip BoxItem;

}
