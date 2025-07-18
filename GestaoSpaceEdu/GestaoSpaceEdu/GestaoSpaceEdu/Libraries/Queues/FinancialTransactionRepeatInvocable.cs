using Coravel.Invocable;
using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Enums;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;

namespace GestaoSpaceEdu.Libraries.Queues;

public class FinancialTransactionRepeatInvocable : IInvocable, IInvocableWithPayload<FinancialTransaction>
{
    private readonly IFinancialTransactionRepository _repository;

    public FinancialTransactionRepeatInvocable(IFinancialTransactionRepository financialTransactionRepository)
    {
        _repository = financialTransactionRepository;
    }

    public FinancialTransaction Payload { get; set; }

    public async Task Invoke()
    {
        int startPoint = 1;
        int countTransactionsSameGroup = await _repository.GetCountTransactionsSameGroupAsync(Payload.Id);
        await AssignRepeatGroupToPayload();

        if(countTransactionsSameGroup == 0)
        {
            await RegisterNewTransactionsAsync(startPoint);
        }
        else
        {
            await RegisterNewTransactionsAsync(countTransactionsSameGroup);
        }

        await TransactionsReductionAsync(countTransactionsSameGroup);
        await RepeatTransactionsRemoveAsync(countTransactionsSameGroup);
    }

    private async Task AssignRepeatGroupToPayload()
    {
        if (Payload.Repeat != Recurrence.None)
        {
            Payload.RepeatGroup = Payload.Id;
            await _repository.UpdateAsync(Payload);
        }
    }

    private async Task RepeatTransactionsRemoveAsync(int countTransactionsSameGroup)
    {
        if (Payload.Repeat == Recurrence.None && countTransactionsSameGroup > 1)
        {
            var transactions = await _repository.GetTransactionsSameGroupAsync(Payload.Id);

            for (int i = 2; i <= countTransactionsSameGroup; i++)
            {
                await _repository.DeleteAsync(transactions.ElementAt(i));
            }
        }
    }

    private async Task TransactionsReductionAsync(int countTransactionsSameGroup)
    {
        if (Payload.Repeat != Recurrence.None && countTransactionsSameGroup > Payload.RepeatTimes)
        {
            var transactions = await _repository.GetTransactionsSameGroupAsync(Payload.Id);

            for (int i = countTransactionsSameGroup; i > Payload.RepeatTimes; i--)
            {
                await _repository.DeleteAsync(transactions.ElementAt(i));
            }
        }
    }

    private async Task RegisterNewTransactionsAsync(int startPoint)
    {
        if (Payload.Repeat != Recurrence.None)
        {
            var repeatTimes = Payload.RepeatTimes -= 1;

            for (int i = startPoint; i <= repeatTimes; i++)
            {
                var financialTransaction = new FinancialTransaction
                {
                    TypeFinancialTransaction = Payload.TypeFinancialTransaction,
                    Description = Payload.Description,
                    ReferenceDate = IncrementDate(Payload.Repeat, i, Payload.ReferenceDate),
                    DueDate = Payload.DueDate.HasValue ? IncrementDate(Payload.Repeat, i, Payload.DueDate.Value) : null,
                    Amount = Payload.Amount,
                    RepeatGroup = Payload.Id,
                    Repeat = Recurrence.None,
                    RepeatTimes = null,
                    CreatedAt = DateTimeOffset.Now,

                    CompanyId = Payload.CompanyId,
                    AccountId = Payload.AccountId,
                    CategoryId = Payload.CategoryId,
                };

                await _repository.AddAsync(financialTransaction);
            }
        }
    }

    private DateTimeOffset IncrementDate(Recurrence repeat, int count, DateTimeOffset date)
    {
        return repeat switch
        {
            //Recurrence.Daily => date.AddDays(1),
            Recurrence.Weekly => date.AddDays(7 * count),
            Recurrence.Monthly => date.AddMonths(count),
            Recurrence.Yearly => date.AddYears(count),
            _ => date,
        };
    }
}
