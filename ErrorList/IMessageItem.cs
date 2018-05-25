/* ErrorList
 * http://Suplanus.de by Johann Weiher
 *
 * Control based on the Idea: http://errorlist.codeplex.com/
 *
 */

namespace ErrorList {

    public enum ErrorListLevel {
        Error,
        Warning,
        Information,
        Note,
    }

    public interface IMessageItem {
        ErrorListLevel Level { get; }
        string Description { get; }
    }
}