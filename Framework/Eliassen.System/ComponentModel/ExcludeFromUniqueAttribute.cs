﻿using System;

namespace Eliassen.System.ComponentModel
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class ExcludeFromUniqueAttribute : Attribute
    {
    }
}
