namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IUnitOfWork
{
    /// <summary>
    /// Сохраняет все изменения в рамках транзакции
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Начинает новую транзакцию
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Фиксирует транзакцию
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Откатывает транзакцию
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Выполняет действие в рамках транзакции
    /// </summary>
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет, активна ли транзакция
    /// </summary>
    bool HasActiveTransaction { get; }
}