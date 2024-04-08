namespace IntexII.Infrastructure;
//this is so that you can return back to where you once
//were when you leave the cart again
public static class UrlExtensions
{
    public static string PathAndQuery(this HttpRequest request) =>
        request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
}