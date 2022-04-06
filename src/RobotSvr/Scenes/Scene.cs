namespace RobotSvr;

public class Scene
{
    public SceneType Scenetype;
    public RobotClient robotClient;

    public Scene(SceneType scenetype, RobotClient robotClient)
    {
        this.Scenetype = scenetype;
        this.robotClient = robotClient;
    }

    public virtual void Initialize()
    {

    }

    public virtual void OpenScene()
    {

    }

    public virtual void CloseScene()
    {

    }

    public virtual void OpeningScene()
    {

    }

    public virtual void PlayScene()
    {

    }
}