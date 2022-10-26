using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    public ParticleSystem PcDiamond;
    private ParticleSystem a;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Started"))
        {
            ParticleSystem strike = PcDiamond;
            a = Instantiate(strike, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(a.gameObject, 1f);
            CharacterMove.score += 3;
        }
    }
}
