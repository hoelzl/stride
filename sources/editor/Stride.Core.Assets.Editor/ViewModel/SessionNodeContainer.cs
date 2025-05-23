// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using Stride.Core.Assets.Editor.Services;
using Stride.Core.Assets.Quantum;
using Stride.Core.Extensions;

namespace Stride.Core.Assets.Editor.ViewModel
{
    public class SessionNodeContainer : AssetNodeContainer
    {
        private readonly Type[] additionalPrimitiveTypes;
        
        public SessionNodeContainer(SessionViewModel session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));

            // Apply primitive types, commands and associated data providers that comes from plugins
            var pluginService = session.ServiceProvider.Get<IAssetsPluginService>();
            additionalPrimitiveTypes = pluginService.GetPrimitiveTypes(session).ToArray();
        }

        public override bool IsPrimitiveType(Type type)
        {
            return base.IsPrimitiveType(type) || additionalPrimitiveTypes.Any(x => x.IsAssignableFrom(type));
        }
    }
}
