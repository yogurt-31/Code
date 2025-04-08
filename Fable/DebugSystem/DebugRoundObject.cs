using UnityEngine;

public class DebugRoundObject : MonoBehaviour
{
    private void Awake()
    {
        float value = Round(transform.localScale.x);
        transform.localScale = new Vector3(value, value, value);

        transform.position = new Vector3(Round(transform.position.x), Round(transform.position.y), Round(transform.position.z));
    }

    private float Round(float value) => Mathf.Round(value * 100) * 0.01f;
}
