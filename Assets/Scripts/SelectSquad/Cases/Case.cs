using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is bad practice but it works and makes it easy to add cases
// + better than just hardcoding cases
[CreateAssetMenu(fileName = "New Case", menuName = "Case")]
public class Case : ScriptableObject
{
    public enum Squad : byte
    {
        InclusiveDesignAndThoughtfulTechnology,
        GamesAndPlay,
        Vitality,
        TransformingPractices,
        CraftingWearableSenses,
        NewFutures,
        Health,
        SensoryMatters,
        ArtificeArtificialIntelligence
    }

    public string caseText;
    [SerializeField] private Squad squad;

    public string getSquadString() {
        string squadString = null;

        switch (squad) {
            case Squad.InclusiveDesignAndThoughtfulTechnology:
                squadString = "Inclusive Design and Thoughtful Technology";
                break;
            case Squad.GamesAndPlay:
                squadString = "Games And Play";
                break;
            case Squad.Vitality:
                squadString = "Vitality";
                break;
            case Squad.TransformingPractices:
                squadString = "Transforming Practices";
                break;
            case Squad.CraftingWearableSenses:
                squadString = "Crafting Wearable Senses";
                break;
            case Squad.NewFutures:
                squadString = "New Futures (connectivity in the home)";
                break;
            case Squad.Health:
                squadString = "Health";
                break;
            case Squad.SensoryMatters:
                squadString = "Sensory Matters (sustainable food systems)";
                break;
            case Squad.ArtificeArtificialIntelligence:
                squadString = "Artifice - Artificial Intelligence";
                break;
        }

        if (squadString == null)
            Debug.LogError("Case.cs: Squad cannot be resolved.");

        return squadString;
    }
}
