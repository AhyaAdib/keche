using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NorskaLib.GoogleSheetsDatabase;

public class dataImporter : MonoBehaviour
{
    public DataContainerBase dataContainer; // Reference to your DataContainer asset

    void Start()
    {
        StartImportProcess();
    }
    
    public void ImportDataFromGoogleSheets()
    {
        // Perform the import process here (e.g., using web requests or API calls)
        // Retrieve data from Google Sheets and populate the 'dataContainer' asset
        // Ensure data is parsed and stored appropriately in 'dataContainer'
        // Example: dataContainer.Quizzes = fetchedData;

        // Once data is imported and stored, notify or perform other actions as needed
        Debug.Log("Data imported successfully!");
    }

    // Example: Method to start import process in response to an event or condition
    public void StartImportProcess()
    {
        ImportDataFromGoogleSheets();
    }
}
