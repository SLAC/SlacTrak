//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  FileUtil.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the class that has all file related common functions.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ContractManagement.Data
{
    public class FileUtil
    {
        public byte[] GetByteArrayFromFileField(HttpPostedFile filMyFile)
        {
            // Returns a byte array from the passed 
            // file field controls file
            int intFileLength = 0;
            byte[] bytData = null;
            System.IO.Stream objStream = null;
            if (FileFieldSelected(filMyFile))
            {
                intFileLength = filMyFile.ContentLength;
                bytData = new byte[intFileLength + 1];
                objStream = filMyFile.InputStream;
                objStream.Read(bytData, 0, intFileLength);
                return bytData;
            }
            else
                return null;
        }

        public string FileFieldType(HttpPostedFile filMyFile)
        {
            // Returns the type of the posted file
            if ((filMyFile != null))
                return filMyFile.ContentType;
            else
                return null;
        }

        public int FileFieldLength(HttpPostedFile filMyFile)
        {
            // Returns the length of the posted file
            if ((filMyFile != null))
                return filMyFile.ContentLength;
            else
                return -1;
        }

        public string FileFieldFilename(HttpPostedFile filMyFile)
        {
            // Returns the core filename of the posted file
            if (filMyFile != null)
            {
                return Path.GetFileName(filMyFile.FileName);
            }
            else
                return null;
        }

        public bool FileFieldSelected(HttpPostedFile filMyFile)
        {
            //Returns true if the passed has the posted file
            if ((filMyFile == null) || (filMyFile.ContentLength == 0))
            {
                return false;
            }
            else
                return true;


        }
    }
}