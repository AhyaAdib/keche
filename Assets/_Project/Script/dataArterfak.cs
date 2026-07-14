using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "dataArterfak", menuName = "SENDARIBA/dataArterfak", order = 0)]
public class dataArterfak : ScriptableObject {

    public Arterfak1[] arterfak1;
}

[System.Serializable]
public class Arterfak1
{
    public Sprite[] pecahanArterfak;
    public Sprite arterfakFullTransparent;
    public Sprite arterfakFull;
}
