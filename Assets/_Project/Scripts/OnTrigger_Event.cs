using UnityEngine;
using UnityEngine.Events;

public class OnTrigger_Event : MonoBehaviour
{
    [SerializeField] private string TagToCompare = string.Empty;

    public UnityEvent<Collider> TriggerEnter = new();
    public UnityEvent<Collider> TriggerExit = new();

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se è impostato un tag da comparare e se il tag dell'oggetto con cui si è entrati in collisione
        // non è uguale al tag da comparare esco senza richiamare l'evento
        if (TagToCompare != string.Empty && other.CompareTag(TagToCompare) == false) return;

        TriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit?.Invoke(other);
    }
}