using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    /// <summary>
    /// An instance holding a tag
    /// </summary>
    /// <typeparam name="TTag"></typeparam>
    public interface ITagged<TTag>
    {
        /// <summary>
        /// Gets or sets the tag
        /// </summary>
        TTag Tag { get; set; }

        /// <summary>
        /// Raised when the tag is changed
        /// </summary>
        event EventHandler TagChanged;
    }
}
