using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainFixedPosition : MonoBehaviour
{
    public Transform fixedTransform;
    public Transform startTransform;

    private void Awake()
    {
        startTransform = fixedTransform;
    }
    private void Update()
    {
        gameObject.transform.position = startTransform.position;
        gameObject.transform.rotation = startTransform.rotation;
    }
}
