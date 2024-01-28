using UnityEngine;

public class BananaCrate : BaseObject
{
    public override void ActionOne(Player player)
    {
        Debug.Log(player.HeldObject);
        Debug.Log(player.HasHeldObject());
        if (!player.HasHeldObject())
        {
            Debug.Log("player has no held item");
            Transform holdableObjectTransform = Instantiate(HoldableObject.HoldableObjectSO.Prefab, player.HeldObjectPoint);

            player.HeldObject = holdableObjectTransform;
            Debug.Log("now player should have held item");
            Debug.Log(player.HeldObject);
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
