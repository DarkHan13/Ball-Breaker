using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector2 to;

    public void GoTo(Transform who)
    {
        who.transform.position = new Vector3(to.x, to.y, who.transform.position.z);
    }
}
