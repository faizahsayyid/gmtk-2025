using System.Collections.Generic;

public sealed class SceneNames
{
    public const string Scene1 = "FaizahScene";
    public const string Scene2 = "FaizahScene2";
}

public sealed class SceneDirections
{ 
    public const string Right = "right";
    public const string Left = "left";
}

class NextScene
{
    public string rightScene;
    public string leftScene;

    public NextScene(string right = null, string left = null, string up = null, string down = null)
    {
        rightScene = right;
        leftScene = left;
    }
}
public class SceneConfig
{
    private Dictionary<string, NextScene> scenes = new Dictionary<string, NextScene>();
    private NextScene scene1 = new NextScene(right: SceneNames.Scene2);
    private NextScene scene2 = new NextScene(left: SceneNames.Scene1);

    public SceneConfig()
    {
        scenes.Add(SceneNames.Scene1, scene1);
        scenes.Add(SceneNames.Scene2, scene2);
    }

    public string GetNextScene(string sceneName, string direction)
    {
        if (scenes.TryGetValue(sceneName, out NextScene nextScene))
        {
            switch (direction)
            {
                case SceneDirections.Right:
                    return nextScene.rightScene;
                case SceneDirections.Left:
                    return nextScene.leftScene;
                default:
                    return null;
            }
        }
        return null;
    }
}