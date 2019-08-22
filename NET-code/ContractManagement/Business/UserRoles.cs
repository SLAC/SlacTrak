//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  UserRoles.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is class with different user roles
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;

namespace ContractManagement.Business
{
    public class UserRoles
    {
        Data.CMS_DataUtil objData = new Data.CMS_DataUtil();
        enum UserType
        {
            CMA,
            ALD,
            DIRADMIN,
            SSO,
            ADMIN,
            SSOSUPER
        }

        private bool _isAdmin = false;
        private bool _isALD = false;
        private bool _isCMA = false;
        private bool _isSSO = false;
        private bool _isDiradmin = false;
        private bool _isSSOSuper = false;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        public bool IsALD
        {
            get { return _isALD; }
            set { _isALD = value; }
        }

        public bool IsCMA
        {
            get { return _isCMA; }
            set { _isCMA = value; }
        }

        public bool IsSSO
        {
            get { return _isSSO; }
            set { _isSSO = value; }
        }

        public bool IsDirAdmin
        {
            get { return _isDiradmin; }
            set { _isDiradmin = value; }
        }

        public bool IsSSOSuper
        {
            get { return _isSSOSuper; }
            set { _isSSOSuper = value; }
        }


        public void GetUserRole(string userId)
        {

            string _strUser = "SELECT MANAGER_TYPE FROM CMS_MANAGER WHERE IS_ACTIVE ='Y' AND SLAC_ID = :UserId";
            using (OracleCommand _cmdUser = new OracleCommand())
            {
                _cmdUser.CommandType = CommandType.Text;
                _cmdUser.Parameters.AddWithValue("UserId", userId);
                _cmdUser.CommandText = _strUser;
                using (DataSet _dsUser = objData.ReturnDataset(_strUser, "roles", _cmdUser))
                {
                    DataTable _dtRoles = _dsUser.Tables["roles"];

                    for (int i = 0; i < _dtRoles.Rows.Count; i++)
                    {
                        string _type;
                        _type = _dtRoles.Rows[i][0].ToString();

                        if (_type.Equals(UserType.ADMIN.ToString()))
                        {
                            IsAdmin = true;
                        }
 
                        if (_type.Equals(UserType.ALD.ToString()))
                        {
                            IsALD = true;
                        }
 
                        if (_type.Equals(UserType.CMA.ToString()))
                        {
                            IsCMA = true;
                        }
 
                        if (_type.Equals(UserType.SSO.ToString()))
                        {
                            IsSSO = true;
                        }
 
                        if (_type.Equals(UserType.DIRADMIN.ToString()))
                        {
                            IsDirAdmin = true;
                        }
 
                        if (_type.Equals(UserType.SSOSUPER.ToString()))
                        {
                            IsSSOSuper = true;
                        }
                    }
                  
                }

            }

            HttpContext.Current.Session["admin"] = IsAdmin;
            HttpContext.Current.Session["ald"] = IsALD;
            HttpContext.Current.Session["cma"] = IsCMA;
            HttpContext.Current.Session["diradmin"] = IsDirAdmin;
            HttpContext.Current.Session["sso"] = IsSSO;
            HttpContext.Current.Session["ssosuper"] = IsSSOSuper;

        }

       
    }
}