using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XSLT.DTOs;

namespace XSLT.Viewer
{
    public class MainWindowVM : BaseViewModel
    {
        public string PathToInputFile
        {
            get => Get("C:\\Users\\Gizon\\Desktop\\xslt\\XSLT\\XSLT.Groupping\\Resources\\List.xml");
            set => Set(value);
        }

        public string PathToOutputFile
        {
            get => Get("C:\\Users\\Gizon\\Desktop\\xslt\\XSLT\\XSLT.Groupping\\Resources\\Groupped.xml");
            set => Set(value);
        }

        public ObservableCollection<ItemInfo> Items
        {
            get => GetColl<ItemInfo>();
            set => SetColl(value);
        }
        
        public ObservableCollection<GroupInfo> Groups
        {
            get => GetColl<GroupInfo>();
            set => SetColl(value);
        }

        public MainWindowVM()
        {

        }

        public void Browse()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
