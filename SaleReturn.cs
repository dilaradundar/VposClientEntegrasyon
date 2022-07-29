using System;

namespace VposClientEntegrasyonDilara
{
    public class SaleReturn
    {
        public string ResultCode { get; set; }
        public string ResponseMessage { get; set; }
        public string TransactionId { get; set; }
        public int InstallmentCount { get; set; }
        public string LoyaltyAmount { get; set; }
        public string BankId { get; set; }
        public string CardBankId { get; set; }
        public string PanMasked { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string TransactionType { get; set; }
        public string BankRc { get; set; }
        public string AuthCode { get; set; }
        public string MerchantId { get; set; }
        public string Token { get; set; }   
        public string PaymentUrl { get; set; }  
        public DateTime VposResponseDate { get; set; } = DateTime.Now; 
        public string HashedData { get; set; }
       
       
    }
    public class SaleCancelReturn
    {
        public string ResultCode { get; set; }
        public string ResponseMessage { get; set; }
        public string TransactionId { get; set; }
        public string LoyaltyAmount { get; set; }
        public string BankId { get; set; }
        public string CardBankId { get; set; }
        public string CardType { get; set; }    
        public string PanMasked { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string TransactionType { get; set; }
        public string BankRc { get; set; }
        public string AuthCode { get; set; }
        public string MerchantId { get; set; }
        public string Token { get; set; }
        public DateTime VposResponseDate { get; set; } = DateTime.Now;
      

    }
    public class SaleRefundReturn
    {
        public string ResultCode { get; set; }
        public string ResponseMessage { get; set; }
        public string TransactionId { get; set; }
        public string LoyaltyAmount { get; set; }
        public string BankId { get; set; }
        public string CardBankId { get; set; }
        public string CardType { get; set; }
        public string PanMasked { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string TransactionType { get; set; }
        public string BankRc { get; set; }
        public string AuthCode { get; set; }
        public string MerchantId { get; set; }
        public string Token { get; set; }
        public DateTime VposResponseDate { get; set; } = DateTime.Now;

    }
   
}
