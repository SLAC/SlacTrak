//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  CMS_DataUtil.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the class to connect/disconnect the datasource.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using log4net;


namespace ContractManagement.Data
{
    public class CMS_DataUtil
    {
        private string _con;
        private string _errMsg;
        private string _errCode;
        private OracleConnection _ocon;
        private OracleCommand _ocmd;
        private OracleTransaction _otran;
        private string _spName;

        protected static readonly ILog Log = LogManager.GetLogger(typeof(CMS_DataUtil));

        public string SPName
        {
            get { return _spName; }
            set { _spName = value; }
        }

        public OracleCommand CmdName
        {
            get { return _ocmd; }
            set { _ocmd = value; }
        }

        public OracleConnection ConName
        {
            get { return _ocon; }
            set { _ocon = value; }
        }

        public string GetConnectionString()
        {
          
            _con = ConfigurationManager.ConnectionStrings["SLAC_WEB"].ConnectionString; 
            return _con;        
        }

       

        public void ConnectToDB(bool tflag)
        {

         
            try
            {
                _ocon = new OracleConnection();
               // ConName.ConnectionString = GetConnectionString(cname);
                _ocon.ConnectionString = GetConnectionString();
                _ocon.Open();

                if (tflag)
                {
                    _otran = _ocon.BeginTransaction();
                    _ocmd = new OracleCommand("", _ocon);
                    _ocmd.Transaction = _otran;
                }
                else
                {
                    _ocmd = new OracleCommand("", _ocon);
                }

            }
            catch (OracleException oraex)
            {
                Log.Error(oraex);
                _errMsg = oraex.Message;
                _errCode = oraex.Code.ToString();
               
            }
        }

        public void DisconnectDB()
        {
            if (_ocon.State == ConnectionState.Open)
            {
                _ocon.Close();
                _ocmd.Dispose();
            }
        }

        public OracleDataReader GetReader(string sqlText)
        {
            OracleDataReader _odr;
            try
            {
                ConnectToDB(false);
                _ocmd.CommandText = sqlText;
                _odr = _ocmd.ExecuteReader(CommandBehavior.CloseConnection);
                 return _odr;
            }
            catch (OracleException ex)
            {
                Log.Error(ex);
                _errMsg = ex.Message.ToString();
                _errCode = ex.Code.ToString();
                return null;
            }
        }

        public OracleDataReader GetReader(string sqlText, OracleCommand cmdPm)
        {
            OracleDataReader _odr;

            try
            {
                _ocon = new OracleConnection();

                _ocon.ConnectionString = GetConnectionString();
                _ocon.Open();
                cmdPm.Connection = _ocon;
                cmdPm.CommandText = sqlText;
                _odr = cmdPm.ExecuteReader(CommandBehavior.CloseConnection);
                return _odr;
            }
            catch (OracleException ex)
            {
                Log.Error(ex);
                _errMsg = ex.Message.ToString();
                _errCode = ex.Code.ToString();
                return null;
            }

        }


        public DataSet ReturnDataset(string sqlText, string tableName)
        {
            DataSet _ods = new DataSet();
            OracleDataAdapter _oda;

            try
            {
                ConnectToDB(false);
                _ocmd.CommandText = sqlText;
                _oda = new OracleDataAdapter(_ocmd);
                _oda.Fill(_ods,tableName);
                return _ods;
            }
            catch (OracleException oraEx)
            {
                Log.Error(oraEx);
                _errMsg = oraEx.Message.ToString();
                _errCode = oraEx.Code.ToString();
                return null;
            }
            finally
            {
                DisconnectDB();
            }
        }
        
        public DataSet ReturnDataset(string sqlText, string tableName,  OracleCommand cmdPm)
        {
            DataSet _ods = new DataSet();
            OracleDataAdapter _oda;

            try
            {
                _ocon = new OracleConnection();

                _ocon.ConnectionString = GetConnectionString();
                _ocon.Open();
                cmdPm.Connection = _ocon;
                cmdPm.CommandText = sqlText;

                _oda = new OracleDataAdapter(cmdPm);
                _oda.Fill(_ods, tableName);
                return _ods;
            }
            catch (OracleException oraEx)
            {
                Log.Error(oraEx);
                _errMsg = oraEx.Message.ToString();
                _errCode = oraEx.Code.ToString();
                return null;
            }
            finally
            {
                if (_ocon.State == ConnectionState.Open)
                {
                    _ocon.Close();
                   
                }
               
            }
        }

  
        public OracleDataReader GetMultiresultDeliverable()
        {
            try
            {
                ConnectToDB(false);
                _ocmd.Connection = _ocon;
                _ocmd.CommandText = SPName;
                _ocmd.CommandType = CommandType.StoredProcedure;

                _ocmd.Parameters.Add(new OracleParameter("TYPECUR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                _ocmd.Parameters.Add(new OracleParameter("DIRECTORATECUR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                _ocmd.Parameters.Add(new OracleParameter("NOTIFICATIONCUR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                _ocmd.Parameters.Add(new OracleParameter("SSORECEIPIENTSCUR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataReader Rdr = _ocmd.ExecuteReader();
                return Rdr;

            }
            catch (OracleException e)
            {
               Log.Error(e);
                return null;
            }
        }

       
      

    }

    

}