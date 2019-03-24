using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public enum State { Red, Orange, Green, Out };
    public State currentState;
    Renderer rend;
    TCar subscriber;
    public State setState;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        EnterState(State.Red);
    }

    private void EnterState(State state)
    {
        Color color = Color.white;
        currentState = state;
        if (currentState == State.Red) color = Color.red;
        if (currentState == State.Orange) color = new Color(255,165,0);
        if (currentState == State.Green) color = Color.green;
        rend.material.SetColor("_Color", color);
    }

    public void Subscribe(TCar subscriber)
    {
        this.subscriber = subscriber;
    }

    // Update is called once per frame
    void Update()
    {
        if (subscriber)
        {
            if (currentState == State.Red) subscriber.Stop();
            if (currentState == State.Orange) subscriber.Stop();
            if (currentState == State.Green) subscriber.Drive();
        }
        if (setState != State.Out)
        {
            if(setState == State.Green) EnterState(State.Green);
            if (setState == State.Orange) EnterState(State.Orange);
            if (setState == State.Red) EnterState(State.Red);
            setState = State.Out;
        }
    }
}
