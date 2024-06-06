using UnityEngine;
using TMPro;

public class ScoreBoxes : MonoBehaviour
{
    [SerializeField] private Storager _storager;
    [SerializeField] private TMP_Text _score;

    private void Start()
    {
        _score.text = "-";
    }

    private void OnEnable()
    {
        _storager.CountBoxesChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _storager.CountBoxesChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}
