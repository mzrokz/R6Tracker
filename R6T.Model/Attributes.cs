using R6T.Model.ViewModels;

namespace R6T.Model
{
    using System;
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class AttachXPath : System.Attribute
    {
        private string[] xPath;
        private EMatchType[] statTypes;
        
        public AttachXPath(params string[] _xPath)
        {
            xPath = _xPath;
        }

        public AttachXPath(params EMatchType[] _statTypes)
        {
            statTypes = _statTypes;
        }
        
        public string[] XPath {
            get {
                return xPath;
            }
        }
        public EMatchType[] StatType {
            get {
                return statTypes;
            }
        }
    }
}