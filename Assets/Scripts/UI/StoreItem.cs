using TMPro;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string cost;
    void Start()
    {
        TextMeshProUGUI[] textComponents = this.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI title = textComponents[0];
        TextMeshProUGUI cost = textComponents[1];

        title.text = this.title;
        cost.text = this.cost;

    }
}
