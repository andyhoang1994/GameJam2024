using UnityEngine;

public class BananaCrate : BaseObject
{
    public override void ActionOne(Player player)
    {
        Debug.Log("ActionOne");
        Transform holdableObject = Instantiate(HoldableObjectSO.Prefab, this.SpawnPoint);
        holdableObject.localPosition = Vector3.zero;
    }

    public void ActionTwo()
    {

    }
}
