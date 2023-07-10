/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program. If not, see <http://www.gnu.org/licenses/>

    This library makes use of the iTextSharp PDF library.
    See http://itextpdf.com/terms-of-use/ for their license information.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EncoderLib
{
    public class EncoderFileListen
    {
        private System.IO.FileSystemWatcher _fileSystemWatcher;
        private string myfilepath = "";
        private string outpath = "";
        public EncoderFileListen() {
            myfilepath = @"D:\pdfenc\";
            if (!System.IO.Directory.Exists(myfilepath)) {
                myfilepath = @"C:\pdfenc\";
            }
            outpath = myfilepath + @"out\";
            System.IO.Directory.CreateDirectory(outpath);

            this._fileSystemWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this._fileSystemWatcher)).BeginInit();
            this._fileSystemWatcher.EnableRaisingEvents = true;
            this._fileSystemWatcher.Filter = "*.ok";
            this._fileSystemWatcher.Path = myfilepath;
            this._fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(_fileSystemWatcher_Created);
            ((System.ComponentModel.ISupportInitialize)(this._fileSystemWatcher)).EndInit();
        }
        void _fileSystemWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                //密碼檔案
                string gpwdfile = e.FullPath.ToString().Replace(".ok",".txt");
                using (StreamReader CurSR = new StreamReader(gpwdfile, Encoding.GetEncoding(950)))
                {
                    string gpwd = CurSR.ReadLine();
                    string pdffile = gpwdfile.Replace(".txt", ".pdf");
                    string encpdffile = outpath + e.Name.Replace(".ok", ".pdf");
                    Encoder.EncryPdf(pdffile, encpdffile, gpwd, "1");
                }
                System.IO.File.Delete(gpwdfile);
                System.IO.File.Delete(gpwdfile.Replace(".txt", ".pdf"));
                System.IO.File.Delete(e.FullPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
