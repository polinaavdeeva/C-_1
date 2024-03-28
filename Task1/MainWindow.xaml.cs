using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BankAccountLibrary;

namespace Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BankAccount? _bankAccount;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _bankAccount = new BankAccount(
                    BankNameBox.Text,
                    InnBox.Text,
                    BikBox.Text,
                    CorporateBox.Text,
                    Convert.ToDecimal(BalanceBox.Text),
                    Convert.ToDecimal(WithdrawalCommissionBox.Text),
                    Convert.ToDecimal(CreditRateBox.Text)
                );
                MessageBox.Show(_bankAccount.ToString());
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Введены некорректные значения для числовых полей");
            }

            RefreshBankLabel();
        }

        private void BalanceBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        private void DepositButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_bankAccount == null)
            {
                MessageBox.Show("Нельзя производить действия с аккаунтом, пока Вы его не создадите");
                return;
            }

            try
            {
                var amount = Convert.ToDecimal(DepositBox.Text);
                _bankAccount.Deposit(amount);

                MessageBox.Show($"Операция на изменение счёта была проведена успешно. Текущий счёт - {_bankAccount.Balance}");
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Введены некорректные значения для числового поля");
            }

            RefreshBankLabel();
        }

        private void ChargePercentsInAccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_bankAccount == null)
            {
                MessageBox.Show("Нельзя производить действия с аккаунтом, пока Вы его не создадите");
                return;
            }
            
            _bankAccount.ChargePercentsInAccount();
            RefreshBankLabel();
        }

        private void RefreshBankLabel()
        {
            if(_bankAccount != null)
                BankAccountLabel.Text = _bankAccount.ToString();
        }
    }
}