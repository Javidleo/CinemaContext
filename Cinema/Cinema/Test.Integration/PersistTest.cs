using Microsoft.EntityFrameworkCore;
using System;
using System.Transactions;

namespace Test.Integration
{
    public abstract class PersistTest<T> : IDisposable where T : DbContext, new()
    {
        private TransactionScope scope;
        protected PersistTest()
        {
            scope = new TransactionScope();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}