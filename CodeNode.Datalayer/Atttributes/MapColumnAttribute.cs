using System;

namespace CodeNode.Datalayer.Atttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapColumnAttribute : Attribute
    {
        #region Constructor

        public MapColumnAttribute(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        #endregion
    }
}