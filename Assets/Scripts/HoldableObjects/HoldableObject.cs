using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    [SerializeField]
    private HoldableObjectSO holdableObjectSO;

    public HoldableObjectSO HoldableObjectSO { get { return holdableObjectSO; } }
}
