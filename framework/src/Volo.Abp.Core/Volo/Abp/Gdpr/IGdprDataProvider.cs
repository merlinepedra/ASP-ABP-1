using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volo.Abp.Gdpr;

public interface IGdprDataProvider
{
    Task<List<GdprFile>> GetFileAsync(Guid userId);
}