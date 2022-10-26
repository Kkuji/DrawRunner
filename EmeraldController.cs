using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldController : MonoBehaviour
{
    public ParticleSystem PcEmerald;
    private ParticleSystem a;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Started"))
        {
            ParticleSystem strike = PcEmerald;
            a = Instantiate(strike, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(a.gameObject, 1f);
            CharacterMove.score += 1;
        }
    }
}
