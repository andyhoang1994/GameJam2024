using UnityEngine;

[CreateAssetMenu()]
public class HoldableObjectSO : ScriptableObject
{
    [SerializeField]
    private Transform prefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private string objectName;

    public Transform Prefab { get { return this.prefab; } }

    public Sprite Sprite { get { return this.sprite; } }

    public string ObjectName { get { return this.objectName; } }
}
