using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Gdpr;

public class DefaultGdprDataProvider : IGdprDataProvider, ITransientDependency
{
    public Task<List<GdprFile>> GetFileAsync(Guid userId)
    {
        return Task.FromResult(new List<GdprFile>());
    }
}