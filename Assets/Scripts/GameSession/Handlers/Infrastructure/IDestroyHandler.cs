﻿using Cysharp.Threading.Tasks;
using Elements.GameSession.Data;
using System.Collections.Generic;
using System.Threading;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface IDestroyHandler
    {
        UniTask<IEnumerable<PositionData>> TryDestroyItems(CancellationToken token);
    }
}