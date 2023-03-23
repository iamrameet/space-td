namespace Util;
using Godot;
using System.Collections.Generic;

class Util{
  private static int maxId = 0;
  public delegate void Handler();
  private static Dictionary<int, Timer> timeouts = new Dictionary<int, Timer>();
  public static int setTimeout(Node node, Handler handler, int timeout){
    int id = ++maxId;
    SceneTree sceneTree = node.GetTree();
    Timer timer = new Timer();
    SceneTreeTimer time = sceneTree.CreateTimer(timeout / 1000);
    timeouts.Add(id, timer);
    timer.Connect("timeout", new Callable(timer, nameof(handler)));
    timer.ToSignal(timer, "timeout").OnCompleted(() => {
      handler();
      clearTimeout(id);
    });
    timer.Start(timeout / 1000);
    return id;
  }
  static bool clearTimeout(int id){
    timeouts[id].Stop();
    return timeouts.Remove(id);
  }
};