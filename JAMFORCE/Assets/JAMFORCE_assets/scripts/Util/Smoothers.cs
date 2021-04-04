using UnityEngine;

public abstract class Smooth<T> where T : struct
{
    public T value, target;
    protected T current;

    //------------------------------------------------------------------------------------------------------------------------------

    public Smooth(T init = default) 
        => value = target = init;

    //------------------------------------------------------------------------------------------------------------------------------

    protected bool isDone => value.Equals(target) && current.Equals(default);
    public abstract void SmoothDamp(float damp, float deltaTime);
    public abstract void SmoothDamp(float damp, float spring, float deltaTime);
}

[System.Serializable]
public class SmoothFloat : Smooth<float>
{
    public SmoothFloat(float init = default) : base(init) { }

    //------------------------------------------------------------------------------------------------------------------------------

    public override void SmoothDamp(float damp, float deltaTime)
    {
        if (!isDone)
            value = Mathf.SmoothDamp(value, target, ref current, damp, Mathf.Infinity, deltaTime);
    }

    public override void SmoothDamp(float damp, float spring, float deltaTime)
    {
        if (!isDone)
            value = Mathf.SmoothDamp(value, spring * target - (spring - 1) * value, ref current, damp, Mathf.Infinity, deltaTime);
    }
}

[System.Serializable]
public class SmoothVector2 : Smooth<Vector2>
{
    public SmoothVector2(Vector2 init = default) : base(init) { }

    //------------------------------------------------------------------------------------------------------------------------------

    public override void SmoothDamp(float damp, float deltaTime)
    {
        if (!isDone)
            value = Vector2.SmoothDamp(value, target, ref current, damp, Mathf.Infinity, deltaTime);
    }

    public override void SmoothDamp(float damp, float spring, float deltaTime)
    {
        if (!isDone)
            value = Vector2.SmoothDamp(value, spring * target - (spring - 1) * value, ref current, damp, Mathf.Infinity, deltaTime);
    }
}

[System.Serializable]
public class SmoothVecotr3 : Smooth<Vector3>
{
    public SmoothVecotr3(Vector3 init = default) : base(init) { }

    //------------------------------------------------------------------------------------------------------------------------------

    public override void SmoothDamp(float damp, float deltaTime)
    {
        if (!isDone)
            value = Vector3.SmoothDamp(value, target, ref current, damp, Mathf.Infinity, deltaTime);
    }

    public override void SmoothDamp(float damp, float spring, float deltaTime)
    {
        if (!isDone)
            value = Vector3.SmoothDamp(value, spring * target - (spring - 1) * value, ref current, damp, Mathf.Infinity, deltaTime);
    }
}