using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private HoldableObjectSO holdableObjectSO;

    public virtual void ActionOne(Player player)
    {
        Debug.LogError("BaseObject.Interact();");
    }

    public virtual void ActionTwo(Player player)
    {
        Debug.LogError("BaseObject.InteractAlternate();");
    }

    public HoldableObjectSO HoldableObjectSO { get { return this.holdableObjectSO; } set { this.holdableObjectSO = value; } }

    public Transform SpawnPoint { get { return this.spawnPoint; } }

    public Vector3 position()
    {
        return base.transform.position;
    }
}