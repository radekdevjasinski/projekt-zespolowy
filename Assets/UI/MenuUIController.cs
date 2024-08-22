using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

public class MenuUIController : MonoBehaviour
{
    public MenuScript menuScript;
    public MusicController musicController;
    public SoundManager soundManager;
    public VisualTreeAsset mainMenuVTA;
    public VisualTreeAsset settingsVTA;
    public VisualTreeAsset creditsVTA;
    public VisualTreeAsset creditsVTAold;
    VisualElement root;
    Button newGameButton;
    Button creditsButton;
    Button settingsButton;
    Button exitButton;
    Button backButton;
    Slider volumeSlider;
    Toggle muteToggle;
    Label volumeLabel;
    Button polishButton;
    Button englishButton;
    Label settingsLabel;
    Label sliderLabel;
    Label muteLabel;
    Label creditLabel;
    public bool toggleCredits = false;


    [SerializeField] private Locale polishLocale;
    [SerializeField] private Locale englishLocale;



    private const float AnimationDuration = 10f; 

    public void OnEnable()
    {
    
        root = GetComponent<UIDocument>().rootVisualElement;
        LoadMainMenu();
       

    }

    private void Update()
    {   
        if(volumeSlider != null)
        UpdateVolumeLabel(volumeSlider.value);

        if(toggleCredits == true)
        {
          //  StartCoroutine(ToggleClassRoutine());
        }
        UpdateUI();
    }

    private void ChangeLanguage(Locale locale)
    {
        if (locale != null)
        {
            Debug.Log("Changing language to: " + locale.name);
            LocalizationSettings.SelectedLocale = locale;
            UpdateUI();
        }
        else
        {
            Debug.LogError("Locale is null");
            UpdateUI();
        }
    }

    public void LoadMainMenu()
    {
        root.Clear();
        mainMenuVTA.CloneTree(root);

        newGameButton = root.Q<Button>("NewGameButton");
        creditsButton = root.Q<Button>("CreditsButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");

        newGameButton.clicked += () =>
        {
            menuScript.NewGame();
        };

        settingsButton.clicked += () =>
        {
            soundManager = SoundManager.instance;
            LoadSettings();
            UpdateUI();

            /* AudioListener audioListener = FindObjectOfType<AudioListener>();
              if (audioListener != null)
              {
                  Debug.Log("AudioListener found.");
                  volumeSlider.value = AudioListener.volume;


              }
              else
              {
                  Debug.LogWarning("No AudioListener found in the scene.");
              }*/
        };

        creditsButton.clicked += () =>
        {
            LoadCredits();
            menuScript.ToggleCredits();

            // toggleCredits = true;
        };

        exitButton.clicked += () =>
        {
            menuScript.QuitGame();
        };

        UpdateUI();
    }
    private void UpdateVolumeLabel(float value)
    {
        float percentage = value * 100;
        volumeLabel.text = $"{percentage:0}%";
    }

    public void LoadSettings()
    {
        root.Clear();
        settingsVTA.CloneTree(root);
     

        backButton = root.Q<Button>("BackButton");
        volumeSlider = root.Q<Slider>("VolumeSlider");
        sliderLabel = volumeSlider.Q<Label>();
        volumeLabel = root.Q<Label>("VolumeLabel");
        muteToggle = root.Q<Toggle>("MuteToggle");
        muteLabel = muteToggle.Q<Label>();
        polishButton = root.Q<Button>("PolishButton");
        englishButton = root.Q<Button>("EnglishButton");
        settingsLabel = root.Q<Label>("SettingsLabel");
      

/*        if (soundManager == null)
        {
            Debug.LogError("SoundManager instance not found");
            return;
        }*/


        if (volumeSlider != null)
        {
            volumeSlider.lowValue = 0f;
            volumeSlider.highValue = 1f;
            volumeSlider.value = soundManager.MusicVolume;
           
            Debug.Log($"VolumeSlider value set to: {volumeSlider.value}");
            volumeSlider.RegisterValueChangedCallback(OnVolumeChanged);
        }

        if(muteToggle != null)
        {
            muteToggle.value = soundManager.IsMuted; 
            muteToggle.RegisterValueChangedCallback(OnToggleValueChanged);
        }

        if (backButton != null)
        {
            backButton.clicked += () =>
            {
                LoadMainMenu();
            };
        }

        if(englishButton != null)
        {
            englishButton.clicked += () =>
            {
                ChangeLanguage(englishLocale);
            };
        }

        if(polishButton != null)
        {
            polishButton.clicked += () =>
            {
                ChangeLanguage(polishLocale);
            };
        }

        UpdateUI();
    }

    private void OnVolumeChanged(ChangeEvent<float> evt)
    {
        float volume = evt.newValue;
        soundManager.MusicVolume = volume;
        soundManager.SaveSettings();

        AudioSource[] audioSources = soundManager.GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }

    }

    private void OnToggleValueChanged(ChangeEvent<bool> evt)
    {
        soundManager.IsMuted = evt.newValue;
        soundManager.SaveSettings();

 
        AudioSource[] audioSources = soundManager.GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = soundManager.IsMuted;
        }
    }

    public void LoadCredits()
    {
        root.Clear();
        //creditsVTA.CloneTree(root);
        creditsVTAold.CloneTree(root);

        backButton = root.Q<Button>("BackButton");
        //Label creditLabel = root.Q<Label>("CreditsLabel");
        //creditLabel.AddToClassList("CreditsTextEnd"); // Dodaj klasê CSS

        if (backButton != null)
        {
            backButton.clicked += () =>
            {
                Debug.Log("Wyjscie do menu...");
                toggleCredits = false;
                Debug.Log("ToggleCredits = " + toggleCredits);
                menuScript.ToggleCredits();
                LoadMainMenu();
                
            };
        }

        if (creditLabel != null)
        {

            toggleCredits = true;
            Debug.Log("Wejœcie w creditsy...");
            Debug.Log("toggleCredits = " + toggleCredits);
            Debug.Log("Start korutyny");
            if (toggleCredits == true)
            {
                StartCoroutine(ToggleClassRoutine(toggleCredits, creditLabel));
            }
        }
        UpdateUI();
    }


    public void AnimateCredits(Label creditLabel)
    {
       // creditLabel.AddToClassList("CreditsTextEnd");
    }
    private IEnumerator ToggleClassRoutine(bool toggleCredits, Label creditLabel)
    {

        while (toggleCredits == true) // Pêtla wykonuj¹ca siê w nieskoñczonoœæ
        {
            creditLabel.AddToClassList("CreditsTextEnd"); // Dodaj klasê CSS
            yield return new WaitForSeconds(25f); // Czekaj 25 sekund

            creditLabel.RemoveFromClassList("CreditsTextEnd"); // Usuñ klasê CSS
        }
    }
    private void UpdateUI()
    {
        // Update text for buttons and labels according to the selected locale
        if (newGameButton != null) newGameButton.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "NewGame");
        if (creditsButton != null) creditsButton.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Credits");
        if (settingsButton != null) settingsButton.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Settings");
        if (exitButton != null) exitButton.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Exit");
        //if (volumeLabel != null) volumeLabel.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Volume")
       // if (polishButton != null) polishButton.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Polish");
       // if (englishButton != null) englishButton.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "English");
        if (backButton != null) backButton.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Back");
        if (settingsLabel != null) settingsLabel.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Settings");
        if (sliderLabel != null) sliderLabel.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "MusicSound");
        if (muteLabel != null) muteLabel.text = LocalizationSettings.StringDatabase.GetLocalizedString("StringsUI", "Mute");
        if (creditLabel != null) creditLabel.text = LocalizationSettings.StringDatabase.GetLocalizedString("Menu", "Credits");
    }
}
