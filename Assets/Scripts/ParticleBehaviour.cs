using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    public void DestroyMe(float delay)
    {
        Destroy(gameObject, delay);
    }
}
