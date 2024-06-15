using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

public class LanguageAreaChanger : ActivatableAreaObjectTrigger
{
    [SerializeField] private Locale local;

    public override bool activate(GameObject player)
    {
        Debug.Log("acitvated lagnaue chagner");
        LocalizationSettings.SelectedLocale = local;
        return true;
    }
}
