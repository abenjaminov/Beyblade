using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floats", menuName = "Variables/Float", order = 1)]
public class FloatVariable : ScriptableObject
{
    public float Value;

    // Implicit conversion from FloatVariable to float
    public static implicit operator float(FloatVariable floatVar) => floatVar.Value;

    // Implicit conversion from float to FloatVariable
    public static implicit operator FloatVariable(float value)
    {
        FloatVariable floatVar = CreateInstance<FloatVariable>();
        floatVar.Value = value;
        return floatVar;
    }

    // Addition
    public static FloatVariable operator +(FloatVariable a, FloatVariable b) => a.Value + b.Value;
    public static FloatVariable operator +(FloatVariable a, float b) => a.Value + b;
    public static FloatVariable operator +(float a, FloatVariable b) => a + b.Value;

    // Subtraction
    public static FloatVariable operator -(FloatVariable a, FloatVariable b) => a.Value - b.Value;
    public static FloatVariable operator -(FloatVariable a, float b) => a.Value - b;
    public static FloatVariable operator -(float a, FloatVariable b) => a - b.Value;

    // Multiplication
    public static FloatVariable operator *(FloatVariable a, FloatVariable b) => a.Value * b.Value;
    public static FloatVariable operator *(FloatVariable a, float b) => a.Value * b;
    public static FloatVariable operator *(float a, FloatVariable b) => a * b.Value;

    // Division
    public static FloatVariable operator /(FloatVariable a, FloatVariable b) => a.Value / b.Value;
    public static FloatVariable operator /(FloatVariable a, float b) => a.Value / b;
    public static FloatVariable operator /(float a, FloatVariable b) => a / b.Value;

    // Modulus
    public static FloatVariable operator %(FloatVariable a, FloatVariable b) => a.Value % b.Value;
    public static FloatVariable operator %(FloatVariable a, float b) => a.Value % b;
    public static FloatVariable operator %(float a, FloatVariable b) => a % b.Value;

    // Unary minus
    public static FloatVariable operator -(FloatVariable a) => -a.Value;

    // Comparison operators
    public static bool operator ==(FloatVariable a, FloatVariable b) => a.Value == b.Value;
    public static bool operator !=(FloatVariable a, FloatVariable b) => a.Value != b.Value;
    public static bool operator <(FloatVariable a, FloatVariable b) => a.Value < b.Value;
    public static bool operator >(FloatVariable a, FloatVariable b) => a.Value > b.Value;
    public static bool operator <=(FloatVariable a, FloatVariable b) => a.Value <= b.Value;
    public static bool operator >=(FloatVariable a, FloatVariable b) => a.Value >= b.Value;

    // Override Equals and GetHashCode
    public override bool Equals(object obj)
    {
        return obj is FloatVariable variable &&
               Value == variable.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    // ToString override
    public override string ToString() => Value.ToString();

 }
