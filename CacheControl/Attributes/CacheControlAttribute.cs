using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CacheControl.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CacheControlAttribute : ActionFilterAttribute
    {
        private readonly bool? _cacheNoStore;
        private readonly int? _cacheDuration;
        private readonly ResponseCacheLocation? _cacheLocation;
        
        public CacheControlAttribute()
            : this(ResponseCacheLocation.None, 1000)
        {
        }

        public CacheControlAttribute(int duration)
            : this(ResponseCacheLocation.None, duration)
        {
        }

        public CacheControlAttribute(ResponseCacheLocation location, int duration)
        {
            _cacheDuration = duration;
            _cacheLocation = location;
        }

        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_cacheNoStore ?? false)
            {
                // Duration MUST be set (either in the cache profile or in this filter) unless NoStore is true.
                if (_cacheDuration == null)
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                var headers = context.HttpContext.Response.Headers;

                if (_cacheNoStore ?? false)
                {
                    headers.Add("Cache-control", "no-store");
                }

                string cacheControlValue = GetLocation(_cacheLocation);

                cacheControlValue = $"{cacheControlValue}, max-age={_cacheDuration}";

                if (cacheControlValue != null)
                {
                    headers.Add("Cache-control", cacheControlValue);
                }
            }
        }

        private string GetLocation(ResponseCacheLocation? location)
        {
            return location switch
            {
                ResponseCacheLocation.Any => "public",
                ResponseCacheLocation.Client => "private",
                ResponseCacheLocation.None => "no-cache",

                // Default
                _ => throw new InvalidOperationException()
            };
        }
    }
}
