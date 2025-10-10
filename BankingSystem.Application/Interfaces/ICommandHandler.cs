namespace BankingSystem.Application.CQRS;

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> Handle(TCommand cmd, CancellationToken ct = default);
}
