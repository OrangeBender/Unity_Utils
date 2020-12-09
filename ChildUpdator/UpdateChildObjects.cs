using UnityEngine;

public class UpdateChild{

    public static void recursiveCallOnChild (GameObject _obj)
    {
        if (_obj == null)
            return;

        //Changes to the current object

        foreach (Transform _child in _obj.transform)
        {
            if (_child == null)
                continue;

            recursiveCallOnChild(_child.gameObject);
        }
    }
}