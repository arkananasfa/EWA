using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CageView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [SerializeField]
    private Color _pointerOnHandlerColorDifference = new Color(0.2f, 0.2f, 0.2f);

    [SerializeField]
    private Color _markColorDifference = new Color(0.2f, 0f, 0.2f);

    [Inject] 
    private CageChooseManager _cageChooseManager;

    private Cage _context;
    private Image _image;

    private bool _marked;
    private bool _pointed;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    public CageView SetContext(Cage cage) {
        _context = cage;
        return this;
    }

    public void UnitEnter(Unit unit) {
        _image.color = unit.Team.Color;
        _image.color.ChangeAlpha(1f);
    }

    public void UnitExit(Unit unit) {
        _image.color = Color.white;
        _image.color.ChangeAlpha(1f);
    }

    public void Mark() {
        _image.color -= _markColorDifference;
        _marked = true;
    }

    public void Unmark() {
        if (_marked) {
            _image.color += _markColorDifference;
            _marked = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (_marked) {
            _image.color -= _pointerOnHandlerColorDifference;
            _pointed = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_pointed) {
            _image.color += _pointerOnHandlerColorDifference;
            _pointed = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            Unmark();
            OnPointerExit(eventData);
            Game.CageChooseManager.ChooseCage(_context);
        }
    } 

}