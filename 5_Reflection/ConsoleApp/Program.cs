using MyIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {

      var container = new Container();
      //container.AddAssembly(Assembly.GetExecutingAssembly());

      container.AddType(typeof(CustomerBLL));
      container.AddType(typeof(Logger));
      container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

      var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
      var customerBLL2 = container.CreateInstance<CustomerBLL2>();

      Console.WriteLine(customerBLL.ToString());
      Console.WriteLine(customerBLL2.ToString());
    }
  }
}
