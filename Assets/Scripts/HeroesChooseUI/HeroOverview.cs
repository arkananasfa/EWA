using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroOverview : MonoBehaviour {

    public HeroType HeroType { get; set; }

    public bool WasUsed { get; set; }

    [SerializeField]
    private HeroesChooseUI _heroChooseUI;

    [SerializeField]
    private Image _heroIcon;

    [SerializeField]
    private UnitInfoPanel _heroInfoPanel;

    [SerializeField]
    private TextMeshProUGUI _heroNameText;

    [SerializeField]
    private TextMeshProUGUI _heroDescriptionText;

    [SerializeField]
    private Button _pickHeroButton;

    [SerializeField]
    private GameObject _overviewHidePanel;

    [SerializeField]
    private TextMeshProUGUI _overviewHideText;

    [SerializeField]
    private int _playerNumber;

    private bool _isPlayers;

    private void Start() {
        _isPlayers = Game.Network.IsPlayersNumber(_playerNumber);
        _overviewHidePanel.SetActive(!_isPlayers);
        _overviewHideText.text = "Picking...";
        _pickHeroButton.onClick.AddListener(() => {
            _heroChooseUI.PickHero();
        });

        SetHero(HeroType.HolyPrincess);
    }

    public void SetHero(HeroType heroType) {
        WasUsed = true;
        HeroType = heroType;
        if (_isPlayers) {
            _heroChooseUI.gameObject.SetActive(true);
            _overviewHidePanel.SetActive(false);
            Hero hero = Game.UnitsFactory.CreateHero(heroType);
            _heroIcon.sprite = Game.HeroesArchive.GetSpriteByUnitType(HeroType);
            _heroInfoPanel.SetUnitWithoutSprite(hero, true);
            NameDescriptionJSON heroVisual = LanguageManager.GetHeroVisual(hero.Code);
            _heroNameText.text = heroVisual.Name;
            _heroDescriptionText.text = heroVisual.Description;
        } else {
            _overviewHideText.text = "Picked!";
        }

    }

    public void HidePickButton() {
        _pickHeroButton.gameObject.SetActive(false);
    }

}