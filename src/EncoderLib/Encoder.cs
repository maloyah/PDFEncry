/*
    This file is part of the PDFEncry, providing a user-friendly
    interface to encrypting pdfs, utilizing the iTextSharp PDF library.
    Copyright (c) 2018 Mark Harrison.
    Contact via https://github.com/harrisonmeister/PdfEncryptor/issues

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderLib
{
    public class Encoder
    {
        public static void EncryPdf(string filein , string fileout ,string pwd , string pwdtype)
        {
            if (pwdtype == "1")
            {

                iTextSharp.text.pdf.PdfReader pr1 = new iTextSharp.text.pdf.PdfReader(filein);
                using (System.IO.MemoryStream mos = new System.IO.MemoryStream())
                {
                    iTextSharp.text.pdf.PdfStamper stamper = new iTextSharp.text.pdf.PdfStamper(pr1, mos);
                    stamper.SetEncryption(System.Text.Encoding.ASCII.GetBytes(pwd), System.Text.Encoding.ASCII.GetBytes(pwd), iTextSharp.text.pdf.PdfWriter.ALLOW_PRINTING, iTextSharp.text.pdf.PdfWriter.STANDARD_ENCRYPTION_128);

                    stamper.Writer.CloseStream = false;
                    stamper.Close();
                    mos.Position = 0;

                    System.IO.File.WriteAllBytes(fileout, mos.ToArray());
                }
            }
        }
    }
}
