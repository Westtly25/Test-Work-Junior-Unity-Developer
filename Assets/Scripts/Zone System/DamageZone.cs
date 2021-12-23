using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    public event Action OnPlayerDamaged;
    private ParticleSystem tempParticle;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateChanger playerStateChanger;

        if (other.TryGetComponent<PlayerStateChanger>(out playerStateChanger))
        {
            if(playerStateChanger.IsInvincible == false)
            {
                Debug.Log("Player Triggered");
                if (tempParticle != null)
                {
                    Destroy(tempParticle.gameObject);
                }

                tempParticle = Instantiate(particle, playerStateChanger.transform.position, Quaternion.identity);
                OnPlayerDamaged?.Invoke();
            }
        }
    }
}