using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Commander : MonoBehaviour
{
    public static Commander instance;

    public InputActionAsset actions;
    public static CurrentController currentController;
    public InputActionReference clickActionReference;

    public static float inputX;
    public static float inputZ;

    public static Action<Vector3> Click = (position) => { };
    public static Action Move = () => { };
    public static Action Skill = () => { };
    public static Action Confirm = () => { };
    public static Action Menu = () => { };
    public static Action Cancel = () => { };

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        clickActionReference.action.performed += OnMovePerformed;

        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void OnMove(InputValue value)
    {
        inputX = value.Get<Vector2>().x;
        inputZ = value.Get<Vector2>().y;
        Move.Invoke();
    }

    public void OnMove2(InputValue value)
    {
        inputX = value.Get<Vector2>().x;
        inputZ = value.Get<Vector2>().y;
        Move.Invoke();
    }

    void OnClick()
    {
        Click.Invoke(Mouse.current.position.ReadValue());
    }

    void OnSkill()
    {
        Skill.Invoke();
    }

    void OnConfirm()
    {
        Confirm.Invoke();
    }
    
    void OnMenu()
    {
        Menu.Invoke();
    }

    void OnCancel()
    {
        Cancel.Invoke();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        if (device is Gamepad)
        {
            currentController = CurrentController.GamePad;
            print("Pad");
        }
        else if (device is Keyboard)
        {
            currentController = CurrentController.PC;
        }
    }

    public void SaveInput()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public static int GetController()
    {
        switch (currentController)
        {
            case CurrentController.PC:
                return 0;
            case CurrentController.GamePad:
                return 1;

            default:
                return 0;
        }
    }
}
