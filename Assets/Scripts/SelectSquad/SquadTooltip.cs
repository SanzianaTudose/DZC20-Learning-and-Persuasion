using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SquadTooltip : MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    public TMP_Text squadName;
    private string squad;
    private bool timerFlashes;

    private Dictionary<string, string> squadTooltips;

    private void Start()
    {
        squad = "";

        squadTooltips = new Dictionary<string, string>();
        squadTooltips.Add("Squad Choice", 
            "Default Squad Choice for debugging purposes.");

        squadTooltips.Add("Inclusive Design and Thoughtful Technology", 
            "This squad address relevant societal challenges such as designing for different forms of vulnerability, " +
            "for marginalized and ageing populations, and for digital wellbeing.");

        squadTooltips.Add("Games And Play",
            "This squad lets you leverage playful technologies, game design, and game data " +
            "to facilitate societal change in a range of application areas.");

        squadTooltips.Add("Vitality", 
            "This squad offers designerly ways to trigger behavior change for healthier lifestyles," +
            "with solutions contributing to both physical and mental vitality and healthy living.");

        squadTooltips.Add("Transforming Practices",
            "This squad aims to foster alternative practices for human development and societal resilience.");

        squadTooltips.Add("Crafting Wearable Senses",
            "This squad is dedicated to supporting students to pursue design and research ideas " +
            "within the broader scope of research through design, complexity of attention and soft and flexible things.");

        squadTooltips.Add("New Futures (connectivity in the home)",
            "This squad concerns itself with designing for ‘the good life’. " +
            "Considering current and future challenges such as the climate crisis, " +
            "unbridled consumerism and shifting(economic) superpowers.");

        squadTooltips.Add("Health",
            "In this squad students design and do research on IoT solutions for healthcare " +
            "and applications linked to large datasets, machine learning and AI.");

        squadTooltips.Add("Sensory Matters (sustainable food systems)",
            "This squad aims to conduct research on the design of systems " +
            "with emerging materials such as stimuli - responsive materials, bio and living material.");

        squadTooltips.Add("Artifice - Artificial Intelligence",
            "This squad explores the interaction designs of AI systems and humans.");
    }

    public void ShowTooltip()
    {
        if (timerFlashes) return;

        squad = squadName.text;

        if (!squadTooltips.ContainsKey(squad)) return;

        squadName.text = squadTooltips[squad];
    }

    public void HideTooltip()
    {
        if (squad == "") return;
        if (!squadTooltips.ContainsKey(squad)) return;

        squadName.text = squad;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData data)
    {
        HideTooltip();
    }

    public void SetTimerFlashes(bool value)
    {
        this.timerFlashes = value;
        if (value) HideTooltip();
    }

}
