namespace FlavorTalk.Shared;

public class HttpResponse<T> where T : class
{
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = [];
    public List<string> Reasons { get; set; } = [];
}
