using UnityEngine;

public class PizzaBox : BaseObject
{
    public override void ActionOne(Player player)
    {
        if (!player.HasHeldObject())
        {
            Transform holdableObjectTransform = Instantiate(HoldableObject.HoldableObjectSO.Prefab, player.HeldObjectPoint);

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
