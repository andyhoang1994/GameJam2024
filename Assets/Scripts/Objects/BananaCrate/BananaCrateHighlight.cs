using UnityEngine;

public class BananaCrateHighlight : MonoBehaviour
{
    [SerializeField]
    private BaseObject bananaCrate;

    [SerializeField]
    private GameObject highlightedBananaCrate;

    private void Start()
    {
        Player.Instance.OnEnterInteractRange += InstanceOnEnterInteractRange;
    }

    private void InstanceOnEnterInteractRange(object sender, OnEnterInteractRangeArgs e)
    {
        if (e.nearbyObject && e.nearbyObject.position().Equals(this.bananaCrate.transform.position))
        {
            Debug.Log("setting highlight");
            Debug.Log(highlightedBananaCrate);
            this.HighlightCrate();
        }
        else
        {
            this.UnhighlightCrate();
        }
    }

    private void HighlightCrate()
    {
        this.highlightedBananaCrate.SetActive(true);
    }

    private void UnhighlightCrate()
    {
        this.highlightedBananaCrate.SetActive(false);
    }
}
