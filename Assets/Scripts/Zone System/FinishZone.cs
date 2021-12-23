using UnityEngine;
using System;

public class FinishZone : MonoBehaviour
{
    public event Action OnPlayerFinished;

    [SerializeField] private ParticleSystem particle;
    private ParticleSystem tempParticle;


    private void OnTriggerEnter(Collider other)
    {
        PlayerStateChanger playerStateChanger;

        if (other.TryGetComponent<PlayerStateChanger>(out playerStateChanger))
        {
            Debug.Log("Player Triggered");
            
            if (tempParticle != null)
            {
                Destroy(tempParticle.gameObject);
            }

            tempParticle = Instantiate(particle, playerStateChanger.transform.position, Quaternion.identity);
            OnPlayerFinished?.Invoke();
        }
    }
}
