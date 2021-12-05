using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject {
    public enum ExpertiseAreas
    {
        BusinessAndEntrepreneurship,
        CreativityAndAesthetics,
        MathDataAndComputing, 
        TechnologyAndRealization,
        UserAndSociety
    }

    public string word;
    public ExpertiseAreas expertiseArea;
}
