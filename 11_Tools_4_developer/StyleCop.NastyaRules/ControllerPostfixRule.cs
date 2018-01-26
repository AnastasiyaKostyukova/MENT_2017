using System;
using StyleCop.CSharp;

namespace StyleCop.NastyaRules
{
  using System.Web.Mvc;

  [SourceAnalyzer(typeof(CsParser))]
  public class ControllerPostfixRule : SourceAnalyzer
  {
    public override void AnalyzeDocument(CodeDocument document)
    {
      CsDocument csDocument = (CsDocument)document;
      csDocument.WalkDocument(new CodeWalkerElementVisitor<object>(this.VisitElement));
    }

    private bool VisitElement(CsElement element, CsElement parentElement, object context)
    {
      var controller = element as Class;
      if (controller != null )//&&*/ element.GetType().BaseType == typeof(Controller))
      {
        if (!element.Name.EndsWith("NastyaController", StringComparison.Ordinal))
        {
          this.AddViolation(element, "ControllerPostfixRule");
        }

        return false;
      }
      return true;
    }
  }
}
