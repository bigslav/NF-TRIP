// Make sure this script is saved inside a folder called "Editor" in you projects Assets folder. Otherwise you'll recieve errors when trying to create a build opf your game.

using UnityEditor;            // Allows us access to methods that can make edits inside the Unity Editor.

// This class will be used to read the Material Types we created for the 'FMODStudioFirstPersonFootsteps' script, and then add them as options to select within a dropdown menu on the 'FMODStudioMaterialSetter' script. It will also read which material type we set for each surface the player can walk on and set the 'MaterialValue' variable to the value used to represent that type.
[CustomEditor(typeof(FMODStudioMaterialSetter))]                                                               // Using this Attribute, we can set which script we want to make edits to inside the Editor.
public class FMODStudioFootstepsEditor : Editor                                                                // Because this script won't be attached to a component, we don't want to derive from the 'Monobehaviour' class. Because we want it to edit an already exisiting script, we want it to derive from the 'Editor' class instead.
{
    public override void OnInspectorGUI()                                                                      // Unity uses this method to display fileds and variables for our scripts inside Unity's Editor in the Inspector tab. By including the keyword 'override' we're saying that we want to not display all of the variables for the 'MaterialSetter' script that normally would be, and instead display whatever we put inside this method. It runs, whenever a change to the object in question has been made via the inspector tab.
    {
        var MS = target as FMODStudioMaterialSetter;                                                           // We need to access our 'MaterialSetter' scripts to edit it. So we create a variable called MS and by using 'target' we can target any instances of a script that this class is editing. In our case, it's the 'MaterialSetter' script (remember we set this in the "CustomEditor" attribute). We can now access the 'MaterialSetter' script attached to our surfaces and the variables inside of them using MS.
        var FPF = FindObjectOfType<PlayerFootstepsMushroom>();                                          // We create a variable called FPF to read variables from it. Unfortuantly, because we're not editing this script in this class, we can't use 'target' to find it. So instead we use 'FindObjectOfType' instead.

        MS.MaterialValue = EditorGUILayout.Popup("Set Material As", MS.MaterialValue, FPF.MaterialTypes);      // 'EditorGUILayout.Popup' creates a dropdown menu inside our inspector tab for us to use to set the material of the surface we're editing. We give it 3 arguments. The first is a string that simply labels it. The second is an int value that our menu will show us in the inspector as selected. The third is a range of string options for us to select. At the beginning of this line, we're setting our 'MaterialValue' variable to match the result of whatever option we select in this menu when we next interact with then inspector tab.
    }
}


// This class will be used to read the Material Types we created for the 'FMODStudioFirstPersonFootsteps' script, and then add them as options to select within a dropdown menu as a default for whenever the player walks on top of a surface that doesn't have a 'MaterialSetter' script attached to it. It then read which type we selected and set a value to the 'DefulatMaterialValue' variable to represent that type.
[CustomEditor(typeof(InputHandler))]
public class FMODStudioFootstepsEditorTwo : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();                                                                                                       // This method will display all of the default inspector serialized variables and fields we created inside the 'FirstPersonFootsteps' script whilst still allowing us to overide the inspector and add new ones.

        var FPF = target as PlayerFootstepsMushroom;                                                                           // We create a variable to store a reference to the script we want to edit. We can then use 'target' to find it.

        FPF.DefulatMaterialValue = EditorGUILayout.Popup("Set Default Material As", FPF.DefulatMaterialValue, FPF.MaterialTypes);     // We then create another dropdown menu using 'EditorGUILayout.Popup'. This time we call it "Set Default Material As", we chose the 'DefaultMaterialValue' as the int we want to be see as selected and set the options to the same array of strings, 'MaterialTypes'. We then set the result of our dropdown menu to the 'DefaultMaterialValue' int for the 'FirstPersonFootsteps' script to use.
    }
}
