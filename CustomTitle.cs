using System;
using System.Reflection;
using UnityEditor;


[InitializeOnLoad]
public static class CustomTitle
{
    public static string NewTitle = "NewTitle";

    public static string Title {
        set { NewTitle = value; ForceUpdateTitle(); } 
    }

    static MethodInfo UpdateMainWindowTitle = typeof(EditorApplication).GetMethod("UpdateMainWindowTitle", BindingFlags.NonPublic | BindingFlags.Static);
    static CustomTitle()
    {
        var a = typeof(EditorApplication).GetMethod("add_updateMainWindowTitle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        a.Invoke(null, new object[] { new Action<object>(handler) });
        ForceUpdateTitle();
    }
    public static void handler(object o)
    {
        var title = o.GetType().GetField("title");
        title.SetValue(o, NewTitle);
    }
    public static void ForceUpdateTitle()
    {
        UpdateMainWindowTitle.Invoke(null,new object[0]);
    }
}
