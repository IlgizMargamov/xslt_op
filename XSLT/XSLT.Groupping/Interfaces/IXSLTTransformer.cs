using Library.DTOs;

namespace Library.Interfaces
{
    public interface IXSLTTransformer
    {
        IXSLTTransformerOutput TransformFileFromListToGroups(string pathToInputFile, string pathToOutputFile);
    }
}
