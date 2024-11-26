using System.Reflection;
using Domain.Shows;
using NetArchTest.Rules;

namespace UnitTests.SolutionInfrastructure;

/// <summary>
/// Tests for dependency graphs
/// </summary>
public sealed class DependenciesGraphUnitTests
{
    /// <summary>
    /// Verifies that the domain project does not reference any of the other projects
    /// </summary>
    [Fact]
    public void Application_Domain_Should_Have_No_Dependencies()
    {
        // Arrange
        Assembly domainAssembly = typeof(Show).Assembly;
        
        // Act
        TestResult? results = Types.InAssembly(domainAssembly)
                                   .ShouldNot()
                                   .HaveDependencyOnAny(["Infrastructure", "MigrationService", "Shows.Api", "Shows.Apphost", "Shows.ServiceDefaults", "UnitTests"])
                                   .GetResult();
            
        // Assert
        Assert.True(results.IsSuccessful);
    }
}