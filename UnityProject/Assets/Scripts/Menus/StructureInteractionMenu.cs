using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class StructureInteractionMenu : MonoBehaviour, IMenu {
	public static StructureInteractionMenu Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI structureName;
	[SerializeField] private Transform       structureSpecificPanel;

	private Structure _currentStructure;
	private Transform _currentStructurePanel;

	public StructureInteractionMenu() {
		Instance = this;
	}

	public void Enable(Structure structure) {
		
		_currentStructure      = structure;
		MenuHandler.EnableMenu(this);
	}

	public void OnExitClicked() {
		MenuHandler.DisableMenu();
		Reset();
	}

	public void OnMoveClicked() {
		try {
			MenuHandler.EnableMenu(BuildMenu.Instance);
			_currentStructure.Move();
			Reset();
		} catch (NullReferenceException) { }
	}
	
	public void OnRemoveClicked() {
		try {
			MenuHandler.DisableMenu();
			_currentStructure.Remove();
			Reset();
		} catch (NullReferenceException) { }
	}

	private void Reset() {
		_currentStructure      = null;
		_currentStructurePanel = null;
	}
	
	#region IMenu
	
	[Obsolete(IMenu.EnableObsoleteMessage, true)]
	public void Enable() {
		if (_currentStructure == null) {
			return;
		}

		_currentStructure.OnMenuEnable();
		InputActions.SIMenu.Enable();
		
		_currentStructurePanel = Instantiate(_currentStructure.MenuPanel, structureSpecificPanel);
		structureName.text = _currentStructure.StructureName;

		gameObject.SetActive(true);
		
	}

	[Obsolete(IMenu.DisableObsoleteMessage, true)]
	public void Disable() {
		_currentStructure.OnMenuDisable();
		InputActions.SIMenu.Disable();
		
		gameObject.SetActive(false);
		
		Destroy(_currentStructurePanel.gameObject);
	}

	#endregion
}