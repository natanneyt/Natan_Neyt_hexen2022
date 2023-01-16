using System;
using UnityEngine;

public class StartState : State
{
    private StartView _menuView;

    public override void OnEnter()
    {
        base.OnEnter();
        InitializeScene();
    }

    private void InitializeScene()
    {
        _menuView = GameObject.FindObjectOfType<StartView>();
        if (_menuView != null)
            _menuView.PlayClicked += OnPlayClicked;
    }


    public override void OnExit()
    {
        base.OnExit();

        if (_menuView != null)
            _menuView.PlayClicked -= OnPlayClicked;
    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        StateMachine.MoveTo(States.Playing);
        _menuView.gameObject.SetActive(false);
    }
}