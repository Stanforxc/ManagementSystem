using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Services
{
    public class win32_file_iterator
    {
        [DllImport("Win32Project.dll")]
        public static extern IntPtr CreateFileIterator();

        [DllImport("Win32Project.dll")]
        public static extern void DisposeFileIterator(IntPtr wfi_Object);

        public void method()
        {

            IntPtr pWin_file_iter = CreateFileIterator();
        }
    }
}
