using System.Collections;
using UnityEngine;

public class UnitsActionsUI : MonoBehaviour {

    [SerializeField]
    private SkillButton _skillButtonPrefab;

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private SkillButton _moveButton;

    [SerializeField]
    private SkillButton _attackButton;

    [SerializeField]
    private Transform _upSkillsPanel;

    [SerializeField]
    private Transform _downSkillsPanel;

    [SerializeField]
    private float _appearTime;

    private Unit _unit;
    private UnitView _unitView;

    private Coroutine _appearCoroutine;

    private void Start() {
        Hide();
        Game.Loop.OnMoveEnded += Hide;
        Game.Loop.OnUnitDied += (unit) => {
            if (unit == _unit)
                Hide();
        };
    }

    public void SetUnit(Unit unit) {
        if (_unit == unit) {
            Hide();
            return;
        }

        if (_unitView != null)
            _unitView.ChangeIndicatorsVisibility(true);
        if (_appearCoroutine != null)
            StopCoroutine(_appearCoroutine);
        _appearCoroutine = StartCoroutine(AppearRoutine());

        _unit = unit;
        _unitView = unit.View;
        _unitView.ChangeIndicatorsVisibility(false);
        
        _moveButton.SetSkill(unit.Mover);
        _unit.Mover.OnUsed += Hide;

        _attackButton.SetSkill(_unit.Attacker);
        _unit.Attacker.OnUsed += Hide;

        _content.transform.position = _unitView.transform.position;
        _content.SetActive(true);
        Show();

        ClearSkillsPanels();

        if (unit.Cage.Y < Game.Map.Height / 2)
            SetSkillPanel(_downSkillsPanel, unit);
        else
            SetSkillPanel(_upSkillsPanel, unit);
    }

    private void Hide() {
        if (_unit != null) {
            _unitView.ChangeIndicatorsVisibility(true);
            _unit.Mover.OnUsed -= Hide;
            _unit.Attacker.OnUsed -= Hide;
        }
        _unit = null;
        _unitView = null;
        _content.SetActive(false);
    }

    private void Show() {
        _content.SetActive(true);
    }

    private IEnumerator AppearRoutine() {
        _content.transform.localScale = Vector3.zero;
        float time = 0;
        while (time < _appearTime) {
            time += Time.deltaTime;
            _content.transform.localScale = Vector3.one * Mathf.Lerp(0, 1, time/_appearTime);
            yield return null;
        }
        _content.transform.localScale = Vector3.one;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Game.CageChooseManager.CancelAction();
            Hide();
        }
    }

    private void ClearSkillsPanels() {
        foreach (Transform skillButton in _downSkillsPanel) {
            Destroy(skillButton.gameObject);
        }
        foreach (Transform skillButton in _upSkillsPanel) {
            Destroy(skillButton.gameObject);
        }
        _downSkillsPanel.gameObject.SetActive(false);
        _upSkillsPanel.gameObject.SetActive(false);
    }

    private void SetSkillPanel(Transform panel, Unit unit) {
        if (unit.ActiveSkills.Count == 0)
            return;

        foreach (var skill in unit.ActiveSkills) {
            var skillButton = Instantiate(_skillButtonPrefab, panel);
            skillButton.SetSkill(skill);
        }
        panel.gameObject.SetActive(true);
    }

}