# DigitalProduction.Maui.AppToolkit
A collection of classes, utilities, and resources for building .Net Maui applications faster and conistently.

It includes Views (Pages and Popups), ViewModels, Validation, FileTypes, Converations, and Validation components.

## Requirements

Version 0.14.0 and later require .NET 10 and DigitalProduction.Maui 0.11.0 or later. The demo requires DigitalProduction.Maui.Images 1.2.0. Build with the .NET 10 SDK, its default C# 14 language version, and corresponding MAUI platform workloads.

Version 0.13.15 is the final .NET 9 release and is no longer maintained.

## Validation behaviors and trimming

Starting with 0.15.0, `FileExistsValidationBehavior`, `DirectoryExistsValidationBehavior` and
`FileNameValidationBehavior` use `TrimSafeValidationBehavior<T>` instead of the Toolkit's
`ValidationBehavior<T>`. The Toolkit base creates string-path bindings and declares itself
unsafe for trimming; preserving all members does not make those bindings safe.

The behaviors read `Text` directly from the bindable property on `Entry`, `Editor`, `SearchBar`
and `Label`. `Flags` (the Toolkit `ValidationFlags` enum), validity/running state, styles,
`ForceValidate`, `ForceValidateCommand` and the reported `Error` remain available. Unset values
report validation errors. Updating an error no longer triggers another validation pass.

This changes the base type: these behaviors cannot be assigned to Toolkit `ValidationBehavior`
or nested in its `MultiValidationBehavior`. Arbitrary string property paths are unsupported.
For another control/property, override `GetValueProperty` to return its `BindableProperty`, or
set `ValuePropertyName=""` and bind `Value` using a compiled binding. For example, in C#:

```csharp
var behavior = new DirectoryExistsValidationBehavior { ValuePropertyName = "" };
behavior.SetBinding(TrimSafeValidationBehavior<string>.ValueProperty,
    static (Entry source) => source.Text, source: entry);
entry.Behaviors.Add(behavior);
```

For XAML, use `x:DataType` and enable compilation of bindings with an explicit `Source`
(`MauiEnableXamlCBindingWithSourceCompilation`). Behaviors do not inherit the control's binding
context. The new base propagates validation exceptions; Toolkit's global exception-suppression
option no longer applies. The built-in path validators handle null input without throwing.

The Windows [validation smoke app](src/ValidationSmoke/README.md) checks the actual library in
a consumer published with full trimming and Native AOT. This verifies these behaviors, not
Native AOT compatibility of every AppToolkit component or dependency. Shared styles and
`akgul.Maui.DataGrid` still produce separate publish-time warnings, tracked in
[#124](https://github.com/lendres/DigitalProduction.Maui.AppToolkit/issues/124). Apple Native AOT and
Android runtime execution require validation on their respective hosts/devices.

`FileNameValidationBehavior.RequireFileToExist` currently checks the parent directory only;
that existing discrepancy is tracked in [#125](https://github.com/lendres/DigitalProduction.Maui.AppToolkit/issues/125).
