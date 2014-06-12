﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.MonsterModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;

namespace Microsoft.Data.Entity.InMemory.FunctionalTests
{
    public class MonsterFixupTest : MonsterFixupTestBase
    {
        protected override IServiceProvider CreateServiceProvider()
        {
            return new ServiceCollection().AddEntityFramework().AddInMemoryStore().ServiceCollection.BuildServiceProvider();
        }

        protected override DbContextOptions CreateOptions(string databaseName)
        {
            return new DbContextOptions().UseInMemoryStore();
        }

        protected override Task CreateAndSeedDatabase(string databaseName, Func<MonsterContext> createContext)
        {
            using (var context = createContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.SeedUsingFKs();
            }

            return Task.FromResult(0);
        }
    }
}
