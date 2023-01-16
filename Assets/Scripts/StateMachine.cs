using System.Collections.Generic;

public class StateMachine
{
    private readonly Dictionary<string, State> _states = new();

    private readonly Stack<string> _currentStateNames = new();

    public string InitialState
    {
        set
        {
            _currentStateNames.Push(value);
            CurrentState.OnEnter();
        }
    }

    public State CurrentState => _states[_currentStateNames.Peek()];

    public void Register(string stateName, State state)
    {
        state.StateMachine = this;
        _states.Add(stateName, state);
    }

    public void MoveTo(string stateName)
    {
        CurrentState.OnExit();

        _currentStateNames.Pop();
        _currentStateNames.Push(stateName);

        CurrentState.OnEnter();
    }


    public void Push(string stateName)
    {
        _currentStateNames.Push(stateName);

        CurrentState.OnEnter();
    }

    public void Pop()
    {
        CurrentState.OnExit();

        _currentStateNames.Pop();
    }
}