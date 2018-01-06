namespace FluentIL.Emitters
{
    using System;

    public class LabelAdapter
        : ILabel,
        IAdaptedLabel
    {
        internal LabelAdapter(string name, object label = null)
        {
            this.Name = name;
            this.Label = label;
        }

        public object Label { get; set; }

        public string Name { get; set; }
    }
}