namespace Sitecore.Support.Pipelines.HttpRequest
{
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines.HttpRequest;

    public class CustomHandlers : Sitecore.Pipelines.HttpRequest.CustomHandlers
    {
        public override void Process([NotNull] HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (this.IsHandlerResolved(args))
            {
                args.AbortPipeline();
            }

            #region Changed code
            var handler = Sitecore.Support.Pipelines.HttpRequest.HandlerUtil.GetHandler(args); // using patched HandlerUtil
            #endregion

            if (string.IsNullOrEmpty(handler))
            {
                return;
            }

            args.Context.RewritePath(handler, args.Context.Request.Url.LocalPath, args.Url.QueryString, true);
            Context.Diagnostics.AbortProfiling();
            Context.Diagnostics.AbortTracing();

            args.AbortPipeline();
        }
    }
}