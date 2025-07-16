using GestaoSpaceEdu.Domain.Enums;

namespace GestaoSpaceEdu.Client.Libraries.Converters;

public class TypeFinancialTransactionToString
{
    public static string Converter(string type)
    {
       return type == TypeFinancialTransaction.Pay.ToString() 
              ? "pagamento" 
              : "recebimento";
    }

    public static string ConverterInfinitive(string type)
    {
        return type == TypeFinancialTransaction.Pay.ToString()
               ? "pagar"
               : "receber";
    }
}
