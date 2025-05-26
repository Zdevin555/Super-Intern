using System.Collections;
using UnityEngine;


public class FallTrapTrigger : MonoBehaviour
{

    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            player.FallDamage();
        }
    }

}
