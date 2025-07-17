using Coravel.Invocable;
using GestaoSpaceEdu.Domain.Entities;

namespace GestaoSpaceEdu.Libraries.Queues;

public class FinancialTransactionRepeatInvocable : IInvocable, IInvocableWithPayload<FinancialTransaction>
{
    public FinancialTransaction Payload { get; set; }

    public async Task Invoke()
    {
        Payload.RepeatTimes -= 1;
    }
}
