using System;
using DG.Tweening;
using UnityEngine;

public class EyeMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private EyeType eyeType;
    
    private enum EyeType
    {
        glass,
        drawer,
        table
    }
    private Vector3 lookDir;

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        lookDir = target.position - transform.position;
        lookDir.Normalize();
        var look = Quaternion.LookRotation(lookDir).eulerAngles;

        switch (eyeType)
        {
            case EyeType.table:
            #region table
                if (look.x is > 0 and < 180 and > 3)
                {
                    look.x = 3;
                }
                else if (look.x is < 360 and > 180 and < 357)
                {
                    look.x = 357;
                }
        
                if (look.y is > 0 and < 180 and > 6)
                {
                    look.y = 6;
                }
                else if (look.y is < 360 and > 180 and < 354)
                {
                    look.y = 354;
                }
            #endregion
            break;
            case EyeType.glass:
                break;
            case EyeType.drawer:
                break;
            
        }
        

        transform.rotation = Quaternion.Euler(look);
    }
}
