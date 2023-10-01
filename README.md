# TheNoobs.Specification Library in C#: Handling Domain Validations Efficiently

![GitHub](https://img.shields.io/github/license/thenoobsbr/domain-validator)
![GitHub issues](https://img.shields.io/github/issues/thenoobsbr/domain-validator)
![GitHub pull requests](https://img.shields.io/github/issues-pr/thenoobsbr/domain-validator)
![GitHub Sponsors](https://img.shields.io/github/sponsors/thenoobsbr)
[![nuget](https://buildstats.info/nuget/TheNoobs.Specification)](http://www.nuget.org/packages/TheNoobs.Specification)


The **TheNoobs.Specification** library is a powerful C# implementation of the Specification design pattern, offering advanced features for encapsulating complex business rules in a single class. This pattern is commonly used to determine whether an entity satisfies a specific set of conditions. By consolidating business rules in one place, it enhances code readability and manageability, promotes code reuse, and separates responsibilities.

## Overview

The Specification design pattern provided by **TheNoobs.Specification** enables you to create specifications with customizable validation behavior. You can choose between two validation modes: **CircuitBreaker** and **NonCircuitBreaker**.

- **CircuitBreaker**: In this mode, specification validation stops at the first item that does not meet the specification criteria. Only the first validation issue is reported in the output.
- **NonCircuitBreaker**: In this mode, validation continues for all items in the specification, even if multiple items fail to meet the criteria. This mode ensures a comprehensive validation of the specification.

## Getting Started

### Creating a CircuitBreaker Specification

```csharp
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.Internals;
using TheNoobs.Specification.UnitTests.Stubs.Entities;

namespace TheNoobs.Specification.UnitTests.Stubs.Specifications.Shareholders
{
    public class CircuitBreakerIsShareholderAbleBeSentSpecification : ISpecification<Shareholder>
    {
        private readonly ISpecification<Shareholder> _specification;
        
        public CircuitBreakerIsShareholderAbleBeSentSpecification()
        {
            _specification = SpecificationFactory<Shareholder>
                .CircuitBreaker()
                .Requires("SH001", "Shareholder last name is required.", s => !string.IsNullOrEmpty(s.LastName))
                .And(s => s.Document != null)
                .WithCodeAndDescription("SH002", "Shareholder document cannot be null.")
                .Build();
        }
        
        public bool IsSatisfiedBy(Shareholder entity, out IEnumerable<IIssue> issues)
        {
            return _specification.IsSatisfiedBy(entity, out issues);
        }
    }
}
```

### Creating a NonCircuitBreaker Specification

```csharp
using TheNoobs.Specification.Abstractions;
using TheNoobs.Specification.Internals;
using TheNoobs.Specification.UnitTests.Stubs.Entities;

namespace TheNoobs.Specification.UnitTests.Stubs.Specifications.Shareholders
{
    public class NonCircuitBreakerIsShareholderAbleBeSentSpecification : ISpecification<Shareholder>
    {
        private readonly ISpecification<Shareholder> _specification;
        
        public NonCircuitBreakerIsShareholderAbleBeSentSpecification()
        {
            _specification = SpecificationFactory<Shareholder>
                .NonCircuitBreaker()
                .Requires("SH001", "Shareholder last name is required.", s => !string.IsNullOrEmpty(s.LastName))
                .And(s => s.Document != null)
                .WithCodeAndDescription("SH002", "Shareholder document cannot be null.")
                .Build();
        }
        
        public bool IsSatisfiedBy(Shareholder entity, out IEnumerable<IIssue> issues)
        {
            return _specification.IsSatisfiedBy(entity, out issues);
        }
    }
}
```

## Usage

Here's how you can use **TheNoobs.Specification** library in your unit tests:

```csharp
[Trait("Category", "UnitTest")]
[Trait("Class", nameof(SpecificationBuilder<Shareholder>))]
public class SpecificationTest
{
    [Fact]
    public void Given_AnInvalidObjectAndACircuitBreakerSpecification_When_ITryToCheckIfIsSatisfied_Then_TheSpecificationShouldNotBeSatisfiedAndOnlyTheFirstIssuesShouldBeReported()
    {
        var shareholder = new Shareholder("Bernardo", new DateTime(1984, 03, 26));
        var specification = new CircuitBreakerIsShareholderAbleBeSentSpecification();

        specification.IsSatisfiedBy(shareholder, out var issues).Should().BeFalse();
        issues.Should().HaveCount(1);
    }

    [Fact]
    public void Given_AnInvalidObjectAndANonCircuitBreakerSpecification_When_ITryToCheckIfIsSatisfied_Then_TheSpecificationShouldNotBeSatisfiedAndAllIssuesShouldBeReported()
    {
        var shareholder = new Shareholder("Bernardo", new DateTime(1984, 03, 26));
        var specification = new NonCircuitBreakerIsShareholderAbleBeSentSpecification();

        specification.IsSatisfiedBy(shareholder, out var issues).Should().BeFalse();
        issues.Should().HaveCountGreaterThan(1);
    }
}
```

## Conclusion

**TheNoobs.Specification** library simplifies complex business rule validation by encapsulating rules in a unified manner. Whether you prefer the CircuitBreaker or NonCircuitBreaker behavior, this library provides the tools you need to enhance the quality and maintainability of your C# code.

---
> ♥ Made with love!
