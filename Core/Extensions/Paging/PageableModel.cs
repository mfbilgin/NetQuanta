namespace Core.Extensions.Paging;

public class PageableModel<T>(IList<T> data, int index, int size, int count)
{
    public IList<T> Data { get; set; } = data;
    public int Index { get; set; } = index;
    public int Size { get; set; } = size;
    public int Count { get; set; } = count;
    public int Pages => (int)Math.Ceiling((double)Count / Size);
    public bool HasPrevious => Index > 1;
    public bool HasNext => Index < Pages;
}