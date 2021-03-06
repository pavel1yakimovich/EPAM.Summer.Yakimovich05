﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Summer.Yakimovich._05
{
    /// <summary>
    /// Class for polynomial arithmetics
    /// </summary>
    public class Polinom : IEquatable<Polinom>
    {
        public CoeffDegree[] Coeff { get;}

        public struct CoeffDegree
        {
            public int Degree { get; set; }
            public double Coeff { get; set; }

            public CoeffDegree(int degree, double coeff)
            {
                Degree = degree;
                Coeff = coeff;
            }
        }

        public int Degree
        {
            get
            {
                if (coeff.Length == 0)
                {
                    return 0;
                }
                return coeff.Length - 1;
            }
        }

        public Polinom(double[] coeff)
        {
            int i = coeff.Length - 1, k = 0;

            for (; i >= 0; i--)
            {
                if (Math.Abs(coeff[i]) > eps)
                {
                    Coeff = new CoeffDegree[i+1];
                    break;
                }
            }

            for (int j = 0; j < i; j++)
            {
                if (Math.Abs(coeff[j]) > eps)
                {
                    Coeff[k] = new CoeffDegree(j, coeff[j]);
                    k++;
                }
            }
        }
        
        public static Polinom operator +(Polinom firstPolinom, Polinom secondPolinom)
        {
            bool firstBigger = (firstPolinom.Degree > secondPolinom.Degree);

            CoeffDegree[] coeff = (firstBigger) ? firstPolinom.Coeff : secondPolinom.Coeff;
            if (firstBigger)
            {
                for (int i = 0; i < coeff.Length; i++)
                    coeff[i] += secondPolinom.Coeff[i];
            }
            else
            {
                for (int i = 0; i < coeff.Length; i++)
                    coeff[i] += firstPolinom.Coeff[i];
            }
            return new Polinom(coeff);
        }
        
        public static Polinom operator -(Polinom firstPolinom, Polinom secondPolinom)
        {

            bool firstBigger = (firstPolinom.Degree > secondPolinom.Degree);

            double[] coeff;
            if (firstBigger)
            {
                coeff = firstPolinom.Coeff;
                for (int i = 0; i < coeff.Length; i++)
                {
                    coeff[i] -= secondPolinom.Coeff[i];
                }
            }
            else
            {
                coeff = new double[secondPolinom.Degree];
                int i = 0;

                for (i = 0; i < firstPolinom.Degree; i++)
                {
                    coeff[i] = firstPolinom.Coeff[i] - secondPolinom.Coeff[i];
                }

                for (; i < coeff.Length; i++)
                {
                    coeff[i] -= secondPolinom.Coeff[i];
                }

            }

            return new Polinom(coeff);
        }
        
        public static Polinom operator *(Polinom firstPolinom, Polinom secondPolinom)
        {
            bool firstBigger = (firstPolinom.Degree > secondPolinom.Degree);

            double[] coeff = (firstBigger) ? firstPolinom.Coeff : secondPolinom.Coeff;
            if (firstBigger)
            {
                for (int i = 0; i < coeff.Length; i++)
                    coeff[i] *= secondPolinom.Coeff[i];
            }
            else
            {
                for (int i = 0; i < coeff.Length; i++)
                    coeff[i] *= firstPolinom.Coeff[i];
            }
            return new Polinom(coeff);
        }
        
        public static Polinom operator *(Polinom somePolinom, int number)
        {
            double[] coeff = somePolinom.Coeff;

            for (int i = 0; i < somePolinom.Degree; i++)
            {
                coeff[i] *= number;
            }

            return new Polinom(coeff);
        }
        
        public static Polinom operator *(int number, Polinom somePolinom)
        {
            double[] coeff = somePolinom.Coeff;

            for (int i = 0; i < somePolinom.Degree; i++)
            {
                coeff[i] *= number;
            }

            return new Polinom(coeff);
        }
        
        public static bool operator ==(Polinom firstPolinom, Polinom secondPolinom)
        {
            try
            {
                return firstPolinom.Equals(secondPolinom);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
        
        public static bool operator !=(Polinom firstPolinom, Polinom secondPolinom)
        {
            try
            {
                return !firstPolinom.Equals(secondPolinom);
            }
            catch (NullReferenceException)
            {
                return true;
            }
        }

        /// <summary>
        /// checks equality of 2 polynoms
        /// </summary>
        /// <param name="somePolinom">polynom we want to check</param>
        /// <returns></returns>
        public bool Equals(Polinom somePolinom)
        {
            if (Degree != somePolinom.Degree)
                return false;
            for (int i = 0; i < this.Degree; i++)
            {
                if (this.Coeff[i] != somePolinom.Coeff[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// overrided equals
        /// </summary>
        /// <param name="somePolinomObj"></param>
        /// <returns></returns>
        public override bool Equals(Object somePolinomObj)
        {
            if (somePolinomObj == null)
                return false;
            Polinom somePolinom = somePolinomObj as Polinom;
            return this.Equals(somePolinom);
        }

        /// <summary>
        /// overrided toString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            bool first = false;

            for (int i = this.Degree - 1; i > 0; i--)
            {
                if (this.Coeff[i] != 0)
                {
                    if (first)
                    {
                        str.Append(" + " + this.Coeff[i] + "x^" + i);
                    }
                    else
                    {
                        str.Append(this.Coeff[i] + "x^" + i);
                        first = true;
                    }
                }
            }
            return str.ToString();
        }

        /// <summary>
        /// Method returns value of polynom with variable
        /// </summary>
        /// <param name="number">variable</param>
        /// <returns></returns>
        public double Sum(double number)
        {
            double sum = 0;

            for (int i = 0; i < this.Degree; i++)
            {
                sum += this.Coeff[i]*Math.Pow(number, i);
            }

            return sum;
        }
    }
}
