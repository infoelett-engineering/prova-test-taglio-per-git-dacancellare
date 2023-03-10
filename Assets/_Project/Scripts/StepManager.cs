using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    [System.Serializable]
    public class Piece
    {
        public GameObject StepPiece;
        public int MaxPiece;
        public bool NeedOrder;
    }
    
    public Material NormalMaterial;
    [SerializeField] private AudioClip _clipCut;
    [SerializeField] private GameObject _holsterConfirm;
    [ReadOnly] public int CurrentStepPiece;
    [ReadOnly] public int CurrentPiece;

    [SerializeField] private List<Piece> _pieces = new();
    
    [ReadOnly] public int MaxStepPiece;
    private AudioSource _localAudioSource;
    
    private void Awake()
    {
        CurrentPiece = 0;
        CurrentStepPiece = 0;
        MaxStepPiece = _pieces.Count;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _localAudioSource = GetComponent<AudioSource>();
        _pieces[CurrentStepPiece].StepPiece.SetActive(true);
    }

    public void NextStep()
    {
        if (CurrentStepPiece + 1 < MaxStepPiece)
        {
            CurrentPiece = 0;
            _pieces[CurrentStepPiece].StepPiece.SetActive(false);
            CurrentStepPiece++;
            _pieces[CurrentStepPiece].StepPiece.SetActive(true);

            // Questo caso si verifica quando si arriva ad un ultimo step senza pezzi da tagliare ma solo da mostrare
            // in questo caso invece di avviare un video, segnalo che il trainig è stato completato
            if(_pieces[CurrentStepPiece].StepPiece.GetComponentInChildren<SinglePiece>() != null)
            {
                SimulatorManager.Instance.ChangeStepPiece(CurrentStepPiece);

                TVManager.Instance.ActivatePanelVideo();
                TVManager.Instance.PlayVideo(CurrentStepPiece);
            }
            else
            {
                TVManager.Instance.ActivatePanelUI();
                TVManager.Instance.ChangeTextPanelUI("Training Completato");
            }
        }
        else
        {
            // Fine di tutti gli step
            TVManager.Instance.ActivatePanelUI();
            TVManager.Instance.ChangeTextPanelUI("Training Completato");
        }
    }

    public void NextPiece(int pieceIndex)
    {
        if (CheckError(pieceIndex)) return;

        _localAudioSource.PlayOneShot(_clipCut);

        if (CurrentPiece + 1 < _pieces[CurrentStepPiece].MaxPiece)
        {
            CurrentPiece++;
        }
        else
        {
            TVManager.Instance.ActivatePanelUI();
            TVManager.Instance.ChangeTextPanelUI("Posiziona il pezzo nell'area centrale per continuare");

            _holsterConfirm.SetActive(true);
        }
        //NextStep();
    }

    private bool CheckError(int pieceIndex)
    {
        if (_pieces[CurrentStepPiece].NeedOrder)
        {
            if (CurrentPiece == pieceIndex - 1)
                return false;
            else
            {
                Debug.Log("Error");
                return true;
            }
        }

        return false;
    }
}