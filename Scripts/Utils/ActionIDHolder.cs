using Managers;

public static class ActionIDHolder
{
    // Show Gameplay UI event ID
    private static int _gameplayPanelShowID = ActionManager.GetTriggerIndex();
    public static int GameplayPanelShowID { get { return _gameplayPanelShowID; } }

    // Hide gameplay UI event ID

    private static int _gameplayHideID = ActionManager.GetTriggerIndex();
    public static int GameplayHideID { get { return _gameplayHideID; } }

    // Hide TapToPlay UI event ID
    private static int _tapToPlayPanelHideID = ActionManager.GetTriggerIndex();
    public static int TapToPlayPanelHideID { get { return _tapToPlayPanelHideID; } }

    // Show tap to play UI event ID
    private static int _tapToPlayPanelShowID = ActionManager.GetTriggerIndex();
    public static int TapToPlayPanelShowID { get { return _tapToPlayPanelShowID; } }

    // Prepare level event ID
    private static int _prepareNextLevelID = ActionManager.GetTriggerIndex();
    public static int PrepareNextLevelID { get { return _prepareNextLevelID; } }
    
    // Level failed event ID
    private static int _onLevelFailedID = ActionManager.GetTriggerIndex();
    public static int OnLevelFailedID { get { return _onLevelFailedID; } }

    // Level completed event ID
    private static int _onLevelCompletedID = ActionManager.GetTriggerIndex();
    public static int OnLevelCompletedID { get { return _onLevelCompletedID; } }

    // Level is preparing event ID
    private static int _onLevelPreparedID = ActionManager.GetTriggerIndex();
    public static int OnLevelPreparedID { get { return _onLevelPreparedID; } }
    
    // Level completed event ID
    private static int _onLevelStartedID = ActionManager.GetTriggerIndex();
    public static int OnLevelStartedID { get { return _onLevelStartedID; } }

    // Level is resetting event ID
    private static int _levelResettedID = ActionManager.GetTriggerIndex();
    public static int OnLevelResettedID { get { return _levelResettedID; } }
    
    // Level is resetting event ID
    private static int _coinPickUp = ActionManager.GetTriggerIndex();
    public static int CoinPickUp { get { return _coinPickUp; } }

}
