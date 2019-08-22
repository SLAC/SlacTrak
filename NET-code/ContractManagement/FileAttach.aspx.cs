//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  FileAttach.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//  This is the codebehind for file attachments
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

namespace ContractManagement
{
    public partial class FileAttach : BasePage
    {
        Data.FileUtil objFile = new Data.FileUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();

        protected void Page_Load(object sender, EventArgs e)
        {
            bool _sso =false;
            BtnCancel.Attributes.Add("onClick", "JQueryClose('dialogfile'); return false;");

            _sso = IsSO();
            CheckIfManager();
            if (_cma)
            {
                if (CheckIfEmailOn() == "Y")
                {
                    BtnAttach.Attributes.Add("onClick", "return OpenDialogEmail();");

                }
            }
           if (!Page.IsPostBack)
            {
                string _deliverableId = "";
                _deliverableId = Request.QueryString["id"];
                if ((!string.IsNullOrEmpty(_deliverableId)) && (Regex.IsMatch(_deliverableId, "^[0-9]+$")))
                {
                          HdnId.Value = _deliverableId;
                }
                else
                {
                    Response.Redirect("Error.aspx?msg=pminvld");
                }

            }
        }

        protected bool IsSO()
        {
            if (Session["so"] != null)
            {
                if ((bool)Session["so"])
                {
                    return true;

                }
                else { return false; }
            }
            else { return false; }

        }

        protected bool FileValidation(HttpPostedFile postedFile)
        {
            string _fileName;
            string _fileExtn;
           
            StringBuilder _sbText = new StringBuilder();
            string[] _allowedextn = {".doc",".docx",".jpg",".bmp",".pdf",".xls",".xlsx",".png",".txt", ".gif"};

           
            _fileName = Server.HtmlEncode(objFile.FileFieldFilename(postedFile));
            _sbText.Append("Error attaching the file ");
            

            if (!string.IsNullOrEmpty(_fileName))
            {
                _sbText.Append("~");
                _sbText.Append(_fileName);
                _sbText.Append("~\\n");
                if (_fileName.Length > 90)
                {
                    _sbText.Append("Reason: File Name is too long. \\n");
                    _sbText.Append("Action: Pick a shorter name and try attaching the file/s again");
                    objCommon.CreateMessageAlert(this, _sbText.ToString(), "File error", false);
                    return false;
                }
                if (_fileName.LastIndexOf(".") > -1)
                {
                    _fileExtn = _fileName.Substring(_fileName.LastIndexOf(".")).ToLower();
                }
                else { _fileExtn = ""; }

                if (_fileExtn == ".exe")
                {
                    _sbText.Append("Reason: Executable files cannot be uploaded.\\n");
                    _sbText.Append("Action: Pick a different file and try attaching it again");
                    objCommon.CreateMessageAlert(this, _sbText.ToString(), "File error", false);
                    return false;
                }

                if (_fileExtn == "")
                {
                    _sbText.Append("Reason: Files with no extension cannot be uploaded.\\n");
                    _sbText.Append("Action: Pick a different file and try attaching it again");
                    objCommon.CreateMessageAlert(this, _sbText.ToString(), "File error", false);
                    return false;

                }
                if (!(_allowedextn.Contains(_fileExtn)))
                {
                     _sbText.Append("Reason: File Type not allowed.\\n");
                    _sbText.Append("Action: Pick a file with allowed Type and try attaching it again");
                    objCommon.CreateMessageAlert(this, _sbText.ToString(), "File error", false);
                    return false;
                }


                if (!(postedFile.FileName == "" || postedFile.ContentLength < 1))
                {
                    if ((postedFile.ContentLength / 1024) > 10240)
                    {
                        _sbText.Append("Reason: File size exceeded the allowed limit. \\n");
                        _sbText.Append("Action: Pick a file with allowed size and try attaching the file again");
                        objCommon.CreateMessageAlert(this, _sbText.ToString(), "File error", false);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                _sbText.Append("\\n");
                _sbText.Append("Reason: There is no file to attach. \\n");
                _sbText.Append("Action: Pick a file with allowed size and try attaching the file");
                objCommon.CreateMessageAlert(this, _sbText.ToString(), "File error", false);
                return false;

            }
        }

        protected string Getfiledata(HttpPostedFile m_objFile, int objId)
        {
            string _extension;
            string _filename;
            string _content;
            string _fileid;
            Byte[] _filedata;
            int _filesize;

            _filename = objFile.FileFieldFilename(m_objFile);
            _extension = _filename.Substring(_filename.IndexOf("."));
            _content = objFile.FileFieldType(m_objFile);
            _filedata = objFile.GetByteArrayFromFileField(m_objFile);
            _filesize = objFile.FileFieldLength(m_objFile);
            try
            {
                _fileid = objDml.InsertFileData(objId, _filename, _filesize, _content, _filedata, objCommon.GetUserID());
                return _fileid.ToString();
            }
            catch
            {
                return "-1";
            }
        }

        protected void BtnAttach_Click(object sender, EventArgs e)
        {
            
         
            string _result = "";
            if (FileValidation(FUDocument.PostedFile))
            {
                _result = Getfiledata(FUDocument.PostedFile, Convert.ToInt32(HdnId.Value));

                if (_result == "-1")
                {
                    objCommon.CreateMessageAlert(this, "Error! File could not be attached. Please try later", "File error", false);
                }
                else
                {
                   
                         bool _isso = IsSO();
 
                        if (!_isso)
                        {                                                      
                            if ((HdnEmail.Value == "yes") || (HdnEmail.Value == ""))
                            {
                                objDml.SendEmail(Convert.ToInt32(HdnId.Value), 8, "");
                            }
                        }
                           
                }               

            }
 
        }

      
     
    }
}