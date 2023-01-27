using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class EditorInput : MonoBehaviour
{
    int editorStatus;
    [SerializeField]
    private GameObject _editorCanvas1, _editorCanvas2;

    public InputActionReference toggleEditorCanvas;

    private void Start() {
        toggleEditorCanvas.action.started += ToggleEditors;
    }
    public void ToggleEditors(InputAction.CallbackContext context) {
        editorStatus++;
        editorStatus = editorStatus % 3;

        switch (editorStatus) {
            case 0:
                _editorCanvas1.SetActive(false);
                _editorCanvas2.SetActive(false);
                break;
            case 1:
                _editorCanvas1.SetActive(true);
                _editorCanvas2.SetActive(false);
                break;
            case 3:
                _editorCanvas1.SetActive(false);
                _editorCanvas2.SetActive(true);
                break;

        }
            
    }
}
