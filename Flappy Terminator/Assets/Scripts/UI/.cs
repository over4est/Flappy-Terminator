using System;

public class StartScreen : Window
{
    public override event Action ButtonClicked;

    protected override void ActionOnClick()
    {
        ButtonClicked?.Invoke();
    }
}