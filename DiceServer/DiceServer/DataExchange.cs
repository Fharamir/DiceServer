using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

class DataExchange
{
    IPAddress IP;
    Int32 P;

    public DataExchange(IPAddress Addr, Int32 Port)
    {
        IP = Addr;
        P = Port;
    }

    public DataExchange(string Addr, Int32 Port)
    {
        IP = IPAddress.Parse(Addr);
        P = Port;
    }

    /// <summary>
    /// Invia tramite TCPClient i dati all'ip specificato nel costruttore
    /// </summary>
    /// <param name="Data">
    /// I dati da inviare in formato String
    /// </param>
    /// <returns>
    /// Restituisce True se non sono avvenuti errori altrimenti False
    /// </returns>
    public Boolean Send(string Data)
    {
        TcpClient TC = new TcpClient();

        try
        {
            TC.Connect(IP, P);
        }
        catch
        {
            return false;
        }

        try
        {
            NetworkStream NS = TC.GetStream();

            if (NS.CanRead && NS.CanWrite)
            {
                Byte[] B;
                B = Encoding.ASCII.GetBytes(Data);

                NS.Write(B, 0, B.Length);
            }
            TC.Close();
        }
        catch
        {
            TC.Close();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Invia tramite TCPClient i dati all'ip specificato nel costruttore
    /// </summary>
    /// <param name="Data">
    /// I dati da inviare in formato Byte[]
    /// </param>
    /// <returns>
    /// Restituisce True se non sono avvenuti errori altrimenti False
    /// </returns>
    public Boolean Send(byte[] Data)
    {
        TcpClient TC = new TcpClient();

        try
        {
            TC.Connect(IP, P);
        }
        catch
        {
            return false;
        }

        try
        {
            NetworkStream NS = TC.GetStream();

            if (NS.CanRead && NS.CanWrite)
            {
                NS.Write(Data, 0, Data.Length);
            }
            TC.Close();
        }
        catch
        {
            TC.Close();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Avvia le funzioni di server e resta in attesa
    /// dei dati da ricevere sull'ip e la porta specificati
    /// nel costruttore della classe
    /// Solitamente si inserisce all'interno di iterazioni continue
    /// </summary>
    /// <param name="buffersize">
    /// Definisce la dimensione del buffer da ricevere
    /// </param>
    /// <returns>
    /// Restituisce una stringa contenente i dati ricevuti convertiti in caratteri ASCII
    /// </returns>
    public string ReceiveString(int buffersize = 1024)
    {
        string ReceiveData = "";
        try
        {
            TcpListener List = new TcpListener(IP, P);

            List.Start();

            Socket s = List.AcceptSocket();

            byte[] b = new byte[buffersize];
            int k = s.Receive(b);

            ReceiveData = Encoding.ASCII.GetString(b);

            //Rimozione dei dati superflui
            int i = ReceiveData.IndexOf((char)0);
            if (i > 0) ReceiveData = ReceiveData.Substring(0, i);

            s.Close();
            List.Stop();
        }
        catch
        {
            return "";
        }

        return ReceiveData;
    }

    /// <summary>
    /// Avvia le funzioni di server e resta in attesa
    /// dei dati da ricevere sull'ip e la porta specificati
    /// nel costruttore della classe
    /// Solitamente si inserisce all'interno di iterazioni continue
    /// </summary>
    /// <param name="buffersize">
    /// Definisce la dimensione del buffer da ricevere
    /// </param>
    /// <returns>
    /// Restituisce l'array di byte ricevuto
    /// </returns>
    public byte[] ReceiveByteArray(int buffersize = 1024)
    {
        byte[] b = new byte[buffersize];
        try
        {
            TcpListener List = new TcpListener(IP, P);

            List.Start();

            Socket s = List.AcceptSocket();

            int k = s.Receive(b);

            s.Close();
            List.Stop();
        }
        catch
        {
            return new byte[0];
        }

        return b;
    }

}