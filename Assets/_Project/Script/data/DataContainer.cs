using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using NorskaLib.GoogleSheetsDatabase;

namespace Game.Database
{
    [CreateAssetMenu(fileName = "DataContainer", menuName = "SENDARIBA/DataContainer")]
    public class DataContainer : DataContainerBase
    {
        [PageName("Quiz")]
        public List<Quiz> Quizzes;
        [PageName("Quiz2")]
        public List<Quiz2> Quizzes2;
        [PageName("Quiz3")]
        public List<Quiz3> Quizzes3;
        [PageName("Quiz4")]
        public List<Quiz4> Quizzes4;
        [PageName("Quiz5")]
        public List<Quiz5> Quizzes5;
        [PageName("guessImage")]
        public List<guessImage> guessImage;
        [PageName("Benda")]
        public List<Benda> benda;
        [PageName("Situs")]
        public List<Situs> situs;
        [PageName("StrukturDanBangunan")]
        public List<StrukturDanBangunan> strukturDanBangunan;
        [PageName("Card")]
        public List<Card> card;
    }
}