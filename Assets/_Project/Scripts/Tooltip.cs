
using TMPro;
using UnityEngine;


public class Tooltip : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _tooltipTextMesh;

    public TextMeshProUGUI TooltipTextMesh
    {
        get => _tooltipTextMesh;
        set => _tooltipTextMesh = value;
    }

    [SerializeField] private string _tooltip;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (_tooltipTextMesh)
                _tooltipTextMesh.text = _tooltip;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(_tooltipTextMesh)
                _tooltipTextMesh.text = "";
        }
    }
    
}
