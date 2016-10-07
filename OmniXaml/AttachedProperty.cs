﻿namespace OmniXaml
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class AttachedProperty : Property
    {
        private readonly MethodInfo getter;
        private readonly MethodInfo setter;
        private readonly Type propertyType;

        public AttachedProperty(Type type, string propertyName)
        {
            getter = type.GetRuntimeMethods().First(info => IsGetter(propertyName, info));
            setter = type.GetRuntimeMethods().First(info => IsSetter(propertyName, info));

            EnsureTypesAreSame();

            propertyType = getter.ReturnType;
        }

        private void EnsureTypesAreSame()
        {            
        }

        private static bool IsSetter(string propertyName, MethodInfo info)
        {
            return info.Name == $"Set{propertyName}" && info.GetParameters().Length == 2;
        }

        private static bool IsGetter(string propertyName, MethodInfo info)
        {
            return info.Name == $"Get{propertyName}" && info.GetParameters().Length == 1;
        }

        public override object GetValue(object instance)
        {
            return getter.Invoke(instance, null);
        }

        public override Type PropertyType => propertyType;

        public override void SetValue(object instance, object value)
        {
            setter.Invoke(null, new []{ instance, value, } );
        }
    }
}