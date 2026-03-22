using LearningPlatformApi.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearningPlatformApi.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext context;
    private IDbContextTransaction? currentTransaction;
    private bool disposed;

    public bool HasActiveTransaction => currentTransaction != null;

    public UnitOfWork(ApplicationContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Сохраняет изменения в базе данных
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Начинает новую транзакцию
    /// </summary>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress");
        }

        currentTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Фиксирует текущую транзакцию
    /// </summary>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress");
        }

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            await currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            currentTransaction?.Dispose();
            currentTransaction = null;
        }
    }

    /// <summary>
    /// Откатывает текущую транзакцию
    /// </summary>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress");
        }

        try
        {
            await currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            currentTransaction?.Dispose();
            currentTransaction = null;
        }
    }

    /// <summary>
    /// Выполняет действие в рамках транзакции с автоматическим управлением
    /// </summary>
    public async Task<T> ExecuteInTransactionAsync<T>(
        Func<Task<T>> action,
        CancellationToken cancellationToken = default)
    {
        if (HasActiveTransaction)
        {
            // Если транзакция уже активна, просто выполняем действие
            return await action();
        }

        await BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await action();
            await CommitTransactionAsync(cancellationToken);
            return result;
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            currentTransaction?.Dispose();
            context.Dispose();
        }
        disposed = true;
    }
}