using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SimTECH.UnitTests.Extensions;

namespace SimTECH.UnitTests.Components;

public abstract class BunitTest
{
    protected Bunit.TestContext Context { get; private set; }

    [SetUp]
    public virtual void Setup()
    {
        Context = new();
        Context.AddTestServices();
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            Context.Dispose();
        }
        catch (Exception)
        {
            // Doubt we need to do something with this
        }
    }
}
