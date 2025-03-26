using System;

public class GameOverScreen : Window
{
    public override event Action ButtonClicked;

    protected override void ActionOnClick()
    {
        ButtonClicked?.Invoke();
    }
}