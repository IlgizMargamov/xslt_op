using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XSLT.DTOs;
using XSLT.Interfaces;

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

        private readonly IFileProvider m_fileProvider;
        private readonly IXSLTTransformer m_xSLTTransformer;

        public MainWindowVM(IFileProvider fileProvider, IXSLTTransformer xSLTTransformer)
        {
            m_fileProvider = fileProvider;
            m_xSLTTransformer = xSLTTransformer;
        }

        public void Browse(string tag)
        {
            var outputInput = XSLT.Enums.EnumConverter.GetOutputInput(tag);
            switch (outputInput)
            {
                case Enums.OutputInput.Unknown:
                    break;
                case Enums.OutputInput.Output:
                    PathToOutputFile = m_fileProvider.GetFileName();
                    break;
                case Enums.OutputInput.Input:
                    PathToInputFile = m_fileProvider.GetFileName();
                    break;
                default:
                    break;
            }
        }

        public void Start()
        {
            m_xSLTTransformer.TransformFile(PathToInputFile, PathToOutputFile);
        }
    }
}
