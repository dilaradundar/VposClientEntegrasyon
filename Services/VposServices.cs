using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace VposClientEntegrasyonDilara.Services
{
    public class VposServices
    {
        public JsonResult transaction(Sale datas)
        {
            SaleReturn saleReturn = new SaleReturn();
            saleReturn.TransactionType = datas.TransactionType;
            datas.TransactionType = datas.TransactionType.ToLower();

            // Zorunlu verilerin girilip girilmediği kontrolü
            if (string.IsNullOrEmpty(datas.Pan) || datas.CurrencyAmount == 0 || datas.CurrencyCode == 0 || string.IsNullOrEmpty(datas.TransactionType) || string.IsNullOrEmpty(datas.ClientMerchantCode) || string.IsNullOrEmpty(datas.Password) || string.IsNullOrEmpty(datas.Expiry) || datas.Cvv == 0 || string.IsNullOrEmpty(datas.CardHoldersClientIp))
            {
                saleReturn.ResponseMessage = ErrorMessageSale(); //Handler
                return new JsonResult(saleReturn.ResponseMessage);
            }
            else if (datas.TransactionType.Equals("sale"))
            {
                //TransactionId oluşturma ya da atama
                if (string.IsNullOrEmpty(datas.TransactionId))
                {
                    saleReturn.TransactionId = TransactionIdGen();
                }
                else
                {
                    saleReturn.TransactionId = datas.TransactionId.ToString();
                }
                saleReturn.ResponseMessage = "İşlem Başarılı";
                saleReturn.ResultCode = "0000";
                saleReturn.BankRc = saleReturn.ResultCode;

                //BankId İşlemleri
                if (string.IsNullOrEmpty(datas.BankId))
                {
                    saleReturn.BankId = BankIdGen();
                }
                saleReturn.CardBankId = saleReturn.BankId;

                // Pan Control and Masked Pan generation
                int length = datas.Pan.Length;
  
                if (length != 16)
                {
                    saleReturn.ResponseMessage = "Incorrect Pan Info";
                    return new JsonResult(saleReturn.ResponseMessage);
                }
                else
                {
                    saleReturn.PanMasked = MaskedPanGen(datas.Pan);
                }
                //Token Generation
               
                saleReturn.Token = TokenGen();  
                
                //Installment Control
                if (datas.InstallmentCount != 0 && datas.InstallmentCount > 0)
                {
                    saleReturn.InstallmentCount = datas.InstallmentCount;
                }
                return new JsonResult(saleReturn);
            }
            saleReturn.ResponseMessage = "No transaction has activated";
            return new JsonResult(saleReturn.ResponseMessage);
        }
        public JsonResult cancel(SaleCancel cancel)
        {
            SaleCancelReturn saleCancelReturn = new SaleCancelReturn();
            saleCancelReturn.TransactionType = cancel.TransactionType;
            cancel.TransactionType = cancel.TransactionType.ToLower();

            if (string.IsNullOrEmpty(cancel.TransactionType) || string.IsNullOrEmpty(cancel.ClientMerchantCode) || string.IsNullOrEmpty(cancel.Password) || string.IsNullOrEmpty(cancel.ReferenceTransactionId) || string.IsNullOrEmpty(cancel.CardHoldersClientIp))
            {
                saleCancelReturn.ResponseMessage = ErrorMessageSaleCancel();
                return new JsonResult(saleCancelReturn.ResponseMessage);
            }
            else if (cancel.TransactionType.Equals("salecancel"))
            {
                //TransactionId oluşturma ya da atama
                if (string.IsNullOrEmpty(cancel.TransactionId))
                {
                    saleCancelReturn.TransactionId = TransactionIdGen();
                }
                else
                {
                    saleCancelReturn.TransactionId = cancel.TransactionId.ToString();
                }
                cancel.ReferenceTransactionId = saleCancelReturn.ReferenceTransactionId;// İptali yapılan işlemin idsi
                saleCancelReturn.ResponseMessage = "İşlem Başarılı";
                saleCancelReturn.ResultCode = "0000";
                saleCancelReturn.BankRc = saleCancelReturn.ResultCode;

                //BankId İşlemleri
                if (string.IsNullOrEmpty(cancel.BankId))
                {
                    saleCancelReturn.BankId = BankIdGen();
                }
                saleCancelReturn.CardBankId = saleCancelReturn.BankId;

                // Pan Control and Masked Pan generation
                if (string.IsNullOrEmpty(cancel.Pan))
                {
                    cancel.Pan = PanGen();
                    saleCancelReturn.PanMasked = MaskedPanGen(cancel.Pan);
                }

                return new JsonResult(saleCancelReturn);
            }
            saleCancelReturn.ResponseMessage = "No transaction has activated";
            return new JsonResult(saleCancelReturn.ResponseMessage);
        }
        public JsonResult refund(SaleRefund refund)
        {
            SaleRefundReturn saleRefundReturn = new SaleRefundReturn();
            saleRefundReturn.TransactionType = refund.TransactionType;
            refund.TransactionType = refund.TransactionType.ToLower();

            if (string.IsNullOrEmpty(refund.TransactionType) || string.IsNullOrEmpty(refund.ClientMerchantCode) || string.IsNullOrEmpty(refund.Password) || string.IsNullOrEmpty(refund.ReferenceTransactionId) || string.IsNullOrEmpty(refund.CardHoldersClientIp) || double.IsNaN(refund.CurrencyAmount))
            {
                saleRefundReturn.ResponseMessage = ErrorMessageSaleRefund();
                return new JsonResult(saleRefundReturn.ResponseMessage);
            }
            else if (refund.TransactionType.Equals("salerefund"))
            {
                //TransactionId oluşturma ya da atama
                if (string.IsNullOrEmpty(refund.TransactionId))
                {
                    saleRefundReturn.TransactionId = TransactionIdGen();
                }
                else
                {
                    saleRefundReturn.TransactionId = refund.TransactionId.ToString();
                }
                refund.ReferenceTransactionId = saleRefundReturn.ReferenceTransactionId;// İptali yapılan işlemin idsi
                saleRefundReturn.ResponseMessage = "İşlem Başarılı";
                saleRefundReturn.ResultCode = "0000";
                saleRefundReturn.BankRc = saleRefundReturn.ResultCode;

                //BankId İşlemleri
                if (string.IsNullOrEmpty(refund.BankId))
                {
                    saleRefundReturn.BankId = BankIdGen();
                }
                saleRefundReturn.CardBankId = saleRefundReturn.BankId;

                // Pan Control and Masked Pan generation
                if (string.IsNullOrEmpty(refund.Pan))
                {
                    refund.Pan = PanGen();
                    saleRefundReturn.PanMasked = MaskedPanGen(refund.Pan);
                }

                return new JsonResult(saleRefundReturn);

            }
            saleRefundReturn.ResponseMessage = "No transaction has activated";
            return new JsonResult(saleRefundReturn.ResponseMessage);
        }

        public JsonResult Puan(PuanSorgu sorgu)
        {
            return new JsonResult("");
        }


            private string TransactionIdGen()
        {
            string transId;
            Random rnd = new Random();
            transId = rnd.Next(10000, 100000).ToString();
            return transId;
        }
        private string BankIdGen()
        {
            Random rnd = new Random();
            string[] Banks = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "20", "21" };
            int index = rnd.Next(Banks.Length);
            return Banks[index].ToString();
        }
        private string TokenGen()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[32];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);

        }
        private string MaskedPanGen(string pan)
        {
            string mask = "******";
            string tempPanFirst = pan.Substring(0, 6);
            string tempPanLast = pan.Substring(12);
            return tempPanFirst + mask + tempPanLast;
        }
        private string PanGen()
        {
            var chars = "0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }
        private string ErrorMessageSale()
        {
           
            return "please fill in the required parts completely/correctly (Pan,CurrencyAmount,CurrencyCode,TransactionType,ClientMerchantCode,Password,Expiry,Cvv,CardHoldersClientIp)";
        }
        private string ErrorMessageSaleCancel()
        {
            return "please fill in the required parts completely/correctly (TransactionType,ClientMerchantCode,Password,ReferenceTransactionId,CardHoldersClientIp)";
        }
        private string ErrorMessageSaleRefund()
        {
            return "please fill in the required parts completely/correctly (TransactionType,ClientMerchantCode,Password,ReferenceTransactionId,CardHoldersClientIp,CurrencyAmount)";
        }
    }
}
