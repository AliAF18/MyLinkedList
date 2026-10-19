namespace MyLinkedListApp;
public class Node<T>
{
    public Node<T>? Prev { get; set; }
    public Node<T>? Next { get; set; }
    public T Data { get; }
    public Node(T data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data), "Invalid Parameter");
        Data = data;
    }
    public override string ToString() => Data!.ToString() ?? string.Empty;
}