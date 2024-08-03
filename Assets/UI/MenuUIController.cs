using UnityEngine;
using UnityEngine.UIElements;

public class MenuUIController : MonoBehaviour
{
    public MenuScript menuScript;
    public MusicController musicController;
    public SoundManager soundManager;
    public VisualTreeAsset mainMenuVTA;
    public VisualTreeAsset settingsVTA;
    public VisualTreeAsset creditsVTA;
    VisualElement root;
    Button newGameButton;
    Button creditsButton;
    Button settingsButton;
    Button exitButton;
    Button backButton;
    Slider volumeSlider;
    Toggle muteToggle;
    Label volumeLabel;



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
        };

        exitButton.clicked += () =>
        {
            menuScript.QuitGame();
        };
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
        volumeLabel = root.Q<Label>("VolumeLabel");
        muteToggle = root.Q<Toggle>("MuteToggle");

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
            muteToggle.RegisterValueChangedCallback(OnToggleValueChanged);
        }

        if (backButton != null)
        {
            backButton.clicked += () =>
            {
                LoadMainMenu();
            };
        }


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
        bool isChecked = evt.newValue;

        if(isChecked)
        {
            ToggleMusic();
        }
        else
        {
            ToggleMusic();
        }
    }

    private void ToggleMusic()
    {
        soundManager.IsMuted = !soundManager.IsMuted;
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
        creditsVTA.CloneTree(root);

        backButton = root.Q<Button>("BackButton");
        Label creditLabel = root.Q<Label>("CreditsText");

        if (creditLabel != null)
        {
           AnimateCredits(creditLabel);
        }
    }


    public void AnimateCredits(Label creditLabel)
    {
       // creditLabel.AddToClassList("CreditsTextEnd");
    }
}
