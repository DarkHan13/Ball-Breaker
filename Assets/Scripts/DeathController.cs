using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplash;

    public void MakeBloodSplash()
    {
        bloodSplash.SetActive(true);
    }
}
