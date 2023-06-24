using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        
        var newpos = Vector2.Lerp(transform.position, target.position, Time.deltaTime * 10f);
        transform.position = new Vector3(newpos.x, newpos.y, transform.position.z);
    }
}
