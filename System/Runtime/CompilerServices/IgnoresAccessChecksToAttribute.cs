// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute
// Assembly: RealA10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 532C87AC-4BC4-4B3D-ACF1-A050F52D51FF
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\BepInEx\plugins\RealA10.dll

#nullable disable
namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
internal sealed class IgnoresAccessChecksToAttribute : Attribute
{
  internal IgnoresAccessChecksToAttribute(string assemblyName)
  {
  }
}
