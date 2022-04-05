namespace RobotSvr;

public class Scene
{
    public SceneType Scenetype;

    public Scene(SceneType scenetype)
    {
        this.Scenetype = scenetype;
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