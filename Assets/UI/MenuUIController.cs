using UnityEngine;
using UnityEngine.UIElements;

public class MenuUIController : MonoBehaviour
{
    public MenuScript menuScript;
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
            LoadSettings();

            AudioListener audioListener = FindObjectOfType<AudioListener>();
            if (audioListener != null)
            {
                Debug.Log("AudioListener found.");
                volumeSlider.value = AudioListener.volume;

               
            }
            else
            {
                Debug.LogWarning("No AudioListener found in the scene.");
            }
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
        volumeSlider.RegisterValueChangedCallback(OnVolumeChanged);

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
        // Ustaw globaln¹ g³oœnoœæ AudioListener na podstawie wartoœci suwaka
        AudioListener.volume = evt.newValue;

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
