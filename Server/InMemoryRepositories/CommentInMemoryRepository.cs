using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> comments = new();

    public CommentInMemoryRepository()
    {
        _ = AddCommentAsync(new Comment("Leg day sucks!", 1, 1)).Result;
        _ = AddCommentAsync(new Comment("So true!", 1, 2)).Result;
        _ = AddCommentAsync(new Comment("Dont agree, leg days are nice!", 1, 2)).Result;
        _ = AddCommentAsync(new Comment("Try chest next time :)", 1, 1)).Result;
        _ = AddCommentAsync(new Comment("AHAHHAHAHAHAHA!", 1, 4)).Result;
        
        _ = AddCommentAsync(new Comment("Happens to the bests of us.", 2, 2)).Result;
        _ = AddCommentAsync(new Comment("Dude, that's sounds so familiar.", 2, 3)).Result;
        _ = AddCommentAsync(new Comment("Are you alright?",2, 2)).Result;
        _ = AddCommentAsync(new Comment("No way pal", 2, 1)).Result;

        _ = AddCommentAsync(new Comment("Anyways Phyton is better.", 3, 1)).Result;
        _ = AddCommentAsync(new Comment("Totally agree.", 3, 2)).Result;
        _ = AddCommentAsync(new Comment("C# is actually the best programming language.", 3, 4)).Result;
        _ = AddCommentAsync(new Comment("Everything but Java!", 3, 4)).Result;
        
        _ = AddCommentAsync(new Comment("It sucks!", 4, 4)).Result;
        _ = AddCommentAsync(new Comment("Sounds tasty.", 4, 3)).Result;
        _ = AddCommentAsync(new Comment("Can you specify which ingredients did you use?", 4, 4)).Result;
        _ = AddCommentAsync(new Comment("Not that bad", 4, 1)).Result;
        
        _ = AddCommentAsync(new Comment("Did you pet him?", 5, 1)).Result;
        _ = AddCommentAsync(new Comment("I love bears!!!", 5, 1)).Result;
        _ = AddCommentAsync(new Comment("Sounds scary.", 5, 2)).Result;
        _ = AddCommentAsync(new Comment("Did you give him a name?", 5, 3)).Result;
        
        _ = AddCommentAsync(new Comment("I am not into reading :(", 6, 2)).Result;
        _ = AddCommentAsync(new Comment("Try I am Malala, its a great book!", 6, 4)).Result;
        _ = AddCommentAsync(new Comment("1984 is an interesting book!", 6, 3)).Result;
        _ = AddCommentAsync(new Comment("No idea pal.", 6, 1)).Result;
    }

    public Task<Comment> AddCommentAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment); 
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        
        comments.Remove(existingComment);
        comments.Add(comment);

        await Task.CompletedTask;
    }

    public Task DeleteCommentAsync(int id)
    {
        var commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleCommentAsync(int id)
    {
        var user = comments.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        return Task.FromResult(user);
    }

    public Task<IEnumerable<Comment>> GetManyCommentsAsync()
    {
        return Task.FromResult((IEnumerable<Comment>)comments.ToList());
    }

    public Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        throw new NotImplementedException();
    }
}