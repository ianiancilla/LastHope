using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_BindingIcon : MonoBehaviour
{
    [SerializeField]
    private InputActionReference action;

    [SerializeField]
    private TMPro.TextMeshProUGUI bindingLabelTMP;

    [SerializeField] private string bindingTextAfterAction;

    private InputActionReference actionReference
    {
        get => action;
        set
        {
            action = value;
            UpdateBindingDisplay();
        }
    }

    private void OnEnable()
    {
        RebindSavingLoading.KeyRebind += UpdateBindingDisplay;
        UpdateBindingDisplay();
    }

    private void OnDisable()
    {
        RebindSavingLoading.KeyRebind -= UpdateBindingDisplay;
    }


    public void UpdateBindingDisplay()
    {
        var displayString = string.Empty;
        var deviceLayoutName = default(string);
        var controlPath = default(string);

        // Get display string from action.
        var action = this.action?.action;
        if (action != null)
        {
            var bindingIndex = 1;
            if (bindingIndex != -1)
                displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath).ToUpper()
                                + bindingTextAfterAction;
        }

        // Set on label (if any).
        if (bindingLabelTMP != null)
            bindingLabelTMP.text = displayString;
    }


}
