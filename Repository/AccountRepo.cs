﻿using new_Karlshop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_Karlshop.Repository
{
    public class AccountRepo
    {
        ApplicationDbContext _context;

        public AccountRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts;
        }

        public Boolean FindAccount(string username)
        {
            Account exist = new Account();
            exist =  _context.Accounts.Where(un => un.ApplicationUser.UserName == username).FirstOrDefault();
            if (exist != null)
            {
                return true;
            }
            return false;
        }

        public string GetAccountMaxID()
        {
            return _context.Accounts.Select(id => id.Id).Max(); 
        }


        //public int GetAccountNumByUserName(string username)
        //{
        //    return _context.Accounts.Where(un => un.ApplicationUser.UserName == username).Select(num => num.Id).FirstOrDefault();
        //}

        public void DelOneAccountByNum(string id)
        {
            Account account = _context.Accounts.Where(a => a.Id == id).FirstOrDefault();
            IEnumerable<AccountGood> accountGoods = _context.AccountGoods.Where(a => a.Account_ID == id);
            foreach (var accountGood in accountGoods)
            {
                _context.AccountGoods.Remove(accountGood);
            }
            _context.SaveChanges();
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }


        public Account GetOneAccountByNum(string id)
        {
            return _context.Accounts.Where(a => a.Id == id).FirstOrDefault();
        }

        public void AddOneAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void EditOneAccount(Account account)
        {
           Account result = _context.Accounts.Where(a => a.Id == account.Id).FirstOrDefault();

           result.firstName = account.firstName;
           result.lastName = account.lastName;
          
           result.phone = account.phone;
           result.address  = account.address ;
            _context.SaveChanges();
        }

        public void QuickEditAccount(Account account)
        {
            Account result = _context.Accounts.Where(a => a.Id == account.Id).FirstOrDefault();
            result.firstName = account.firstName;
            result.lastName = account.lastName;
      
            result.phone = account.phone;
            result.address = account.address;
            _context.SaveChanges();
        }

    }
}
