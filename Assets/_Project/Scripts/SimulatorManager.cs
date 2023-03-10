using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SimulatorManager : MonoBehaviour
{
    public static SimulatorManager Instance;

    [System.Serializable]
    public class SimulatorPiece
    {
        public GameObject StepPiece;
        public List<GameObject> Pieces = new();
    }

    [SerializeField, ReadOnly] private int _currentStepPiece;
    [SerializeField, ReadOnly] private int _currentPiece;
    [SerializeField] private List<SimulatorPiece> _simulatorPieces = new();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            _currentStepPiece = 0;
            _currentPiece = -1;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _simulatorPieces[_currentStepPiece].StepPiece.transform
          .DOLocalRotate(new Vector3(0f, 360f, 0f), 2.5f, RotateMode.FastBeyond360)
          .SetLoops(-1, LoopType.Restart)
          .SetEase(Ease.Linear);
    }

    public void ChangeStepPiece(int newStepPiece)
    {
        _currentPiece = -1;
        _simulatorPieces[_currentStepPiece].StepPiece.transform.DOKill(false);
        _simulatorPieces[_currentStepPiece].StepPiece.SetActive(false);
        
        _currentStepPiece = newStepPiece;
        _simulatorPieces[_currentStepPiece].StepPiece.SetActive(true);
        
        _simulatorPieces[_currentStepPiece].StepPiece.transform
            .DOLocalRotate(new Vector3(0f, 360f, 0f), 2.5f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    public void PrevPiece()
    {
        if(_currentPiece - 1 >= -1)
        {
            _simulatorPieces[_currentStepPiece].Pieces[_currentPiece].SetActive(true);
            _currentPiece--;
        }
    }

    public void NextPiece()
    {
        if (_currentPiece + 1 < _simulatorPieces[_currentStepPiece].Pieces.Count)
        {
            _currentPiece++;
            _simulatorPieces[_currentStepPiece].Pieces[_currentPiece].GetComponent<SinglePieceSimulator>().DisableObject();
        }
    }
}