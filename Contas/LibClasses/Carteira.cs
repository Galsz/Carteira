﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contas.LibClasses
{
    public class Carteira
    {
        public double Saldo
        {
            get;
            private set;
        }
        public string Dono { get; set; }
        public string CPF { get; set; }
        public int nconta { get; set; }

        public double limite { get; set; }

        public DateTime Tarifa { get; set; }

        public bool CobrarTarifa(DateTime date)
        {
      
            this.Saldo -= 19.90;

            this.Tarifa = date;

            return true;
        }
         

        public bool Sacar(double Valor, DateTime date) //parametro criado para limitar dentro do horario
        {
            if (!((date.Hour >= 8) && (date.Hour <= 18)))
                return false;

            return this.Sacar(Valor);
        }

        public bool Sacar(double Valor)
        {

            if (Valor > (this.Saldo + this.limite))
                return false;

            this.Saldo -= Valor;
            //this.Saldo = Saldo - Valor;
            return true;

        }

        public bool Depositar(double Valor, DateTime date) //parametro criado para limitar dentro do horario
        {
            if (!((date.Hour >= 8) && (date.Hour <= 18)))
                return false;
            return this.Depositar(Valor);
        }

        public bool Depositar(double Valor)
        {
    
            this.Saldo += Valor;
            return true;
        }

        public bool Transferir
            (Carteira destino, double valor)
        {  
            //se nao tiver saldo cancela transferencia retornando false
            if (this.Saldo <= valor)
                return false;

            //Executa transferencia tirando da conta origram e deposinto na conta destino
            this.Sacar(valor);
            bool tOK = destino.Depositar(valor);
            if (tOK)// se transferencia ocorreu com sucesso retorna true
                return true;
            else// caso ocorrer erro faz o rollback voltando dinheiro para conta de origem
            {
                this.Depositar(valor);
                return false;
            }
        }
    }
}
