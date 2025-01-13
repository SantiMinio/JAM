using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class IndexSelectorUI<T> : MonoBehaviour
{
    public int CurrentIndex { get => index; }
    protected int index;
    [SerializeField] protected int minIndex;
    [SerializeField] protected int maxIndex;

    [SerializeField] UnityEvent OnReachMaxIndex = new UnityEvent();
    [SerializeField] UnityEvent OnReachMinIndex = new UnityEvent();
    public event Action OnReachMaxIndexEvent;
    public event Action OnReachMinIndexEvent;

    public event Action<int, int> OnChangeIndex;

    public void SetMinIndex(int _minIndex) => minIndex = _minIndex;
    public void SetMaxIndex(int _maxIndex) => maxIndex = _maxIndex;

    public void SetIndex(int _index)
    {
        var oldIndex = index;
        index = Mathf.Clamp(_index, minIndex, maxIndex);
        ChangeIndex(index, oldIndex);
        if (index == minIndex)
        {
            OnReachMinIndex.Invoke();
            OnReachMinIndexEvent?.Invoke();
        }
        else if (index == maxIndex)
        {
            OnReachMaxIndex.Invoke();
            OnReachMaxIndexEvent?.Invoke();
        }
    }

    public abstract void InitializeIndexer(T[] elements, int firstIndex);

    public void AddIndex(int indexToAdd = 1)
    {
        if(index + indexToAdd > maxIndex)
        {
            Debug.LogError("Index cannot be greater than Max Index");
            return;
        }
        var oldIndex = index;
        index += indexToAdd;

        ChangeIndex(index, oldIndex);
        if(index == maxIndex)
        {
            OnReachMaxIndex.Invoke();
            OnReachMaxIndexEvent?.Invoke();
        }

    }

    public void SubstractIndex(int indexToSubstract = 1)
    {
        if (index - indexToSubstract < minIndex)
        {
            Debug.LogError("Index cannot be lower than Min Index");
            return;
        }
        var oldIndex = index;

        index -= indexToSubstract;

        ChangeIndex(index, oldIndex);
        if (index == minIndex)
        {
            OnReachMinIndex.Invoke();
            OnReachMinIndexEvent?.Invoke();
        }

    }

    void ChangeIndex(int newIndex, int oldIndex)
    {
        OnChangeIndexAbs(newIndex, oldIndex);
        OnChangeIndex?.Invoke(newIndex, oldIndex);
    }

    protected abstract void OnChangeIndexAbs(int newIndex, int oldIndex);
}
