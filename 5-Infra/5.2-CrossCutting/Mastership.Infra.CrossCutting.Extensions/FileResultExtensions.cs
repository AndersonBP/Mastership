using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class FileResultExtensions
    {
        public static byte[] Buffer(this FileResult fileResult)
        {

            if (fileResult is FileContentResult)
            {
                var fileContentResult = fileResult as FileContentResult;
                var fileContent = fileContentResult.FileContents;
                return fileContent;
            }
            else if (fileResult is FileStreamResult)
            {
                var fileStreamResult = fileResult as FileStreamResult;
                var fileStream = ConverteStreamToByteArray(fileStreamResult.FileStream);
                return fileStream;
            }
            return new byte[] { };
        }

        public static Stream Stream(this FileResult fileResult)
        {
            return new MemoryStream(fileResult.Buffer());
        }

        public static byte[] ConverteStreamToByteArray(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }
                return mStream.ToArray();
            }
        }
    }
}
