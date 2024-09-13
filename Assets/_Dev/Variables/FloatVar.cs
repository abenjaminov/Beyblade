using UnityEngine.Events;

public class FloatVar
{
    private float _value;
    public UnityAction OnValueChangeEvent;

    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;

            OnValueChangeEvent?.Invoke();
        }
    }

    public FloatVar(float initialValue)
    {
        Value = initialValue;
    }

    public static implicit operator float(FloatVar floatVar) => floatVar.Value;

    // Implicit conversion from float to FloatVar
    public static implicit operator FloatVar(float value)
    {
        return new FloatVar(value);
    }

    // Addition
    public static FloatVar operator +(FloatVar a, FloatVar b) => a.Value + b.Value;
    public static FloatVar operator +(FloatVar a, float b) => a.Value + b;
    public static FloatVar operator +(float a, FloatVar b) => a + b.Value;

    // Subtraction
    public static FloatVar operator -(FloatVar a, FloatVar b) => a.Value - b.Value;
    public static FloatVar operator -(FloatVar a, float b) => a.Value - b;
    public static FloatVar operator -(float a, FloatVar b) => a - b.Value;

    // Multiplication
    public static FloatVar operator *(FloatVar a, FloatVar b) => a.Value * b.Value;
    public static FloatVar operator *(FloatVar a, float b) => a.Value * b;
    public static FloatVar operator *(float a, FloatVar b) => a * b.Value;

    // Division
    public static FloatVar operator /(FloatVar a, FloatVar b) => a.Value / b.Value;
    public static FloatVar operator /(FloatVar a, float b) => a.Value / b;
    public static FloatVar operator /(float a, FloatVar b) => a / b.Value;

    // Modulus
    public static FloatVar operator %(FloatVar a, FloatVar b) => a.Value % b.Value;
    public static FloatVar operator %(FloatVar a, float b) => a.Value % b;
    public static FloatVar operator %(float a, FloatVar b) => a % b.Value;

    // Unary minus
    public static FloatVar operator -(FloatVar a) => -a.Value;

    // Comparison operators
    public static bool operator ==(FloatVar a, FloatVar b) => a.Value == b.Value;
    public static bool operator !=(FloatVar a, FloatVar b) => a.Value != b.Value;
    public static bool operator <(FloatVar a, FloatVar b) => a.Value < b.Value;
    public static bool operator >(FloatVar a, FloatVar b) => a.Value > b.Value;
    public static bool operator <=(FloatVar a, FloatVar b) => a.Value <= b.Value;
    public static bool operator >=(FloatVar a, FloatVar b) => a.Value >= b.Value;

    // Override Equals and GetHashCode
    public override bool Equals(object obj)
    {
        return obj is FloatVar variable &&
               Value == variable.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    // ToString override
    public override string ToString() => Value.ToString();
}