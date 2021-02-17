using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI angle_text = null;
    [SerializeField]
    private TextMeshProUGUI force_text = null;

    // Callback to update the interface
    public void OnRequestUpdateUI(float angle_val, float force_val)
    {
        UpdateParams(angle_val, force_val);
    }

    // Update the interface internally
    private void UpdateParams(float angle_val, float force_val)
    {
        angle_text.text = angle_val + "";
        force_text.text = force_val + "";
    }
}
