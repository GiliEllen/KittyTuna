using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPDisplayManager : MonoBehaviour
{
    public TMP_Text blackCatHPText;    
    public TMP_Text grayCatHPText;
    public TMP_Text calicoCatHPText;

    public PlayableCharacter blackCat;
    public PlayableCharacter grayCat;
    public PlayableCharacter calicoCat;

    private Dictionary<PlayableCharacter, TMP_Text> catHPTexts;

    void Start()
    {
        catHPTexts = new Dictionary<PlayableCharacter, TMP_Text>
        {
            { blackCat, blackCatHPText },
            { grayCat, grayCatHPText },
            { calicoCat, calicoCatHPText }
        };

        UpdateAllHPDisplays(); 
    }

    public void UpdateHP(PlayableCharacter cat)
    {
        if (catHPTexts.ContainsKey(cat))
        {
            catHPTexts[cat].text = " " + cat.CurrentHp;
        }
    }

    private void UpdateAllHPDisplays()
    {
        foreach (var entry in catHPTexts)
        {
            PlayableCharacter cat = entry.Key;
            TMP_Text hpText = entry.Value;
            hpText.text = " " + cat.CurrentHp;
        }
    }

   
    void Update()
    {
        UpdateAllHPDisplays();
    }
}
