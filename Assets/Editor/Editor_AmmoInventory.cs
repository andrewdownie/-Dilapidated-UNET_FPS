using UnityEngine;
using UnityEditor;
using System;


[CustomEditor(typeof(AmmoInventory))]
public class Editor_AmmoInventory : Editor {
	public override void OnInspectorGUI(){
		AmmoInventory myTarget = (AmmoInventory)target;

        Array types = Enum.GetValues(typeof(GunType));
        
        if(types.Length != myTarget.bullets.Count){
            foreach(GunType wt in types){
                if (myTarget.bullets.ContainsKey(wt) == false)
                {
                    myTarget.bullets.Add(wt, 0);
                }
            }
        }
        
		foreach(GunType wt in types){
            myTarget.bullets[wt] = Convert.ToInt32(EditorGUILayout.TextField(wt.ToString(), myTarget.bullets[wt].ToString()));
        }





		this.Repaint();
	}

}
