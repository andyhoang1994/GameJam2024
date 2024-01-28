using UnityEngine;

public class EmployeeAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField]
    private Employee employee;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, this.employee.IsWalking);
    }
}
