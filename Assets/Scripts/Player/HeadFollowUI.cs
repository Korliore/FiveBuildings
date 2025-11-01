using UnityEngine;

public class HeadFollowUI : MonoBehaviour
{
    [SerializeField] private Transform _humanoidHead;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0f, 0.22f);

    private void Update()
    {
        if (_humanoidHead == null) return;

        transform.position = _humanoidHead.position + _offset;
    }
}
