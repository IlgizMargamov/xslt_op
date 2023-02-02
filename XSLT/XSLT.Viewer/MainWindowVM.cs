using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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
            var filePath = m_fileProvider.GetFileName();
            var isFilePathEmpty = filePath.Equals(string.Empty);
            switch (outputInput)
            {
                case Enums.OutputInput.Output:
                    PathToOutputFile = isFilePathEmpty ? PathToOutputFile : filePath;
                    break;
                case Enums.OutputInput.Input:
                    PathToInputFile = isFilePathEmpty ? PathToInputFile : filePath;
                    break;
                case Enums.OutputInput.Unknown:
                default:
                    MessageBox.Show("Internal error");
                    break;
            }
        }

        public void Start()
        {
            try
            {
                var result = m_xSLTTransformer.TransformFile(PathToInputFile, PathToOutputFile);
                Items = new ObservableCollection<ItemInfo>(result.ItemsList);
                Groups = new ObservableCollection<GroupInfo>(result.Groups);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
