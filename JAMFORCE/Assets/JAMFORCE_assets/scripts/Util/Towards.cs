using UnityEngine;

public abstract class Toward<T> where T : struct
{
    public T value, target;

    //------------------------------------------------------------------------------------------------------------------------------

    public Toward(T init = default)
        => value = target = init;

    //------------------------------------------------------------------------------------------------------------------------------

    public abstract bool Towards(float speed, float deltaTime, bool angle);
}

[System.Serializable]
public class TowardFloat : Toward<float>
{
    public TowardFloat(float init = default) : base(init)
    {

    }

    //------------------------------------------------------------------------------------------------------------------------------

    public override bool Towards(float speed, float deltaTime, bool angle)
    {
        if (value == target)
            return false;

        if (angle)
            value = Mathf.MoveTowardsAngle(value, target, speed * deltaTime);
        else
            value = Mathf.MoveTowards(value, target, speed * deltaTime);

        return true;
    }
}

[System.Serializable]
public class TowardVecot2 : Toward<Vector2>
{
    public TowardVecot2(Vector2 init = default) : base(init)
    {

    }

    //------------------------------------------------------------------------------------------------------------------------------

    public override bool Towards(float speed, float deltaTime, bool angle)
    {
        if (value == target)
            return false;

        if (angle)
            value = Vector3.RotateTowards(value, target, speed * deltaTime, 1);
        else
            value = Vector2.MoveTowards(value, target, speed * deltaTime);

        return true;
    }
}

[System.Serializable]
public class TowardVecot3 : Toward<Vector3>
{
    public TowardVecot3(Vector3 init = default) : base(init)
    {

    }

    //------------------------------------------------------------------------------------------------------------------------------

    public override bool Towards(float speed, float deltaTime, bool angle)
    {
        if (value == target)
            return false;

        if (angle)
            value = Vector3.RotateTowards(value, target, speed * deltaTime, 1);
        else
            value = Vector3.MoveTowards(value, target, speed * deltaTime);

        return true;
    }
}