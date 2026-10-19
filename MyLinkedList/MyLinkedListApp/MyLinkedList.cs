using System.Text;
namespace MyLinkedListApp;
public class MyLinkedList<T>
{
    private Node<T>? Head { get; set; }
    private Node<T>? Tail { get; set; }
    public int Count
    {
        get
        {
            int cnt = 0;
            for (Node<T>? node = Head; node != null; node = node.Next) cnt++;
            return cnt;
        }
    }
    public void AddAtStart(T item) => InsertAt(0, item);
    public void AddAtEnd(T item) => InsertAt(Count, item);
    public void InsertAt(int index, T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Invalid Parameter");
        int count = Count;
        if (index < 0 || index > count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        Node<T> newNode = new Node<T>(item);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
            return;
        }
        if (index == 0)
        {
            newNode.Next = Head;
            Head.Prev = newNode;
            Head = newNode;
            return;
        }
        if (index == count)
        {
            Tail!.Next = newNode;
            newNode.Prev = Tail;
            Tail = newNode;
            return;
        }
        Node<T> current = GetNodeAt(index);
        newNode.Prev = current.Prev;
        newNode.Next = current;
        current.Prev!.Next = newNode;
        current.Prev = newNode;
    }
    public T RemoveFirst() => RemoveAt(0);
    public T RemoveLast() => RemoveAt(Count - 1);
    public T RemoveAt(int index)
    {
        int count = Count;
        if (count == 0)
            throw new InvalidOperationException("List is empty.");
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        Node<T> target = GetNodeAt(index);
        if (target.Prev == null) Head = target.Next;
        else target.Prev.Next = target.Next;
        if (target.Next == null) Tail = target.Prev;
        else target.Next.Prev = target.Prev;
        target.Next = null;
        target.Prev = null;
        return target.Data;
    }
    public bool Contains(T other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other), "Invalid Parameter");
        for (Node<T>? node = Head; node != null; node = node.Next)
        {
            if (EqualityComparer<T>.Default.Equals(node.Data, other)) return true;
        }
        return false;
    }
    private Node<T> GetNodeAt(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index), "Index cannot be negative.");
        Node<T>? current = Head;
        for (int i = 0; i < index && current != null; i++)
            current = current.Next;
        if (current == null)
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        return current;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (Node<T>? node = Head; node != null; node = node.Next)
            sb.AppendLine(node.Data!.ToString());
        return sb.ToString();
    }
}