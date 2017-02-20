using UnityEngine;
using UnityEditor;
using System;


[CustomEditor(typeof(AmmoInventory))]
public class Editor_AmmoInventory : Editor {
	public override void OnInspectorGUI(){
		AmmoInventory myTarget = (AmmoInventory)target;

		foreach(GunType wt in Enum.GetValues(typeof(GunType))){
            if (myTarget.bullets.ContainsKey(wt) == false)
            {
                myTarget.bullets.Add(wt, 0);
            }
            myTarget.bullets[wt] = Convert.ToInt32(EditorGUILayout.TextField(wt.ToString(), myTarget.bullets[wt].ToString()));
        }





		this.Repaint();
	}

}
