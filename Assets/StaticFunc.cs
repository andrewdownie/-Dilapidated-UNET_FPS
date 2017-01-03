using UnityEngine;

/// <summary>
/// A collection of static functions.
/// </summary>
public static class StaticFunc {


    /// <summary>
    /// Finds a component of the given type (type: T), on the GameObject with the name (parm: name).
    /// Basic error checking included.
    /// </summary>
    /// <typeparam name="T">The type of the comonent to get.</typeparam>
    /// <param name="name">The name of the gameObject to look for.</param>
    /// <returns>The compoent of type T on the gameObject with the given name</returns>
    public static T FindComponent<T>(string name)
    {
        GameObject go = GameObject.Find(name);

        if (go == null)
        {
            Debug.LogError("GlobalConstants: No gameObjects with name=" + name + ", were found.");
            return default(T);
        }

        T component = go.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("GlobalConstants[FindComponent]: gameObject with name=" + name + ", did not have a component of type=" + component.ToString());
        }

        return component;
    }
}
