namespace BankAccountLibrary;

public class BankAccount
{
    public string BankName { get; private set; }
    public string Inn { get; private set; }
    public string Bik { get; private set; }
    public string CorporationAccount { get; private set; }
    public decimal Balance { get; private set; }
    public decimal WithdrawalCommission { get; }
    public decimal CreditInterestRate { get; }

    public BankAccount(string bankName, string inn, string bik, string corporationAccount, decimal balance,
        decimal withdrawalCommission, decimal creditInterestRate)
    {
        #region Conditions

        if (balance < 0)
            throw new ArgumentException("Нельзя создать счёт с отрицательным балансом");
        if (withdrawalCommission < 0)
            throw new ArgumentException("Коммиссия банка не может быть меньше нуля");
        if (creditInterestRate <= 0)
            throw new ArgumentException("Процент банка не может быть меньше либо равен 0");

        #endregion
        
        this.BankName = bankName;
        this.Inn = inn;
        this.Bik = bik;
        this.CorporationAccount = corporationAccount;
        this.Balance = balance;
        this.WithdrawalCommission = withdrawalCommission;
        this.CreditInterestRate = creditInterestRate;
    }

    public void Deposit(decimal amount)
    {
        var newBalance = this.Balance - amount - WithdrawalCommission;
        ThrowExceptionIfBalanceLessZero(newBalance);
        this.Balance = newBalance;
    }

    public void ChargePercentsInAccount()
    {
        this.Balance *= 1 + CreditInterestRate * Convert.ToDecimal(0.01);
        this.Balance = RoundUp(this.Balance, 2);
    }
    
    private decimal RoundUp(decimal number, int digits)
    {
        var factor = Convert.ToDecimal(Math.Pow(10, digits));
        return Math.Ceiling(number * factor) / factor;
    }
    
    private void ThrowExceptionIfBalanceLessZero(decimal newBalance)
    {
        if (newBalance < 0)
            throw new ArgumentException("В ходе операции баланс станец отрицательным. Операция отклонена");
    }

    public override string ToString()
    {
        return $"Банковский аккаунт: Банк - {BankName} " +
               $"ИНН - {Inn} " +
               $"БИК - {Bik} " +
               $"Корпоративный Аккаунт - {CorporationAccount} " +
               $"Баланс - {Balance} " +
               $"Коммиссия при переводе - {WithdrawalCommission} " +
               $"Процент на счёт - {CreditInterestRate}";
    }
}