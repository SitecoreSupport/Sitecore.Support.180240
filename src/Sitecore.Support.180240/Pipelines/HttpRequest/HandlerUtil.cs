namespace Sitecore.Support.Pipelines.HttpRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines.HttpRequest;
    using Sitecore.Web;

    public class HandlerUtil
    {
        [CanBeNull]
        public static string GetHandler([NotNull] HttpRequestArgs args)
        {
            Debug.ArgumentNotNull(args, "args");

            var path = args.Context.Request.Url.LocalPath;
            var handlers = Factory.GetCustomHandlers();

            return FindMatchingHandler(path, handlers) ?? FindMatchingHandler(MainUtil.DecodeName(path), handlers);
        }

        [CanBeNull]
        public static string FindMatchingHandler([NotNull] string path, IEnumerable<CustomHandler> handlers)
        {
            #region Changed code
            return (from handler in handlers
                    // where path.IndexOf(MainUtil.DecodeName(handler.Trigger), StringComparison.OrdinalIgnoreCase) >= 0
                    where path.IndexOf(handler.Trigger, StringComparison.OrdinalIgnoreCase) >= 0 // removed handler.Trigger decoding
                    select handler.Handler).FirstOrDefault(); 
            #endregion
        }
    }
}