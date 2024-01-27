using UnityEngine;

public class BananaCrate : MonoBehaviour
{
    [SerializeField]
    private HoldableObjectSO holdableObjectSO;

    [SerializeField]
    private Transform spawnPoint;

    public void Interact()
    {
        Debug.Log("Interact");
        Transform holdableObject = Instantiate(holdableObjectSO.Prefab, spawnPoint);
        holdableObject.localPosition = Vector3.zero;
    }
}
