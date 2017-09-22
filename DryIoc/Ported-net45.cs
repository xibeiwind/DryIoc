﻿/*
The MIT License (MIT)

Copyright (c) 2013-2017 Maksim Volkau

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

namespace DryIoc
{
    using System.Threading;

    /// <summary>Poly-fill</summary>
    public static partial class Portable
    {
        // ReSharper disable once RedundantAssignment
        static partial void GetCurrentManagedThreadID(ref int threadID)
        {
            threadID = Thread.CurrentThread.ManagedThreadId;
        }
    }
}

namespace System.Reflection
{
    using Collections.Generic;
    using Linq;

    /// <summary>Provides <see cref="GetTypeInfo"/> for the type.</summary>
    public static class TypeInfoTools
    {
        /// <summary>Wraps input type into <see cref="TypeInfo"/> structure.</summary>
        /// <param name="type">Input type.</param> <returns>Type info wrapper.</returns>
        public static TypeInfo GetTypeInfo(this Type type) => new TypeInfo(type);
    }

    /// <summary>Partial analog of TypeInfo existing in .NET 4.5 and higher.</summary>
    public struct TypeInfo
    {
        private readonly Type _type;

        /// <summary>Creates type info by wrapping input type.</summary> <param name="type">Type to wrap.</param>
        public TypeInfo(Type type)
        {
            _type = type;
        }

#pragma warning disable 1591 // "Missing XML-comment"
        public Type AsType() => _type;

        public Assembly Assembly => _type.Assembly;

        public IEnumerable<ConstructorInfo> DeclaredConstructors =>
            _type.GetConstructors(ALL_DECLARED ^ BindingFlags.Static);

        public IEnumerable<MemberInfo> DeclaredMembers =>
            _type.GetMembers(ALL_DECLARED);

        public IEnumerable<MethodInfo> DeclaredMethods =>
            _type.GetMethods(ALL_DECLARED);

        public IEnumerable<FieldInfo> DeclaredFields =>
            _type.GetFields(ALL_DECLARED);

        public IEnumerable<PropertyInfo> DeclaredProperties =>
            _type.GetProperties(ALL_DECLARED);

        public IEnumerable<Type> ImplementedInterfaces => 
            _type.GetInterfaces();

        public IEnumerable<Attribute> GetCustomAttributes(Type attributeType, bool inherit) =>
            _type.GetCustomAttributes(attributeType, inherit).Cast<Attribute>();

        public Type BaseType => _type.BaseType;
        public bool IsGenericType => _type.IsGenericType;
        public bool IsGenericTypeDefinition => _type.IsGenericTypeDefinition;
        public bool ContainsGenericParameters => _type.ContainsGenericParameters;
        public Type[] GenericTypeParameters => _type.GetGenericArguments();
        public Type[] GenericTypeArguments => _type.GetGenericArguments();

        public bool IsClass => _type.IsClass;
        public bool IsInterface => _type.IsInterface;
        public bool IsValueType => _type.IsValueType;
        public bool IsPrimitive => _type.IsPrimitive;
        public bool IsArray =>  _type.IsArray;
        public bool IsPublic => _type.IsPublic;
        public bool IsNestedPublic => _type.IsNestedPublic; 
        public Type DeclaringType => _type.DeclaringType;
        public bool IsAbstract => _type.IsAbstract;
        public bool IsSealed => _type.IsSealed; 
        public bool IsEnum => _type.IsEnum;

        public Type[] GetGenericParameterConstraints() => _type.GetGenericParameterConstraints();
        public Type GetElementType() => _type.GetElementType();

        public bool IsAssignableFrom(TypeInfo typeInfo) => _type.IsAssignableFrom(typeInfo.AsType());
#pragma warning restore 1591 // "Missing XML-comment"

        private const BindingFlags ALL_DECLARED =
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.DeclaredOnly;
    }
}
