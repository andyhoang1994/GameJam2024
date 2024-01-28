using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private HoldableObject holdableObject;

    public virtual void ActionOne(Player player)
    {
        Debug.LogError("BaseObject.Interact();");
    }

    public virtual void ActionTwo(Player player)
    {
        Debug.LogError("BaseObject.InteractAlternate();");
    }

    public HoldableObject HoldableObject { get { return this.holdableObject; } set { this.holdableObject = value; } }

    public Transform SpawnPoint { get { return this.spawnPoint; } }

    public Vector3 position()
    {
        return base.transform.position;
    }
}