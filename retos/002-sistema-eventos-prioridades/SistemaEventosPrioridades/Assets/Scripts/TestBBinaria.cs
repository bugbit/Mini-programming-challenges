using System;
using System.Collections.Generic;
using UnityEngine;

public class TestBBinaria : MonoBehaviour
{
    [SerializeField] private List<int> numbers = new() { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
    [SerializeField] private int target = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        var idx = numbers.BinarySearch(target);

        Debug.Log($"Index of {target}: {idx}");

        if (idx < 0)
        {
            idx = ~idx;
            Debug.Log($"Index of ~{target}: {idx}");
        }


        numbers.Insert(idx, target);
    }
}