using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {
    public enum ExpertiseAreas : byte
    {
        BusinessAndEntrepreneurship,
        CreativityAndAesthetics,
        MathDataAndComputing, 
        TechnologyAndRealization,
        UserAndSociety,
        OutOfTheBox,
    }

    public string word;
    public ExpertiseAreas expertiseArea;
}
