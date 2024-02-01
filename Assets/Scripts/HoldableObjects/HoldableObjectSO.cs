using UnityEngine;

[CreateAssetMenu()]
public class HoldableObjectSO : ScriptableObject
{
    [SerializeField]
    private Transform prefab;

    [SerializeField]
    private Transform sprite;

    [SerializeField]
    private string objectName;

    public Transform Prefab { get { return this.prefab; } }

    public Transform Sprite { get { return this.sprite; } }

    public string ObjectName { get { return this.objectName; } }
}
