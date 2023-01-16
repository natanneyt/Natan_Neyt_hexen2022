﻿public abstract class State
{
    public StateMachine StateMachine { get; set; }

    public virtual void OnEnter() { }

    public virtual void OnExit() { }
}