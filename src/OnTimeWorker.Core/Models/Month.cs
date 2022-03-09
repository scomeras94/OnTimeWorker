using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models
{
    public class Month
    {
        public string number { get; set; }
        public string name { get; set; }

        public Month(string number)
        {
            this.number = number;
            this.name = GetMonthName(this.number);
        }

        private string GetMonthName(string number)
        {
            if (number == "1")
                return "Enero";

            if (number == "2")
                return "Febrero";

            if (number == "3")
                return "Marzo";

            if (number == "4")
                return "Abril";

            if (number == "5")
                return "Mayo";

            if (number == "6")
                return "Junio";

            if (number == "7")
                return "Julio";

            if (number == "8")
                return "Agosto";

            if (number == "9")
                return "Septiembre";

            if (number == "10")
                return "Octubre";

            if (number == "11")
                return "Noviembre";

            if (number == "12")
                return "Diciembre";

            return "Error";
        }
    }
}
