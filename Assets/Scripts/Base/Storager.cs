using UnityEngine;
using System;

[RequireComponent(typeof(Adder), typeof(Scanner))]
public class Storager : MonoBehaviour
{
    private const string Spawner = nameof(Spawner);

    private int _countBoxes;
    private ObjectPool _pool;
    private Adder _adder;
    private Scanner _scanner;   

    public event Action<int> CountBoxesChanged;

    private void Awake()
    {
        _adder = GetComponent<Adder>();

        if (GameObject.Find(Spawner).TryGetComponent(out ObjectPool objectPool))
            _pool = objectPool;
    }

    private void OnEnable()
    {
        _adder.BoxesSpended += SpendBox;
    }

    private void OnDisable()
    {
        _adder.BoxesSpended -= SpendBox;
    }

    private void Start()
    {
        _countBoxes = 0;
        _scanner = GetComponent<Scanner>();
    }

    private void AddBox()
    {
        _countBoxes++;
        Invoke(nameof(ChangeBoxes), 0.1f);
    }

    private void SpendBox(int count)
    {
        _countBoxes -= count;
        Invoke(nameof(ChangeBoxes), 0.1f);
    }

    private void ChangeBoxes()
    {
        CountBoxesChanged?.Invoke(_countBoxes);
    }

    public void PutUp(Worker worker, Box box)
    {
        worker.Reset();
        box.Reset();
        _pool.PutObject(box);
        _scanner.RemoveBox(box);
        AddBox();
    }  
}
