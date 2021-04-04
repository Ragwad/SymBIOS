#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public static partial class Util
{
    [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(AnimatorInfos))]
    static void AnimatorInfos(MenuCommand command)
    {
        AnimatorController ctr = (AnimatorController)((Animator)command.context).runtimeAnimatorController;

        string layersEnum = "public enum Layers { ";
        var statesEnums = new System.Collections.Generic.List<string>();

        foreach (AnimatorControllerLayer layer in ctr.layers)
        {
            layersEnum += layer.name + ", ";

            string statesEnum = "public enum " + layer.name + "States\n{\n";

            foreach (ChildAnimatorState state in layer.stateMachine.states)
                statesEnum += "    " + state.state.name + " = " + state.state.nameHash + ",\n";

            statesEnums.Add(statesEnum + "}");
        }

        layersEnum += " }\n\n";

        foreach (string statesEnum in statesEnums)
            layersEnum += statesEnum + "\n\n";

        layersEnum += "public enum Parameters\n{\n";

        foreach (var p in ctr.parameters)
            layersEnum += "    " + p.name + " = " + p.nameHash + ",\n";

        layersEnum += "}";

        Debug.Log(layersEnum);
    }
}
#endif