using Library.DTOs;

namespace Library.Interfaces
{
    public interface IXSLTTransformer
    {
        IXSLTTransformerOutput TransformFile(string pathToInputFile, string pathToOutputFile);
    }
}
