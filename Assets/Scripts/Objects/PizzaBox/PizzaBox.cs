using UnityEngine;

public class PizzaBox : BaseObject
{
    public override void ActionOne(Player player)
    {
        Debug.Log("ActionOne");
        if (!player.HasHeldObject())
        {
            Transform holdableObjectTransform = Instantiate(HoldableObject.HoldableObjectSO.Prefab, player.HeldObjectPoint);
            Debug.Log($"holdableObjectTransform: {holdableObjectTransform}");
            player.HeldObject = holdableObjectTransform;
        }
        else
        {
            player.ClearHeldObject();
        }
    }

    public void ActionTwo()
    {

    }
}
