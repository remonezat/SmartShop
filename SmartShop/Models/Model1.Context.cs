﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartShop.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SmartShopEntities : DbContext
    {
        public SmartShopEntities()
            : base("name=SmartShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CustomerPayment> CustomerPayments { get; set; }
        public virtual DbSet<EmployeeDiscount> EmployeeDiscounts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeesWithdraw> EmployeesWithdraws { get; set; }
        public virtual DbSet<Expencess> Expencesses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Jop> Jops { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual DbSet<PurchaseRe> PurchaseRes { get; set; }
        public virtual DbSet<PurchaseReDetail> PurchaseReDetails { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SalesDetail> SalesDetails { get; set; }
        public virtual DbSet<SalesRe> SalesRes { get; set; }
        public virtual DbSet<SalesReDetail> SalesReDetails { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreTransfer> StoreTransfers { get; set; }
        public virtual DbSet<StoreTransferDetail> StoreTransferDetails { get; set; }
        public virtual DbSet<SupplierPayment> SupplierPayments { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<Get_PersonAccountCredit_Result> Get_PersonAccountCredit(Nullable<int> acc_id)
        {
            var acc_idParameter = acc_id.HasValue ?
                new ObjectParameter("acc_id", acc_id) :
                new ObjectParameter("acc_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_PersonAccountCredit_Result>("Get_PersonAccountCredit", acc_idParameter);
        }
    
        public virtual ObjectResult<GetAccountStatement_Result> GetAccountStatement(Nullable<int> acc_id, Nullable<System.DateTime> dte_f, Nullable<System.DateTime> dte_t)
        {
            var acc_idParameter = acc_id.HasValue ?
                new ObjectParameter("acc_id", acc_id) :
                new ObjectParameter("acc_id", typeof(int));
    
            var dte_fParameter = dte_f.HasValue ?
                new ObjectParameter("dte_f", dte_f) :
                new ObjectParameter("dte_f", typeof(System.DateTime));
    
            var dte_tParameter = dte_t.HasValue ?
                new ObjectParameter("dte_t", dte_t) :
                new ObjectParameter("dte_t", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAccountStatement_Result>("GetAccountStatement", acc_idParameter, dte_fParameter, dte_tParameter);
        }
    
        public virtual ObjectResult<Get_PersonAccountCredit_before_Result> Get_PersonAccountCredit_before(Nullable<int> acc_id, Nullable<System.DateTime> beforeDte)
        {
            var acc_idParameter = acc_id.HasValue ?
                new ObjectParameter("acc_id", acc_id) :
                new ObjectParameter("acc_id", typeof(int));
    
            var beforeDteParameter = beforeDte.HasValue ?
                new ObjectParameter("BeforeDte", beforeDte) :
                new ObjectParameter("BeforeDte", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_PersonAccountCredit_before_Result>("Get_PersonAccountCredit_before", acc_idParameter, beforeDteParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> ProcGetItemCredit_Stock(Nullable<int> it_id, Nullable<int> stk_id)
        {
            var it_idParameter = it_id.HasValue ?
                new ObjectParameter("it_id", it_id) :
                new ObjectParameter("it_id", typeof(int));
    
            var stk_idParameter = stk_id.HasValue ?
                new ObjectParameter("stk_id", stk_id) :
                new ObjectParameter("stk_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("ProcGetItemCredit_Stock", it_idParameter, stk_idParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetShift_id()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetShift_id");
        }
    
        public virtual ObjectResult<GetAllBills_Result> GetAllBills()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllBills_Result>("GetAllBills");
        }
    
        public virtual ObjectResult<DailyReport_Result> DailyReport(Nullable<System.DateTime> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DailyReport_Result>("DailyReport", dateParameter);
        }
    
        public virtual ObjectResult<MonthlyReport_Result> MonthlyReport(Nullable<System.DateTime> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MonthlyReport_Result>("MonthlyReport", dateParameter);
        }
    
        public virtual ObjectResult<MonthReport_Result> MonthReport(Nullable<System.DateTime> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MonthReport_Result>("MonthReport", dateParameter);
        }
    
        public virtual ObjectResult<YearReport_Result> YearReport(Nullable<double> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<YearReport_Result>("YearReport", dateParameter);
        }
    
        public virtual ObjectResult<GetItemsCredit_proc_Result> GetItemsCredit_proc(Nullable<int> stk)
        {
            var stkParameter = stk.HasValue ?
                new ObjectParameter("stk", stk) :
                new ObjectParameter("stk", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetItemsCredit_proc_Result>("GetItemsCredit_proc", stkParameter);
        }
    }
}
