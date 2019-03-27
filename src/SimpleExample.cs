using System;

namespace ch1_hello_world {
    class MainClass { 

        public static void Main(String[] args){
            string hello = "Hello World it's Cerulean Cee!";
            DateTime now = DateTime.Now;
            Console.Write(hello);
            Console.WriteLine(" The date is " + now.ToLongDateString());
        }
    }
}
