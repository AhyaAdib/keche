using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Question", menuName = "SENDARIBA/QuestionDatas", order = 0)]
public class QuestionDatas : ScriptableObject
{
    public Quiz[] quiz;
    public Quiz2[] quiz2;
    public Quiz3[] quiz3;
    public Quiz4[] quiz4;
    public Quiz5[] quiz5;
    public guessImage[] guessImage;
    public Benda[] Benda;
    public Situs[] Situs;
    public StrukturDanBangunan[] StrukturDanBangunan;
}

[System.Serializable]
public class Quiz
{
    public string Soal;
    public string opsi1, opsi2, opsi3;
    public int JawabanBenar;
}

[System.Serializable]
public class Quiz2
{
    public string Soal;
    public string opsi1, opsi2, opsi3;
    public int JawabanBenar;
}

[System.Serializable]
public class Quiz3
{
    public string Soal;
    public string opsi1, opsi2, opsi3;
    public int JawabanBenar;
}

[System.Serializable]
public class Quiz4
{
    public string Soal;
    public string opsi1, opsi2, opsi3;
    public int JawabanBenar;
}

[System.Serializable]
public class Quiz5
{
    public string Soal;
    public string opsi1, opsi2, opsi3;
    public int JawabanBenar;
}

[System.Serializable]
public class guessImage
{
    public string gambar;
    public string opsi1, opsi2, opsi3;
    public int JawabanBenar;

}

[System.Serializable]
public class Benda
{	
    public string NamaBenda, Lokasi, SKPenetapan, Deskripsi, Gambar1, Gambar2;
}

[System.Serializable]
public class Situs
{	
    public string NamaSitus,	Lokasi,	SKPenetapan,	Deskripsi,	Gambar1,	Gambar2;
}

[System.Serializable]
public class StrukturDanBangunan
{	
    public string NamaBangunan, Lokasi, SKPenetapan, Deskripsi, Gambar1, Gambar2;
}
