using XSLT.DTOs;

namespace XSLT.Interfaces
{
    public interface IXSLTTransformer
    {
        IXSLTTransformerOutput TransformFile(string pathToInputFile, string pathToOutputFile);
    }
}
