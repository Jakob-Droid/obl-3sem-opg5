using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Models
{
    public class FanOutPut
    {
        private string _navn;
        private double _temp;
        private double _fugt;
        public int Id { get; set; }
        public string Navn
        {
            get { return _navn; }
            set
            {
                if (value != null)
                {
                    if (value.Length >= 2)
                    {
                        _navn = value;
                    }
                    else
                    {
                        throw new Exception("Navnet skal være længere end 1 karakter");
                    }
                }
                else
                {
                    throw new Exception("Udfyld navn");
                }
            }
        }

        public double Temp
        {
            get { return _temp; }
            set
            {
                if (value >= 15 && value <= 25)
                {
                    _temp = value;
                }
                else if (value < 15)
                {
                    throw new Exception("Temp skal være over 15");
                }
                else if (value > 25)
                {
                    throw new Exception("Temp kan ikke være over 25");
                }

            }
        }
        public double Fugt
        {
            get { return _fugt; }
            set
            {
                if (value >= 30 && value <= 80)
                {
                    _fugt = value;
                }
                else if (value < 30)
                {
                    throw new Exception("Fugt skal være over 30");
                }
                else if (value > 80)
                {
                    throw new Exception("Fugt skal være over 80");
                }
            }
        }
        public FanOutPut(int id, string navn, double temp, double fugt)
        {
            _navn = navn;
            _temp = temp;
            _fugt = fugt;
            Id = id;
        }
        public FanOutPut()
        {

        }
    }
}
