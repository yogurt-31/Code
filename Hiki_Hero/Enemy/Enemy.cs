using System.Collections;
using UnityEngine;
public interface Enemy_Option
{
    IEnumerator Move();
    IEnumerator Atk();
    void Death();
}
