#if PEX_NOT_AVAILABLE
namespace Microsoft.Pex.Framework
{


    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PexSequenceTestAttribute : System.Attribute
    {

        public bool IgnoreFieldAccesses;

        public bool IgnoreStates;

        public bool IgnoreWeight;

        public bool MatchUncoveredBranches;

        public int MaxSuggestions;

        public string Categories;

        public int MaxBranches;

        public int MaxRounds;

        public int MaxRuns;

        public int MaxRunsWithUniqueBranchHits;

        public int MaxRunsWithUniquePaths;

        public int MaxCalls;

        public int MaxConditions;

        public int MaxBranchHits;

        public int MaxExceptions;

        public int ConstraintSolverTimeout;

        public int Timeout;

        public string CoverageFileFormat;

        public string TouchMyCode;

        public string TestClassName;

        public bool TestExcludeNonTermination;

        public bool TestDisableNonTermination;

        public Microsoft.Pex.Framework.PexTestEmissionFilter TestEmissionFilter;

        public bool PruneExceptions;

        public bool Joins;

        public bool NoSoftSubstitutions;

        public bool SymbolicCalls;

        public Microsoft.Pex.Framework.Strategies.PexSearchFrontier SearchFrontier;

        public string CustomSolver;

        public Microsoft.Pex.Framework.Strategies.PexCoverageGoal CoverageGoal;

        public string CssProjectStructure;

        public string CssIteration;

        public bool ContainsSettings;

        public System.Collections.Generic.IEnumerable<string> ActiveSettings;

        public object TypeId;

        static PexSequenceTestAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatableByMethodAndSettersAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static PexCreatableByMethodAndSettersAttribute()
        {
        }

        static PexCreatableByMethodAndSettersAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatableByConstructorAndSettersAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static PexCreatableByConstructorAndSettersAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatableAsWebDataAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static PexCreatableAsWebDataAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PexUseGenericArgumentsAttribute : System.Attribute
    {

        public object TypeId;

        static PexUseGenericArgumentsAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PexTestAttribute : System.Attribute
    {

        public string Categories;

        public int MaxBranches;

        public int MaxRounds;

        public int MaxRuns;

        public int MaxRunsWithUniqueBranchHits;

        public int MaxRunsWithUniquePaths;

        public int MaxCalls;

        public int MaxConditions;

        public int MaxBranchHits;

        public int MaxExceptions;

        public int ConstraintSolverTimeout;

        public int Timeout;

        public string CoverageFileFormat;

        public string TouchMyCode;

        public string TestClassName;

        public bool TestExcludeNonTermination;

        public bool TestDisableNonTermination;

        public Microsoft.Pex.Framework.PexTestEmissionFilter TestEmissionFilter;

        public bool PruneExceptions;

        public bool Joins;

        public bool NoSoftSubstitutions;

        public bool SymbolicCalls;

        public Microsoft.Pex.Framework.Strategies.PexSearchFrontier SearchFrontier;

        public string CustomSolver;

        public Microsoft.Pex.Framework.Strategies.PexCoverageGoal CoverageGoal;

        public string CssProjectStructure;

        public string CssIteration;

        public bool ContainsSettings;

        public System.Collections.Generic.IEnumerable<string> ActiveSettings;

        public object TypeId;

        static PexTestAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexWorkItemAttribute : System.Attribute
    {

        public int Id;

        public object TypeId;

        static PexWorkItemAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute((System.AttributeTargets.Class | System.AttributeTargets.Method), AllowMultiple = false, Inherited = true)]
    public class PexIgnoreAttribute : System.Attribute
    {

        public string Message;

        public int WorkItemId;

        public object TypeId;

        static PexIgnoreAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PexFactoryAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx FactoredType;

        public object TypeId;

        static PexFactoryAttribute()
        {
        }

        static PexFactoryAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexExpectedCoverageAttribute : System.Attribute
    {

        public double ExpectedCoverage;

        public Microsoft.Pex.Framework.PexExpectedCoverageAttribute.CoverageOperator Operator;

        public Microsoft.Pex.Framework.PexCoverageUnit Unit;

        public bool DebugOnly;

        public bool ReleaseOnly;

        public object TypeId;

        static PexExpectedCoverageAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFocusOnAssemblyAttribute : System.Attribute
    {

        public Microsoft.Pex.Framework.PexSearchPriority SearchPriority;

        public bool DoNotReportCoverage;

        public object TypeId;

        static PexFocusOnAssemblyAttribute()
        {
        }

        static PexFocusOnAssemblyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = false, Inherited = true)]
    public class PexCrossProductExplorableStrategyAttribute : System.Attribute
    {

        public object TypeId;

        static PexCrossProductExplorableStrategyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute((((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Parameter), AllowMultiple = true, Inherited = true)]
    public class PexUseValuesAttribute : System.Attribute
    {

        public object TypeId;

        static PexUseValuesAttribute()
        {
        }

        static PexUseValuesAttribute()
        {
        }

        static PexUseValuesAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatableByMethodAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static PexCreatableByMethodAttribute()
        {
        }

        static PexCreatableByMethodAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = true, Inherited = true)]
    public class PexAssumeIsNotSharedAttribute : System.Attribute
    {

        public object TypeId;

        static PexAssumeIsNotSharedAttribute()
        {
        }

        static PexAssumeIsNotSharedAttribute()
        {
        }

        static PexAssumeIsNotSharedAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFocusOnNamespaceAttribute : System.Attribute
    {

        public string NamespacePrefix;

        public bool Strict;

        public Microsoft.Pex.Framework.PexSearchPriority SearchPriority;

        public bool DoNotReportCoverage;

        public object TypeId;

        static PexFocusOnNamespaceAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInvariantAssemblyAttribute : System.Attribute
    {

        public System.Reflection.AssemblyName TargetAssembly;

        public Microsoft.ExtendedReflection.Metadata.TypeEx InvariantClassAttributeType;

        public Microsoft.ExtendedReflection.Metadata.TypeEx InvariantMethodAttributeType;

        public Microsoft.ExtendedReflection.Metadata.TypeEx ValidationType;

        public object TypeId;

        static PexInvariantAssemblyAttribute()
        {
        }

        static PexInvariantAssemblyAttribute()
        {
        }

        static PexInvariantAssemblyAttribute()
        {
        }

        static PexInvariantAssemblyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PexFieldBindingAttribute : System.Attribute
    {

        public string FieldName;

        public object TypeId;

        static PexFieldBindingAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatableByClassFactoryAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx FactoryType;

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static PexCreatableByClassFactoryAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexAllowedExceptionAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeDefinition AllowedExceptionTypeDefinition;

        public string Description;

        public string UserAssemblies;

        public bool AcceptExceptionSubtypes;

        public bool AcceptInnerException;

        public object TypeId;

        static PexAllowedExceptionAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PexClassAttribute : System.Attribute
    {

        public string Suite;

        public string Categories;

        public int MaxBranches;

        public int MaxRounds;

        public int MaxRuns;

        public int MaxRunsWithUniqueBranchHits;

        public int MaxRunsWithUniquePaths;

        public int MaxCalls;

        public int MaxConditions;

        public int MaxBranchHits;

        public int MaxExceptions;

        public int ConstraintSolverTimeout;

        public int Timeout;

        public string CoverageFileFormat;

        public string TouchMyCode;

        public string TestClassName;

        public bool TestExcludeNonTermination;

        public bool TestDisableNonTermination;

        public Microsoft.Pex.Framework.PexTestEmissionFilter TestEmissionFilter;

        public bool PruneExceptions;

        public bool Joins;

        public bool NoSoftSubstitutions;

        public bool SymbolicCalls;

        public Microsoft.Pex.Framework.Strategies.PexSearchFrontier SearchFrontier;

        public string CustomSolver;

        public Microsoft.Pex.Framework.Strategies.PexCoverageGoal CoverageGoal;

        public string CssProjectStructure;

        public string CssIteration;

        public bool ContainsSettings;

        public System.Collections.Generic.IEnumerable<string> ActiveSettings;

        public object TypeId;

        static PexClassAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatableAsSingletonAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static PexCreatableAsSingletonAttribute()
        {
        }

        static PexCreatableAsSingletonAttribute()
        {
        }

        static PexCreatableAsSingletonAttribute()
        {
        }

        static PexCreatableAsSingletonAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = true, Inherited = true)]
    public class PexAssumeIsAcyclicAttribute : System.Attribute
    {

        public object TypeId;

        static PexAssumeIsAcyclicAttribute()
        {
        }

        static PexAssumeIsAcyclicAttribute()
        {
        }

        static PexAssumeIsAcyclicAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexExplorableFromConstructorAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExplorableType;

        public Microsoft.ExtendedReflection.Metadata.Method ExplorationMethod;

        public object TypeId;

        static PexExplorableFromConstructorAttribute()
        {
        }

        static PexExplorableFromConstructorAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexExplorableFromMethodAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExplorableType;

        public Microsoft.ExtendedReflection.Metadata.Method ExplorationMethod;

        public object TypeId;

        static PexExplorableFromMethodAttribute()
        {
        }

        static PexExplorableFromMethodAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
    public class PexInvariantControllerAttribute : System.Attribute
    {

        public System.Type ControllerType;

        public object TypeId;

        static PexInvariantControllerAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PexNotCompilableAttribute : System.Attribute
    {

        public object TypeId;

        static PexNotCompilableAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PexInjectedExceptionAttribute : System.Attribute
    {

        public System.Type ExceptionType;

        public string Reason;

        public object TypeId;

        static PexInjectedExceptionAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PexUnexpectedExceptionAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExceptionType;

        public object TypeId;

        static PexUnexpectedExceptionAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCoverageFilterAssemblyAttribute : System.Attribute
    {

        public System.Reflection.AssemblyName TargetAssemblyName;

        public Microsoft.Pex.Framework.PexCoverageDomain CoverageDomain;

        public object TypeId;

        static PexCoverageFilterAssemblyAttribute()
        {
        }

        static PexCoverageFilterAssemblyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCoverageFilterNamespaceAttribute : System.Attribute
    {

        public string NamespaceSuffix;

        public Microsoft.Pex.Framework.PexCoverageDomain CoverageDomain;

        public object TypeId;

        static PexCoverageFilterNamespaceAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCoverageFilterTypeAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeDefinition TargetType;

        public Microsoft.Pex.Framework.PexCoverageDomain CoverageDomain;

        public object TypeId;

        static PexCoverageFilterTypeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCoverageFilterTypesAttribute : System.Attribute
    {

        public string NameSuffix;

        public Microsoft.Pex.Framework.PexCoverageDomain CoverageDomain;

        public object TypeId;

        static PexCoverageFilterTypesAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCoverageFilterMarkedByAttribute : System.Attribute
    {

        public System.Type AttributeType;

        public Microsoft.Pex.Framework.PexCoverageDomain CoverageDomain;

        public object TypeId;

        static PexCoverageFilterMarkedByAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCoverageFilterMethodAttribute : System.Attribute
    {

        public Microsoft.Pex.Framework.PexCoverageDomain CoverageDomain;

        public object TypeId;

        static PexCoverageFilterMethodAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = true, Inherited = true)]
    public class PexAssumeEnumRangeAttribute : System.Attribute
    {

        public object TypeId;

        static PexAssumeEnumRangeAttribute()
        {
        }

        static PexAssumeEnumRangeAttribute()
        {
        }

        static PexAssumeEnumRangeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute((System.AttributeTargets.Class | System.AttributeTargets.Method), AllowMultiple = false, Inherited = true)]
    public class PexExplicitAttribute : System.Attribute
    {

        public object TypeId;

        static PexExplicitAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PexInvariantAttribute : System.Attribute
    {

        public object TypeId;

        static PexInvariantAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class DecimalExplorableAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExplorableType;

        public Microsoft.ExtendedReflection.Metadata.Method ExplorationMethod;

        public object TypeId;

        static DecimalExplorableAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class DateTimeCreatableAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static DateTimeCreatableAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class ArrayListExplorableAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExplorableType;

        public Microsoft.ExtendedReflection.Metadata.Method ExplorationMethod;

        public object TypeId;

        static ArrayListExplorableAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class QueueExplorableAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExplorableType;

        public Microsoft.ExtendedReflection.Metadata.Method ExplorationMethod;

        public object TypeId;

        static QueueExplorableAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class StackExplorableAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExplorableType;

        public Microsoft.ExtendedReflection.Metadata.Method ExplorationMethod;

        public object TypeId;

        static StackExplorableAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
    public class PexAssemblySettingsAttribute : System.Attribute
    {

        public string TestFramework;

        public string TestLanguage;

        public bool TestNoPartialClasses;

        public bool TestNoClassAttribute;

        public bool TestOverrideReadonly;

        public bool TestGenerateDuplicates;

        public string TestRootNamespace;

        public bool TestForceFixtureSetupTeardown;

        public string TestCopyright;

        public int RequiredCoveragePercentile;

        public bool NoSeparateAppDomain;

        public bool UseNoImplicitMocks;

        public string Categories;

        public int MaxBranches;

        public int MaxRounds;

        public int MaxRuns;

        public int MaxRunsWithUniqueBranchHits;

        public int MaxRunsWithUniquePaths;

        public int MaxCalls;

        public int MaxConditions;

        public int MaxBranchHits;

        public int MaxExceptions;

        public int ConstraintSolverTimeout;

        public int Timeout;

        public string CoverageFileFormat;

        public string TouchMyCode;

        public string TestClassName;

        public bool TestExcludeNonTermination;

        public bool TestDisableNonTermination;

        public Microsoft.Pex.Framework.PexTestEmissionFilter TestEmissionFilter;

        public bool PruneExceptions;

        public bool Joins;

        public bool NoSoftSubstitutions;

        public bool SymbolicCalls;

        public Microsoft.Pex.Framework.Strategies.PexSearchFrontier SearchFrontier;

        public string CustomSolver;

        public Microsoft.Pex.Framework.Strategies.PexCoverageGoal CoverageGoal;

        public string CssProjectStructure;

        public string CssIteration;

        public bool ContainsSettings;

        public System.Collections.Generic.IEnumerable<string> ActiveSettings;

        public object TypeId;

        static PexAssemblySettingsAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexSettingsMixAttribute : System.Attribute
    {

        public string MixName;

        public int MixReportOrder;

        public string Categories;

        public int MaxBranches;

        public int MaxRounds;

        public int MaxRuns;

        public int MaxRunsWithUniqueBranchHits;

        public int MaxRunsWithUniquePaths;

        public int MaxCalls;

        public int MaxConditions;

        public int MaxBranchHits;

        public int MaxExceptions;

        public int ConstraintSolverTimeout;

        public int Timeout;

        public string CoverageFileFormat;

        public string TouchMyCode;

        public string TestClassName;

        public bool TestExcludeNonTermination;

        public bool TestDisableNonTermination;

        public Microsoft.Pex.Framework.PexTestEmissionFilter TestEmissionFilter;

        public bool PruneExceptions;

        public bool Joins;

        public bool NoSoftSubstitutions;

        public bool SymbolicCalls;

        public Microsoft.Pex.Framework.Strategies.PexSearchFrontier SearchFrontier;

        public string CustomSolver;

        public Microsoft.Pex.Framework.Strategies.PexCoverageGoal CoverageGoal;

        public string CssProjectStructure;

        public string CssIteration;

        public bool ContainsSettings;

        public System.Collections.Generic.IEnumerable<string> ActiveSettings;

        public object TypeId;

        static PexSettingsMixAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseTypeAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx TargetType;

        public object TypeId;

        static PexUseTypeAttribute()
        {
        }

        static PexUseTypeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseTypesFromFactoryAttribute : System.Attribute
    {

        public object TypeId;

        static PexUseTypesFromFactoryAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseAssemblyAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.AssemblyEx TargetAssembly;

        public object TypeId;

        static PexUseAssemblyAttribute()
        {
        }

        static PexUseAssemblyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseImplementationsOfAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx TargetType;

        public Microsoft.ExtendedReflection.Metadata.AssemblyEx TargetAssembly;

        public object TypeId;

        static PexUseImplementationsOfAttribute()
        {
        }

        static PexUseImplementationsOfAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseNamespaceAttribute : System.Attribute
    {

        public string NamespaceName;

        public bool Strict;

        public Microsoft.ExtendedReflection.Metadata.AssemblyEx TargetAssembly;

        public object TypeId;

        static PexUseNamespaceAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseMarkedByAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx AttributeType;

        public Microsoft.ExtendedReflection.Metadata.AssemblyEx TargetAssembly;

        public object TypeId;

        static PexUseMarkedByAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatableByConstructorAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx CreatableType;

        public object TypeId;

        static PexCreatableByConstructorAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute((((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Parameter), AllowMultiple = true, Inherited = true)]
    public class PexExplorableFromFactoryAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx FactoryType;

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExplorableType;

        public Microsoft.ExtendedReflection.Metadata.Method ExplorationMethod;

        public object TypeId;

        static PexExplorableFromFactoryAttribute()
        {
        }

        static PexExplorableFromFactoryAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexCreatablesAsWebDataFromXmlNamespaceAttribute : System.Attribute
    {

        public string XmlNamespace;

        public object TypeId;

        static PexCreatablesAsWebDataFromXmlNamespaceAttribute()
        {
        }

        static PexCreatablesAsWebDataFromXmlNamespaceAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFromAssemblyAttribute : System.Attribute
    {

        public System.Reflection.AssemblyName TargetAssembly;

        public bool DeriveSubtypes;

        public Microsoft.Pex.Framework.PexFromAction Action;

        public object TypeId;

        static PexFromAssemblyAttribute()
        {
        }

        static PexFromAssemblyAttribute()
        {
        }

        static PexFromAssemblyAttribute()
        {
        }

        static PexFromAssemblyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFromNamespaceAttribute : System.Attribute
    {

        public string Namespace;

        public System.Reflection.AssemblyName TargetAssembly;

        public bool DeriveSubtypes;

        public Microsoft.Pex.Framework.PexFromAction Action;

        public object TypeId;

        static PexFromNamespaceAttribute()
        {
        }

        static PexFromNamespaceAttribute()
        {
        }

        static PexFromNamespaceAttribute()
        {
        }

        static PexFromNamespaceAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFromTypeAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeDefinition Type;

        public bool DeriveSubtypes;

        public Microsoft.Pex.Framework.PexFromAction Action;

        public object TypeId;

        static PexFromTypeAttribute()
        {
        }

        static PexFromTypeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFromMarkedByAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx AttributeType;

        public System.Reflection.AssemblyName TargetAssembly;

        public bool DeriveSubtypes;

        public Microsoft.Pex.Framework.PexFromAction Action;

        public object TypeId;

        static PexFromMarkedByAttribute()
        {
        }

        static PexFromMarkedByAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFromFieldAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.Field Field;

        public Microsoft.Pex.Framework.PexFromAction Action;

        public object TypeId;

        static PexFromFieldAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFromConstructorAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.Method Constructor;

        public Microsoft.Pex.Framework.PexFromAction Action;

        public object TypeId;

        static PexFromConstructorAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFromMethodAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.Method Method;

        public Microsoft.Pex.Framework.PexFromAction Action;

        public object TypeId;

        static PexFromMethodAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PexAttributeFamilyAttribute : System.Attribute
    {

        public Microsoft.Pex.Framework.PexAttributeFamily Family;

        public object TypeId;

        static PexAttributeFamilyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexInvariantCheckAttribute : System.Attribute
    {

        public object TypeId;

        static PexInvariantCheckAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PexKeepMeAttribute : System.Attribute
    {

        public object TypeId;

        static PexKeepMeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexInjectExceptionsOnWriteAttribute : System.Attribute
    {

        public object TypeId;

        static PexInjectExceptionsOnWriteAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexFixItEmitterFactoryAttribute : System.Attribute
    {

        public System.Type FixItEmitterFactoryType;

        public object TypeId;

        static PexFixItEmitterFactoryAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = true, Inherited = true)]
    public class PexUseRangeAttribute : System.Attribute
    {

        public object TypeId;

        static PexUseRangeAttribute()
        {
        }

        static PexUseRangeAttribute()
        {
        }

        static PexUseRangeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PexInstrumentedAttribute : System.Attribute
    {

        public object TypeId;

        static PexInstrumentedAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PexMockAttribute : System.Attribute
    {

        public object TypeId;

        static PexMockAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInstrumentMarkedByAttribute : System.Attribute
    {

        public System.Reflection.Assembly TargetAssembly;

        public System.Type AttributeType;

        public Microsoft.Pex.Framework.PexInstrumentationLevel InstrumentationLevel;

        public bool NoNestedTypes;

        public object TypeId;

        static PexInstrumentMarkedByAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInstrumentMocksAttribute : System.Attribute
    {

        public System.Reflection.Assembly TargetAssembly;

        public System.Type AttributeType;

        public Microsoft.Pex.Framework.PexInstrumentationLevel InstrumentationLevel;

        public bool NoNestedTypes;

        public object TypeId;

        static PexInstrumentMocksAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseMocksAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx AttributeType;

        public Microsoft.ExtendedReflection.Metadata.AssemblyEx TargetAssembly;

        public object TypeId;

        static PexUseMocksAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInstrumentAssemblyAttribute : System.Attribute
    {

        public string TargetAssemblyName;

        public Microsoft.Pex.Framework.PexInstrumentationLevel InstrumentationLevel;

        public object TypeId;

        static PexInstrumentAssemblyAttribute()
        {
        }

        static PexInstrumentAssemblyAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInstrumentAssembliesFromAttribute : System.Attribute
    {

        public string Path;

        public string SearchPattern;

        public Microsoft.Pex.Framework.PexInstrumentationLevel InstrumentationLevel;

        public object TypeId;

        static PexInstrumentAssembliesFromAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = true, Inherited = true)]
    public class PexUseUnicodeStringsAttribute : System.Attribute
    {

        public object TypeId;

        static PexUseUnicodeStringsAttribute()
        {
        }

        static PexUseUnicodeStringsAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexRollbackAttribute : System.Attribute
    {

        public object TypeId;

        static PexRollbackAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexExpectedExceptionAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx ExpectedExceptionType;

        public string Description;

        public string UserAssemblies;

        public bool AcceptExceptionSubtypes;

        public bool AcceptInnerException;

        public object TypeId;

        static PexExpectedExceptionAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = true, Inherited = true)]
    public class PexAssumeIsNotNullAttribute : System.Attribute
    {

        public object TypeId;

        static PexAssumeIsNotNullAttribute()
        {
        }

        static PexAssumeIsNotNullAttribute()
        {
        }

        static PexAssumeIsNotNullAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInstrumentTypesByNameAttribute : System.Attribute
    {

        public Microsoft.Pex.Framework.PexInstrumentationLevel InstrumentationLevel;

        public bool NoNestedTypes;

        public object TypeId;

        static PexInstrumentTypesByNameAttribute()
        {
        }

        static PexInstrumentTypesByNameAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInstrumentTypeAttribute : System.Attribute
    {

        public System.Type TargetType;

        public Microsoft.Pex.Framework.PexInstrumentationLevel InstrumentationLevel;

        public bool NoNestedTypes;

        public object TypeId;

        static PexInstrumentTypeAttribute()
        {
        }

        static PexInstrumentTypeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexInstrumentNamespaceAttribute : System.Attribute
    {

        public System.Reflection.Assembly TargetAssembly;

        public string Namespace;

        public bool IgnoreSubNamespaces;

        public Microsoft.Pex.Framework.PexInstrumentationLevel InstrumentationLevel;

        public bool NoNestedTypes;

        public object TypeId;

        static PexInstrumentNamespaceAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class PexTestFrameworkAttribute : System.Attribute
    {

        public System.Type TestFrameworkType;

        public object TypeId;

        static PexTestFrameworkAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = false, Inherited = true)]
    public class PexGeneratedByAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx FixtureType;

        public string ExplorationName;

        public object TypeId;

        static PexGeneratedByAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute((((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Parameter), AllowMultiple = true, Inherited = true)]
    public class PexUseValuesFromFactoryAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeEx FactoryType;

        public object TypeId;

        static PexUseValuesFromFactoryAttribute()
        {
        }

        static PexUseValuesFromFactoryAttribute()
        {
        }

        static PexUseValuesFromFactoryAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexExpectedTestsAttribute : System.Attribute
    {

        public int TotalCount;

        public int DuplicateCount;

        public int FailureCount;

        public int NewCount;

        public object TypeId;

        static PexExpectedTestsAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((((((((((((((System.AttributeTargets.Assembly | System.AttributeTargets.Module)
                | System.AttributeTargets.Class)
                | System.AttributeTargets.Struct)
                | System.AttributeTargets.Enum)
                | System.AttributeTargets.Constructor)
                | System.AttributeTargets.Method)
                | System.AttributeTargets.Property)
                | System.AttributeTargets.Field)
                | System.AttributeTargets.Event)
                | System.AttributeTargets.Interface)
                | System.AttributeTargets.Parameter)
                | System.AttributeTargets.Delegate)
                | System.AttributeTargets.ReturnValue)
                | System.AttributeTargets.GenericParameter), AllowMultiple = true, Inherited = true)]
    public class PexAssumeRangeAttribute : System.Attribute
    {

        public object TypeId;

        static PexAssumeRangeAttribute()
        {
        }

        static PexAssumeRangeAttribute()
        {
        }

        static PexAssumeRangeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexInjectValuesAttribute : System.Attribute
    {

        public object TypeId;

        static PexInjectValuesAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexInjectExceptionsOnXmlDocumentedCallAttribute : System.Attribute
    {

        public bool IgnoreUncreatableExceptionTypes;

        public bool AllowExceptionSubtypes;

        public object TypeId;

        static PexInjectExceptionsOnXmlDocumentedCallAttribute()
        {
        }

        static PexInjectExceptionsOnXmlDocumentedCallAttribute()
        {
        }

        static PexInjectExceptionsOnXmlDocumentedCallAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PexInvariantClassAttribute : System.Attribute
    {

        public object TypeId;

        static PexInvariantClassAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PexNotReproducibleAttribute : System.Attribute
    {

        public object TypeId;

        static PexNotReproducibleAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PexInjectedValueAttribute : System.Attribute
    {

        public string Value;

        public string Reason;

        public object TypeId;

        static PexInjectedValueAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexInjectExceptionsOnUnverifiableUnsafeMemoryAccessAttribute : System.Attribute
    {

        public System.Type ExceptionType;

        public object TypeId;

        static PexInjectExceptionsOnUnverifiableUnsafeMemoryAccessAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexFocusOnTypeAttribute : System.Attribute
    {

        public Microsoft.ExtendedReflection.Metadata.TypeDefinition TargetTypeDefinition;

        public bool NoNestedTypes;

        public Microsoft.Pex.Framework.PexSearchPriority SearchPriority;

        public bool DoNotReportCoverage;

        public object TypeId;

        static PexFocusOnTypeAttribute()
        {
        }

        static PexFocusOnTypeAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexExplorablesFromMocksAttribute : System.Attribute
    {

        public object TypeId;

        static PexExplorablesFromMocksAttribute()
        {
        }

        static PexExplorablesFromMocksAttribute()
        {
        }
    }

    [System.AttributeUsageAttribute(((System.AttributeTargets.Assembly | System.AttributeTargets.Class)
                | System.AttributeTargets.Method), AllowMultiple = true, Inherited = true)]
    public class PexUseMaxInstancesAttribute : System.Attribute
    {

        public object TypeId;

        static PexUseMaxInstancesAttribute()
        {
        }

        static PexUseMaxInstancesAttribute()
        {
        }
    }

    public sealed class AssertionViolationException : System.Exception
    {

        public AssertionViolationException(string message)
            :
                base(message)
        {
        }
    }
}
#endif