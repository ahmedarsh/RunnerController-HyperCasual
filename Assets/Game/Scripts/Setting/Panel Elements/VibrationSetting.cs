public class VibrationSetting : ButtonToggleBase
{
    protected override void OnOnSelected()
    {
        Vibration.MuteVibration = false;
    }

    protected override void OnOffSelected()
    {
        Vibration.MuteVibration = true;
    }
}