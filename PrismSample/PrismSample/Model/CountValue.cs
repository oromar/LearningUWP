using Prism.Mvvm;
using System;

namespace PrismSample.Model
{
    public class CountValue: BindableBase
    {
        private int val;
        public int Val
        {
            get { return val; }
            set { SetProperty(ref val, value); }
        }

        private DateTime lastModified;
        public DateTime LastModified
        {
            get { return lastModified; }
            set { SetProperty(ref lastModified, value); }
        }
    }
}
