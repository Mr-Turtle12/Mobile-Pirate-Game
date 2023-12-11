
public interface IMiniGamesController
{
    public int GetScore();
    public void StartminiGame();
    public void SetDuration(float time);
    public void IsRunning(bool running);
    public bool GameRunning();
    public string getNextScene();
}
