using UnityEngine;
using System.Collections;

///////
///////
/////// Helpers for the GlobalConstants class
///////
///////
public static partial class GlobalConstants
{
    /// <summary>
    /// Finds game object with the name (parm: name).
    /// Basic error checking included.
    /// </summary>
    /// <param name="name">The name of the gameObject to look for.</param>
    /// <returns>The gameObject with the given name.</returns>
    private static GameObject FindGameObject(string name)
    {
        GameObject go = GameObject.Find(name);

        if (go == null)
        {
            Debug.LogError("GlobalConstants: No gameObjects with name=" + name + ", were found.");
        }

        return go;
    }


    /// <summary>
    /// Finds a component of the given type (type: T), on the GameObject with the name (parm: name).
    /// Basic error checking included.
    /// </summary>
    /// <typeparam name="T">The type of the comonent to get.</typeparam>
    /// <param name="name">The name of the gameObject to look for.</param>
    /// <returns>The compoent of type T on the gameObject with the given name</returns>
    private static T FindComponent<T>(string name)
    {
        GameObject go = FindGameObject(name);

        if(go == null)
        {
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
