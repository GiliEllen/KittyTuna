using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPDisplayManager : MonoBehaviour
{
    public TMP_Text blackCatHPText;    
    public TMP_Text grayCatHPText;
    public TMP_Text calicoCatHPText;

    private Dictionary<string, TMP_Text> catHPTexts;

    void Start()
    {
        catHPTexts = new Dictionary<string, TMP_Text>
        {
            { "BlackCat", blackCatHPText },
            { "GrayCat", grayCatHPText },
            { "CalicoCat", calicoCatHPText }
        };

        UpdateAllHPDisplays(); 
    }

    public void UpdateHP(PlayableCharacter cat)
    {
        Debug.Log("Updating HP display");
        string catName = cat.name.Replace("(Clone)", "").Trim();

        if (catHPTexts.ContainsKey(catName))
        {
            Debug.Log($"{cat.name} HP: {cat.CurrentHp}");
            catHPTexts[catName].text = $"{cat.CurrentHp}"; 
        }
        else
        {
            Debug.LogError($"Character {cat.name} not found in HP display manager!");
        }
    }

    private void UpdateAllHPDisplays()
    {
        foreach (var entry in catHPTexts)
        {
            string catName = entry.Key;
            TMP_Text hpText = entry.Value;
            hpText.text = "3";
        }
    }
}
