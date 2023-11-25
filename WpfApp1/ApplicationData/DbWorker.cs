using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ApplicationData
{
    internal class DbWorker
    {
        private static var_18363Entities _context;
        public static var_18363Entities GetContext()
        {
            if (_context == null)
            {
                _context = new var_18363Entities();
            }
            return _context;
        }
    }
}
