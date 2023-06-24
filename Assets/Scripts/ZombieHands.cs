using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHands : MonoBehaviour
{
    private GameObject _me;

    private void Start()
    {
        _me = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<MoveController>().Death();
            _me.SetActive(false);
        }
    }
}
