using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Database;

public class UnitInitializer : MonoBehaviour
{
    [SerializeField] DataContainer dataContainer;

    void Start()
    {
        if (dataContainer != null && dataContainer.Quizzes != null && dataContainer.Quizzes.Count > 0)
        {
            foreach (var quiz in dataContainer.Quizzes)
            {
                // Access individual quiz data
                Debug.Log("Question: " + quiz.Soal);
                Debug.Log("Option 1: " + quiz.opsi1);
                Debug.Log("Option 2: " + quiz.opsi2);
                Debug.Log("Option 3: " + quiz.opsi3);
                Debug.Log("Correct Answer Index: " + quiz.JawabanBenar);
            }
        }
        else
        {
            Debug.LogError("DataContainer is not assigned!");
        }
    }
}
