using System.Collections.Generic;

namespace VposClientEntegrasyonDilara
{
    interface IOperation
    {
        public string TransactionType { get; set; }
        public string ClientMerchantCode { get; set; }
        public string Password { get; set; }
        public string BankId { get; set; }
        public string TransactionId { get; set; }
        public double CurrencyAmount { get; set; }
        public int CurrencyCode { get; set; }
        public string Pan { get; set; }
        public string Expiry { get; set; }
        public int Cvv { get; set; }
        public int CustomerId { get; set; }
        public string ResponseMessage { get; set; }
        public string ResultCode { get; set; }

    }
    public class Sale : IOperation
    {
        public string ResultCode { get; set; }
        public string ResponseMessage { get; set; }
        public string TransactionId { get; set; }
        public int InstallmentCount { get; set; }
        public string LoyaltyAmount { get; set; }
        public string BankId { get; set; }
        public string CardBankId { get; set; }
        public string TransactionType { get; set; }
        public string ClientMerchantCode { get; set; }
        public string Password { get; set; }
        public double CurrencyAmount { get; set; }
        public int CurrencyCode { get; set; }
        public string Pan { get; set; }
        public string Expiry { get; set; }
        public int Cvv { get; set; }
        public int CustomerId { get; set; }
        public string CardHoldersClientIp { get; set; }
        public string CardHoldersEmail { get; set; }
   
    }
    public class SaleCancel
    {
        public string ClientMerchantCode { get; set; }
        public string Password { get; set; }
        public string TransactionType { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string CardHoldersClientIp { get; set; }
        public string BankId { get; set; }
        public string TransactionId { get; set; }
        public string SubMerchantCode { get; set; }
        public string Pan { get; set; }

    }
    public class SaleRefund
    {
        public string ClientMerchantCode { get; set; }
        public string Password { get; set; }
        public string TransactionType { get; set; }
        public string ReferenceTransactionId { get; set; }
        public double CurrencyAmount { get; set; }
        public string CardHoldersClientIp { get; set; }
        public string BankId { get; set; }  
        public string TransactionId { get; set; }   
        public string SubMerchantCode { get; set; }
        public string Pan { get; set; }

    }
    
}
