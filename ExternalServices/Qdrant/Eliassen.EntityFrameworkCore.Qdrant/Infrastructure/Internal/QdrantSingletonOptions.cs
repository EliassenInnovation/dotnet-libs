﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Eliassen.EntityFrameworkCore.Qdrant.Infrastructure.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class QdrantSingletonOptions : IQdrantSingletonOptions
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual int CompatibilityLevel { get; private set; } = QdrantOptionsExtension.DefaultCompatibilityLevel;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual int? CompatibilityLevelWithoutDefault { get; private set; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual void Initialize(IDbContextOptions options)
    {
        var QdrantOptions = options.FindExtension<QdrantOptionsExtension>();
        if (QdrantOptions != null)
        {
            CompatibilityLevel = QdrantOptions.CompatibilityLevel;
            CompatibilityLevelWithoutDefault = QdrantOptions.CompatibilityLevelWithoutDefault;
        }
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual void Validate(IDbContextOptions options)
    {
        var QdrantOptions = options.FindExtension<QdrantOptionsExtension>();

        if (QdrantOptions != null
            && (CompatibilityLevelWithoutDefault != QdrantOptions.CompatibilityLevelWithoutDefault
                || CompatibilityLevel != QdrantOptions.CompatibilityLevel))
        {
            throw new InvalidOperationException(
                CoreStrings.SingletonOptionChanged(
                    nameof(QdrantDbContextOptionsExtensions.UseQdrant),
                    nameof(DbContextOptionsBuilder.UseInternalServiceProvider)));
        }
    }
}
