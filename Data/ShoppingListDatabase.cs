using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Sirb_Maria_Lab7.Models;

namespace Sirb_Maria_Lab7.Data
{
    public class ShoppingListDatabase
    {
        //Aceasta clasa va contine cod pentru crearea, citirea, srierea si stergerea datelor
        //Utilizam API-r SQLite asincrone pentru a pune operatiile pe baza de date in thread-uri de background
        //Constructorul a estei clase ia ca si argument calea catre fisierul de tip baza de date 
        // => aceasta cale este furnizata de clasa App in pasul urmator

        readonly SQLiteAsyncConnection _database;
        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
        }
        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }
        public Task<ShopList> GetShopListAsync(int id)
        {
            return _database.Table<ShopList>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveShopListAsync(ShopList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);

            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }
        public Task<int> DeleteShopListAsync(ShopList slist)
        {
            return _database.DeleteAsync(slist);
        }

    }

}
