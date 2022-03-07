using System;

namespace Volo.Abp.Gdpr;

/// <summary>
/// Used to indicate that a something is considered GDPR (personal) data.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class GdprDataAttribute : Attribute
{
}